using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using lssWebApi2.EntityFramework;
using lssWebApi2.Controllers;
using lssWebApi2.Enumerations;
using lssWebApi2.BudgetRangeDomain;

namespace lssWebApi2.BudgetDomain
{
    
       public class UnitTestBudget
    {
        private readonly ITestOutputHelper output;

        
        public UnitTestBudget(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestPersonalBudgetViews()
        {
            BudgetModule budgetMod = new BudgetModule();

            IList<PersonalBudgetView> list = await budgetMod.Budget.Query().GetPersonalBudgetViews();

            if (list.Count > 0)
            {
                Assert.True(true);
            }

           
        }
        [Fact]
        public async Task TestNextNumber()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            NextNumber nn = await unitOfWork.nextNumberRepository.GetNextNumber(TypeOfPackingSlip.PackingSlipNumber.ToString());

            if (nn.NextNumberValue > 0) { Assert.True(true); }
        }
        [Fact]
        public async Task TestGetBudget()
        {
            long budgetId = 2;
     
              BudgetModule budgetMod = new BudgetModule();

            BudgetView budgetView = await budgetMod.Budget.Query().GetViewById(budgetId);

            if (budgetView != null) { Assert.True(true); }
        }
        [Fact]
        public async Task TestCreateBudget()
        {
             BudgetModule budgetMod = new BudgetModule();

            BudgetRangeView budgetRangeView = new BudgetRangeView();

            //personal mortgage
            ChartOfAccount coa = await budgetMod.ChartOfAccount.Query().GetEntity("1000", "1200", "502", "01");
            budgetRangeView.StartDate= DateTime.Parse("1/1/2018"); ;
            budgetRangeView.EndDate=DateTime.Parse("12/31/2018"); ;

            budgetRangeView.Location=coa.Location;
            budgetRangeView.GenCode = coa.GenCode;
            budgetRangeView.SubCode=coa.SubCode;
            budgetRangeView.CompanyCode=coa.Company.CompanyCode;
            budgetRangeView.BusUnit=coa.BusUnit;
            budgetRangeView.ObjectNumber = coa.ObjectNumber;
            budgetRangeView.Subsidiary= coa.Subsidiary;
            budgetRangeView.AccountId=coa.AccountId;
            budgetRangeView.SupervisorCode=coa.SupCode;
      
            BudgetView budgetView = new BudgetView();
            budgetMod.BudgetRange.CreateBudgetRange(budgetRangeView).Apply();

            BudgetRangeView budgetRangeLookupView = await budgetMod.BudgetRange.Query().GetBudgetRange(budgetRangeView.AccountId, budgetRangeView.StartDate, budgetRangeView.EndDate);

            budgetMod.Budget.MapRangeToBudgetView(ref budgetView, budgetRangeLookupView);

            BudgetActualsView budgetActualsView = await budgetMod.Budget.Query().GetBudgetActualsView(budgetRangeLookupView);

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
