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

namespace ERP_Core2.PayRollDomain
{

    public class UnitPayRollTotals
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollTotals(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollTotalsModule PayRollTotalsMod = new PayRollTotalsModule();

           PayRollTotalsView view = new PayRollTotalsView()
            {
                    Description = 'PayRollTotals Test',
                    PayRollTotalsCode=99

            };
            NextNumber nnNextNumber = await PayRollTotalsMod.PayRollTotals.Query().GetNextNumber();

            view.PayRollTotalsNumber = nnNextNumber.NextNumberValue;

            PayRollTotals payRollTotals = await PayRollTotalsMod.PayRollTotals.Query().MapToEntity(view);

            PayRollTotalsMod..AddPayRollTotals(payRollTotals).Apply();

            PayRollTotals newPayRollTotals = await PayRollTotalsMod.PayRollTotals.Query().GetEntityByNumber(view.PayRollTotalsNumber);

            Assert.NotNull(newPayRollTotals);

            newPayRollTotals.Description = 'PayRollTotals Test Update';

            PayRollTotalsMod..UpdatePayRollTotals(newPayRollTotals).Apply();

            PayRollTotalsView updateView = await PayRollTotalsMod.PayRollTotals.Query().GetViewById(newPayRollTotals.PayRollTotalsId);

            Assert.Same(updateView.Description, 'PayRollTotals Test Update');
            int code = 99;
           

                   PayRollTotalsMod..DeletePayRollTotals(newPayRollTotals).Apply();
            PayRollTotals lookupPayRollTotals= await PayRollTotalsMod.PayRollTotals.Query().GetEntityById(view.PayRollTotalsId);

            Assert.Null(lookupPayRollTotals);
        }
       
      

    }
}
