using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            this.BudgetAmount = budget.BudgetAmount;
            this.ActualHours = budget.ActualHours;
            this.ActualAmount = budget.ActualAmount;
            this.AccountId = budget.AccountId;
            this.AccountDescription = budget.Account.Description;
            this.CompanyNumber = budget.Account.CompanyNumber;
            this.BusUnit = budget.Account.BusUnit;
            this.ObjectNumber = budget.Account.ObjectNumber;
            this.Subsidiary = budget.Account.Subsidiary;
            this.RangeId = budget.RangeId;
            this.RangeStartDate = budget.Range.StartDate;
            this.RangeEndDate = budget.Range.EndDate;
            this.CompanyCode = budget.Range.CompanyCode;
            this.SupervisorCode = budget.Range.SupervisorCode;
            this.ProjectedHours = budget.ProjectedHours;
            this.ProjectedAmount = budget.ProjectedAmount;
            this.RangeIsActive = budget.Range.IsActive;
            this.ActualsAsOfDate = budget.ActualsAsOfDate;

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
        public bool? RangeIsActive { get; set; }
        public DateTime? ActualsAsOfDate { get; set; }

    }
    public class PersonalBudgetView
    {
        public long AccountId { get; set; }
        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string ObjectNumber { get; set; }
        public string SupCode { get; set; }
        public string Subsidiary { get; set; }
        public string SubSub { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CompanyNumber { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? BudgetHours { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? PaymentHours { get; set; }
    }
    public class BudgetRepository : Repository<Budget>
    {
        ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public BudgetRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<List<PersonalBudgetView>> GetPersonalBudgetViews()
        {
            try
            {
                List<PersonalBudgetView> list = await (from coa in _dbContext.ChartOfAccts
                                                       join bud in _dbContext.Budget
                                             on coa.AccountId equals bud.AccountId
                                                       join bud_range in _dbContext.BudgetRange
                                                       on bud.AccountId equals bud_range.AccountId
                                                       select new PersonalBudgetView
                                                       {
                                                           AccountId = coa.AccountId,
                                                           Location = coa.Location,
                                                           BusUnit = coa.BusUnit,
                                                           ObjectNumber = coa.ObjectNumber,
                                                           SupCode = coa.SupCode,
                                                           Subsidiary = coa.Subsidiary,
                                                           SubSub = coa.SubSub,
                                                           Account = coa.Account,
                                                           Description = coa.Description,
                                                           CompanyNumber = coa.CompanyNumber,
                                                           BudgetAmount = bud.BudgetAmount,
                                                           BudgetHours = bud.BudgetHours,
                                                           StartDate = bud_range.StartDate,
                                                           EndDate = bud_range.EndDate
                                                       }).ToListAsync<PersonalBudgetView>();


                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<BudgetView> GetBudgetView(long budgetId)
        {
            try
            {
                Budget budget = await (from e in _dbContext.Budget
                                       where e.BudgetId == budgetId
                                       select e).FirstOrDefaultAsync<Budget>();

                BudgetView budgetView = null;
                if (budget != null)
                {
                    budgetView = applicationViewFactory.MapBudgetView(budget);
                }
                return budgetView;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<List<BudgetView>> GetBudgetViews()
        {
            try
            {
                var query = await (from e in _dbContext.Budget
                                   select e).ToListAsync<Budget>();

                List<BudgetView> budgetViews = new List<BudgetView>();
                BudgetView budgetView = null;
                foreach (var budget in query)
                {
                    budgetView = applicationViewFactory.MapBudgetView(budget);
                    budgetViews.Add(budgetView);
                }
                return budgetViews;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<BudgetActualsView> GetActualsView(BudgetRangeView budgetRangeView)
        {
            try
            {
                Udc udcActuals = await GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcHours = await GetUdc("GENERALLEDGERTYPE", "HA");



                BudgetActualsView budgetActualsView = applicationViewFactory.MapBudgetRangeToBudgetActuals(budgetRangeView);


                //query actual amounts
                decimal actualAmount = (from e in _dbContext.GeneralLedger
                                        where e.AccountId == budgetRangeView.AccountId
                                        && e.Gldate >= budgetRangeView.StartDate
                                        && e.Gldate <= budgetRangeView.EndDate
                                        && e.LedgerType == udcActuals.KeyCode
                                        select e.Amount
                           ).Sum();
                budgetActualsView.ActualAmount = actualAmount;
                //query actual hours
                decimal? actualHours = (from e in _dbContext.GeneralLedger
                                        where e.AccountId == budgetRangeView.AccountId
                                        && e.Gldate >= budgetRangeView.StartDate
                                        && e.Gldate <= budgetRangeView.EndDate
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
                Budget budgetLookup = await (from e in _dbContext.Budget
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
