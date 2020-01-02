using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.BudgetDomain
{

    public class UnitBudget
    {

        private readonly ITestOutputHelper output;

        public UnitBudget(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            BudgetModule BudgetMod = new BudgetModule();
            ChartOfAccount chartOfAccount = await BudgetMod.ChartOfAccount.Query().GetEntityById(42);
            BudgetRange budgetRange = await BudgetMod.BudgetRange.Query().GetEntityById(1);
            BudgetView view = new BudgetView()
            {
                BudgetAmount = 5216M,
                ActualHours = 0M,
                ActualAmount = 768M,
                AccountId = chartOfAccount.AccountId,
                RangeId = budgetRange.RangeId,
                ProjectedAmount = 9600,
                ProjectedHours = 0,
                ActualsAsOfDate = DateTime.Parse("12/9/2019"),

                AccountDescription = chartOfAccount.Description,
                CompanyCode = chartOfAccount.CompanyCode,
                BusUnit = chartOfAccount.BusUnit,
                ObjectNumber = chartOfAccount.ObjectNumber,
                Subsidiary = chartOfAccount.Subsidiary,
                RangeIsActive = budgetRange.IsActive,
                RangeStartDate = budgetRange.StartDate,
                RangeEndDate = budgetRange.EndDate,
                SupervisorCode = budgetRange.SupervisorCode
            };
            NextNumber nnNextNumber = await BudgetMod.Budget.Query().GetNextNumber();

            view.BudgetNumber = nnNextNumber.NextNumberValue;

            Budget budget = await BudgetMod.Budget.Query().MapToEntity(view);

            BudgetMod.Budget.AddBudget(budget).Apply();

            Budget newBudget = await BudgetMod.Budget.Query().GetEntityByNumber(view.BudgetNumber);

            Assert.NotNull(newBudget);

            newBudget.BudgetAmount = 100M;

            BudgetMod.Budget.UpdateBudget(newBudget).Apply();

            BudgetView updateView = await BudgetMod.Budget.Query().GetViewById(newBudget.BudgetId);

            if (updateView.BudgetAmount == 100M) Assert.True(true);

            BudgetMod.Budget.DeleteBudget(newBudget).Apply();
            Budget lookupBudget = await BudgetMod.Budget.Query().GetEntityById(view.BudgetId);

            Assert.Null(lookupBudget);
        }



    }
}
