using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.CarrierDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.CarrierDomain
{

    public class UnitCarrier
    {

        private readonly ITestOutputHelper output;

        public UnitCarrier(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            CarrierModule CarrierMod = new CarrierModule();
            Udc udcCarrier = await CarrierMod.Udc.Query().GetEntityById(41);
            AddressBook addressBook = await CarrierMod.AddressBook.Query().GetEntityById(18);

            CarrierView view = new CarrierView()
            {

                AddressId = addressBook.AddressId,
                CarrierTypeXrefId = udcCarrier.XrefId,
                CarrierType = udcCarrier.Value,
                CarrierName=addressBook.Name
                    
            };
            NextNumber nnNextNumber = await CarrierMod.Carrier.Query().GetNextNumber();

            view.CarrierNumber = nnNextNumber.NextNumberValue;

            Carrier carrier = await CarrierMod.Carrier.Query().MapToEntity(view);

            CarrierMod.Carrier.AddCarrier(carrier).Apply();

            Carrier newCarrier = await CarrierMod.Carrier.Query().GetEntityByNumber(view.CarrierNumber);

            Assert.NotNull(newCarrier);

            newCarrier.AddressId=19;

            CarrierMod.Carrier.UpdateCarrier(newCarrier).Apply();

            CarrierView updateView = await CarrierMod.Carrier.Query().GetViewById(newCarrier.CarrierId);

            if (updateView.AddressId == 19) Assert.True(true);

              CarrierMod.Carrier.DeleteCarrier(newCarrier).Apply();
            Carrier lookupCarrier= await CarrierMod.Carrier.Query().GetEntityById(updateView.CarrierId??0);

            Assert.Null(lookupCarrier);
        }
       
      

    }
}
