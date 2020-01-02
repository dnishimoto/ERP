using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.BudgetRangeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.BudgetRangeDomain
{

    public class UnitBudgetRange
    {

        private readonly ITestOutputHelper output;

        public UnitBudgetRange(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            BudgetRangeModule BudgetRangeMod = new BudgetRangeModule();
            ChartOfAccount chartOfAccount = await BudgetRangeMod.ChartOfAccount.Query().GetEntityById(42);
            Company company = await BudgetRangeMod.Company.Query().GetEntityById(1);

            BudgetRangeView view = new BudgetRangeView()
            {
                StartDate = DateTime.Parse("1/1/2019"),
                EndDate = DateTime.Parse("12/31/2019"),
                Location = chartOfAccount.Location,
                GenCode = chartOfAccount.GenCode,
                CompanyCode = company.CompanyCode,
                BusUnit = chartOfAccount.BusUnit,
                Subsidiary=chartOfAccount.Subsidiary,
                AccountId=chartOfAccount.AccountId,
                ObjectNumber=chartOfAccount.ObjectNumber,
                SupervisorCode="4366",
                LastUpdated=DateTime.Parse("12/8/2019"),
                IsActive=true,
                PayCycles=12,
                AccountDescription=chartOfAccount.Description


            };
            NextNumber nnNextNumber = await BudgetRangeMod.BudgetRange.Query().GetNextNumber();

            view.BudgetRangeNumber = nnNextNumber.NextNumberValue;

            BudgetRange budgetRange = await BudgetRangeMod.BudgetRange.Query().MapToEntity(view);

            BudgetRangeMod.BudgetRange.AddBudgetRange(budgetRange).Apply();

            BudgetRange newBudgetRange = await BudgetRangeMod.BudgetRange.Query().GetEntityByNumber(view.BudgetRangeNumber);

            Assert.NotNull(newBudgetRange);

            newBudgetRange.IsActive=false;

            BudgetRangeMod.BudgetRange.UpdateBudgetRange(newBudgetRange).Apply();

            BudgetRangeView updateView = await BudgetRangeMod.BudgetRange.Query().GetViewById(newBudgetRange.RangeId);

            if (updateView.IsActive == false) Assert.True(true);

              BudgetRangeMod.BudgetRange.DeleteBudgetRange(newBudgetRange).Apply();
            BudgetRange lookupBudgetRange= await BudgetRangeMod.BudgetRange.Query().GetEntityById(view.RangeId);

            Assert.Null(lookupBudgetRange);
        }
       
      

    }
}
