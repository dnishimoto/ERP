using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;

using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.BudgetDomain
{
    public class BudgetActualsView
    {
        public decimal? ActualHours { get; set; }
        public decimal? ActualAmount { get; set; }
        public long? AccountId { get; set; }
        public long? RangeId { get; set; }
        public DateTime? RangeStartDate { get; set; }
        public DateTime? RangeEndDate { get; set; }
    }
    public class BudgetView
    {
        public BudgetView() { }
        public BudgetView(Budget budget)
        {
            this.BudgetId = budget.BudgetId;
            this.BudgetHours = budget.BudgetHours;
            this.ActualHours = budget.ActualHours;
            this.ActualAmount = budget.ActualAmount;
            this.AccountId = budget.AccountId;
            this.AccountDescription = budget.ChartOfAcct.Description;
            this.CompanyNumber = budget.ChartOfAcct.CompanyNumber;
            this.BusUnit = budget.ChartOfAcct.BusUnit;
            this.ObjectNumber = budget.ChartOfAcct.ObjectNumber;
            this.Subsidiary = budget.ChartOfAcct.Subsidiary;
            this.RangeId = budget.RangeId;
            this.RangeStartDate = budget.BudgetRange.StartDate;
            this.RangeEndDate = budget.BudgetRange.EndDate;
            this.CompanyCode = budget.BudgetRange.CompanyCode;
            this.SupervisorCode = budget.BudgetRange.SupervisorCode;
            this.ProjectedHours = budget.ProjectedHours;
            this.ProjectedAmount = budget.ProjectedAmount;
        }
        public long BudgetId { get; set; }
        public decimal? BudgetHours { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? ActualAmount { get; set; }
        public long? AccountId { get; set; }
        public string AccountDescription { get; set; }
        public string CompanyNumber { get; set; }
        public string BusUnit { get; set; }
        public string ObjectNumber { get; set; }
        public string Subsidiary { get; set; }
        public long? RangeId { get; set; }
        public DateTime? RangeStartDate { get; set; }
        public DateTime? RangeEndDate { get; set; }
        public string CompanyCode { get; set; }
        public string SupervisorCode { get; set; }
        public decimal? ProjectedHours { get; set; }
        public decimal? ProjectedAmount { get; set; }
    }
    public class BudgetRepository : Repository<Budget>
    {
        Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public BudgetRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
        public async Task<BudgetView> GetBudgetView(long budgetId)
        {
            Budget budget = await (from e in _dbContext.Budgets
                                   where e.BudgetId == budgetId
                                   select e).FirstOrDefaultAsync<Budget>();

            BudgetView budgetView = null;
            if (budget != null)
            {
                budgetView=applicationViewFactory.MapBudgetView(budget);
            }
            return budgetView;

        }
                            
        public async Task<BudgetActualsView> GetActualsView(BudgetRangeView budgetRangeView)
        {
            try
            {
                UDC udcActuals = await GetUdc("GENERALLEDGERTYPE", "AA");
                UDC udcHours = await GetUdc("GENERALLEDGERTYPE", "HA");



                BudgetActualsView budgetActualsView = applicationViewFactory.MapBudgetRangeToBudgetActuals(budgetRangeView);


                //query actual amounts
                decimal actualAmount = (from e in _dbContext.GeneralLedgers
                                        where e.AccountId == budgetRangeView.AccountId
                                        && e.GLDate >= budgetRangeView.StartDate
                                        && e.GLDate <= budgetRangeView.EndDate
                                        && e.LedgerType == udcActuals.KeyCode
                                        select e.Amount
                           ).Sum();
                budgetActualsView.ActualAmount = actualAmount;
                //query actual hours
                decimal? actualHours = (from e in _dbContext.GeneralLedgers
                                        where e.AccountId == budgetRangeView.AccountId
                                        && e.GLDate >= budgetRangeView.StartDate
                                        && e.GLDate <= budgetRangeView.EndDate
                                        && e.LedgerType == udcHours.KeyCode
                                        select e.Units
               ).Sum();
                budgetActualsView.ActualHours = actualHours ?? 0;
                return budgetActualsView;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }

        public void MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView)
        {
            applicationViewFactory.MapRangeToBudgetViewEntity(ref budgetView, budgetRangeView);
        }
        public async Task<CreateProcessStatus> TransactBudget(BudgetView budgetView)
        {
            try
            {
                Budget budgetLookup = await (from e in _dbContext.Budgets
                                   where e.AccountId == budgetView.AccountId
                                   && e.RangeId == budgetView.RangeId
                                   select e
                          ).FirstOrDefaultAsync<Budget>();
                if (budgetLookup == null)
                {
                    Budget newBudget = new Budget();
                    applicationViewFactory.MapBudgetEntity(ref newBudget, budgetView);
                    AddObject(newBudget);
                    return CreateProcessStatus.Insert;
                }
                else

                {
                    budgetView.BudgetId = budgetLookup.BudgetId;
                    applicationViewFactory.MapBudgetEntity(ref budgetLookup, budgetView);
                    UpdateObject(budgetLookup);
                    return CreateProcessStatus.Update;
                }
                
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
