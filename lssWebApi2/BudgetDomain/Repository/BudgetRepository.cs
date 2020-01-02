using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.Services;
using lssWebApi2.BudgetsDomain.Repository;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetDomain
{
    public class BudgetActualsView
    {
        public decimal? ActualHours { get; set; }
        public decimal? ActualAmount { get; set; }
        public long? AccountId { get; set; }
        public long? RangeId { get; set; }
        public DateTime? RangeStartDate { get; set; }
        public DateTime? RangeEndDate { get; set; }
        public long BudgetNumber { get; set; }
    }
    public class BudgetView
    {

        public long BudgetId { get; set; }
        public decimal? BudgetHours { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? ActualAmount { get; set; }
        public long? AccountId { get; set; }
        public long? RangeId { get; set; }
        public decimal? ProjectedHours { get; set; }
        public decimal? ProjectedAmount { get; set; }
        public DateTime? ActualsAsOfDate { get; set; }
        public long BudgetNumber { get; set; }

        public string AccountDescription { get; set; }
        public string BusUnit { get; set; }
        public string ObjectNumber { get; set; }
        public string Subsidiary { get; set; }
        public bool? RangeIsActive { get; set; }
        public DateTime? RangeStartDate { get; set; }
        public DateTime? RangeEndDate { get; set; }
        public string CompanyCode { get; set; }
        public string SupervisorCode { get; set; }

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
        public string CompanyCode { get; set; }
        public decimal? BudgetAmount { get; set; }
        public int PayCycles { get; set; }
        public decimal? BudgetHours { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? PaymentHours { get; set; }
        public DateTime GLDate { get; set; }
    }
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public BudgetRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IList<Budget>> GetBudgets()
        {
            try
            {

                var query = await (from e in _dbContext.Budget

                                   select e).ToListAsync<Budget>();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<IList<PersonalBudgetView>> GetPersonalBudgetViews()
        {
            try
            {
                IList<PersonalBudgetView> list = await (from coa in _dbContext.ChartOfAccount
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
                                                            CompanyCode = coa.CompanyCode,
                                                            BudgetAmount = bud.BudgetAmount,
                                                            BudgetHours = bud.BudgetHours,
                                                            StartDate = bud_range.StartDate,
                                                            EndDate = bud_range.EndDate,
                                                            PayCycles = bud_range.PayCycles ?? 0,
                                                            GLDate = DateTime.Now
                                                        }).ToListAsync<PersonalBudgetView>();


                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }


        public async Task<Budget> GetEntityById(long ? budgetId)
        {
            try
            {
                return await _dbContext.FindAsync<Budget>(budgetId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Budget> GetEntityByNumber(long budgetNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.Budget
                                   where detail.BudgetNumber == budgetNumber
                                   select detail).FirstOrDefaultAsync<Budget>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
     
        public async Task<Budget> FindEntityByExpression(Expression<Func<Budget, bool>> predicate)
        {
            IQueryable<Budget> result = _dbContext.Set<Budget>().Where(predicate);

            return await result.FirstOrDefaultAsync<Budget>();
        }
        public async Task<IList<Budget>> FindEntitiesByExpression(Expression<Func<Budget, bool>> predicate)
        {
            IQueryable<Budget> result = _dbContext.Set<Budget>().Where(predicate);

            return await result.ToListAsync<Budget>();
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



    }
}
