using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.UDCDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.UDCDomain
{

    public class UnitUdc
    {

        private readonly ITestOutputHelper output;

        public UnitUdc(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            UdcModule UdcMod = new UdcModule();

           UdcView view = new UdcView()
            {
                  ProductCode= "Test",
                  KeyCode="TestCode",
                  Value="TestValue"

            };
            NextNumber nnNextNumber = await UdcMod.Udc.Query().GetNextNumber();

            view.UdcNumber = nnNextNumber.NextNumberValue;

            Udc udc = await UdcMod.Udc.Query().MapToEntity(view);

            UdcMod.Udc.AddUdc(udc).Apply();

            Udc newUdc = await UdcMod.Udc.Query().GetEntityByNumber(view.UdcNumber);

            Assert.NotNull(newUdc);

            newUdc.KeyCode = "KeyCodeTest";

            UdcMod.Udc.UpdateUdc(newUdc).Apply();

            UdcView updateView = await UdcMod.Udc.Query().GetViewById(newUdc.XrefId);

            Assert.Same(updateView.KeyCode,"KeyCodeTest");
              UdcMod.Udc.DeleteUdc(newUdc).Apply();
            Udc lookupUdc= await UdcMod.Udc.Query().GetEntityById(view.XrefId);

            Assert.Null(lookupUdc);
        }
       
      

    }
}
