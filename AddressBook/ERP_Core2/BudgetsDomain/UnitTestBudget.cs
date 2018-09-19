using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace ERP_Core2.BudgetDomain
{
    
       public class UnitTestBudget
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestBudget(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestCreateBudget()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            BudgetModule budgetMod = new BudgetModule();

            BudgetRangeView budgetRangeView = new BudgetRangeView();

            //personal mortgage
            ChartOfAcct coa = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "502", "01");
            budgetRangeView.StartDate= DateTime.Parse("1/1/2018"); ;
            budgetRangeView.EndDate=DateTime.Parse("12/31/2018"); ;

            budgetRangeView.Location=coa.Location;
            budgetRangeView.GenCode = coa.GenCode;
            budgetRangeView.SubCode=coa.SubCode;
            budgetRangeView.CompanyCode=coa.Company.CompanyCode;
            budgetRangeView.BusinessUnit=coa.BusUnit;
            budgetRangeView.ObjectNumber = coa.ObjectNumber;
            budgetRangeView.Subsidiary= coa.Subsidiary;
            budgetRangeView.AccountId=coa.AccountId;
            budgetRangeView.SupervisorCode=coa.SupCode;
      
            BudgetView budgetView = new BudgetView();
            budgetMod.BudgetRange.CreateBudgetRange(budgetRangeView).Apply();

            BudgetRangeView budgetRangeLookupView = budgetMod.BudgetRange.Query().GetBudgetRange(budgetRangeView.AccountId, budgetRangeView.StartDate, budgetRangeView.EndDate);

            budgetMod.Budget.MapRangeToBudgetView(ref budgetView, budgetRangeLookupView);

            BudgetActualsView budgetActualsView = budgetMod.Budget.Query().GetBudgetActuals(budgetRangeLookupView);

            budgetView.BudgetAmount = 768 * 12;
            budgetView.BudgetHours = 0;
            budgetView.ProjectedAmount = 800 * 12;
            budgetView.ProjectedHours = 0;
            budgetView.ActualAmount = budgetActualsView.ActualAmount;
            budgetView.ActualHours = budgetActualsView.ActualHours;
            
            budgetMod.Budget.TransactBudget(budgetView).Apply();

            Assert.True(true);
        }
     
    }
}
