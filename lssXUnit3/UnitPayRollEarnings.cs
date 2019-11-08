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

    public class UnitPayRollEarnings
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollEarnings(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestGetByEarningCode()
        {
            PayRollEarningsModule PayRollEarningsMod = new PayRollEarningsModule();

            int earningCode = 1;
            string earningType = "E";
            PayRollEarningsView view = await PayRollEarningsMod.PayRollEarnings.Query().GetViewByEarningCode(earningCode,earningType);
            Assert.Null(view);
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollEarningsModule PayRollEarningsMod = new PayRollEarningsModule();

           PayRollEarningsView view = new PayRollEarningsView()
            {
                EarningCode=1,
                Description ="Earnings",
                EarningType ="E"
            };
            NextNumber nnNextNumber = await PayRollEarningsMod.PayRollEarnings.Query().GetNextNumber();

            view.PayRollEarningsNumber = nnNextNumber.NextNumberValue;

            PayRollEarnings payRollEarnings = await PayRollEarningsMod.PayRollEarnings.Query().MapToEntity(view);

            PayRollEarningsMod.PayRollEarnings.AddPayRollEarnings(payRollEarnings).Apply();

            PayRollEarnings newPayRollEarnings = await PayRollEarningsMod.PayRollEarnings.Query().GetEntityByNumber(view.PayRollEarningsNumber);

            Assert.NotNull(newPayRollEarnings);

            newPayRollEarnings.Description = "Earnings Update";

            PayRollEarningsMod.PayRollEarnings.UpdatePayRollEarnings(newPayRollEarnings).Apply();

            PayRollEarningsView updateView = await PayRollEarningsMod.PayRollEarnings.Query().GetViewById(newPayRollEarnings.PayRollEarningsId);

            Assert.Same(updateView.Description, "Earnings Update");
              PayRollEarningsMod.PayRollEarnings.DeletePayRollEarnings(newPayRollEarnings).Apply();
            PayRollEarnings lookupPayRollEarnings= await PayRollEarningsMod.PayRollEarnings.Query().GetEntityById(view.PayRollEarningsId);

            Assert.Null(lookupPayRollEarnings);
        }
       
      

    }
}
