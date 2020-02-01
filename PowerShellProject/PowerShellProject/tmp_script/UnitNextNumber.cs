using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.NextNumberDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.NextNumberDomain
{

    public class UnitNextNumber
    {

        private readonly ITestOutputHelper output;

        public UnitNextNumber(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            NextNumberModule NextNumberMod = new NextNumberModule();

           NextNumberView view = new NextNumberView()
            {
                    Description = 'NextNumber Test',
                    NextNumberCode=99

            };
            NextNumber nnNextNumber = await NextNumberMod.NextNumber.Query().GetNextNumber();

            view.NextNumberNumber = nnNextNumber.NextNumberValue;

            NextNumber nextNumber = await NextNumberMod.NextNumber.Query().MapToEntity(view);

            NextNumberMod.NextNumber.AddNextNumber(nextNumber).Apply();

            NextNumber newNextNumber = await NextNumberMod.NextNumber.Query().GetEntityByNumber(view.NextNumberNumber);

            Assert.NotNull(newNextNumber);

            newNextNumber.Description = 'NextNumber Test Update';

            NextNumberMod.NextNumber.UpdateNextNumber(newNextNumber).Apply();

            NextNumberView updateView = await NextNumberMod.NextNumber.Query().GetViewById(newNextNumber.NextNumberId);

            Assert.Same(updateView.Description, 'NextNumber Test Update');
              NextNumberMod.NextNumber.DeleteNextNumber(newNextNumber).Apply();
            NextNumber lookupNextNumber= await NextNumberMod.NextNumber.Query().GetEntityById(view.NextNumberId);

            Assert.Null(lookupNextNumber);
        }
       
      

    }
}
