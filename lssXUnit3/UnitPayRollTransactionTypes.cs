using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.PayRollDomain
{

    public class UnitPayRollTransactionTypes
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollTransactionTypes(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollTransactionTypesModule PayRollTransactionTypesMod = new PayRollTransactionTypesModule();

           PayRollTransactionTypesView view = new PayRollTransactionTypesView()
            {
                    Description = "PayRollTransactionTypes Test",
               PayRollTranactionCode = 99

            };
            NextNumber nnNextNumber = await PayRollTransactionTypesMod.PayRollTransactionTypes.Query().GetNextNumber();
            view.PayRollTransactionTypesNumber = nnNextNumber.NextNumberValue;

            PayRollTransactionTypes payRollTransactionTypes = await PayRollTransactionTypesMod.PayRollTransactionTypes.Query().MapToEntity(view);

            PayRollTransactionTypesMod.PayRollTransactionTypes.AddPayRollTransactionTypes(payRollTransactionTypes).Apply();

            PayRollTransactionTypes newPayRollTransactionTypes = await PayRollTransactionTypesMod.PayRollTransactionTypes.Query().GetEntityByNumber(view.PayRollTransactionTypesNumber);

            Assert.NotNull(newPayRollTransactionTypes);

            newPayRollTransactionTypes.Description = "PayRollTransactionTypes Test Update";

            PayRollTransactionTypesMod.PayRollTransactionTypes.UpdatePayRollTransactionTypes(newPayRollTransactionTypes).Apply();

            PayRollTransactionTypesView updateView = await PayRollTransactionTypesMod.PayRollTransactionTypes.Query().GetViewById(newPayRollTransactionTypes.PayRollTransactionTypesId);

            Assert.Same(updateView.Description, "PayRollTransactionTypes Test Update");

			PayRollTransactionTypesMod.PayRollTransactionTypes.DeletePayRollTransactionTypes(newPayRollTransactionTypes).Apply();
            PayRollTransactionTypes lookupPayRollTransactionTypes= await PayRollTransactionTypesMod.PayRollTransactionTypes.Query().GetEntityById(view.PayRollTransactionTypesId);

            Assert.Null(lookupPayRollTransactionTypes);
        }
       
      

    }
}
