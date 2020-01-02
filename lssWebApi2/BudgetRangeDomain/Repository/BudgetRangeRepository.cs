using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.BudgetRangeDomain.Repository;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetRangeDomain
{
    public class BudgetRangeView
    {

        public long RangeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string GenCode { get; set; }
        public string SubCode { get; set; }
        public string CompanyCode { get; set; }
        public string BusUnit { get; set; }
        public string Subsidiary { get; set; }
        public long? AccountId { get; set; }
        public string SupervisorCode { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string ObjectNumber { get; set; }
        public bool? IsActive { get; set; }
        public int? PayCycles { get; set; }
        public long BudgetRangeNumber { get; set; }


        public string AccountDescription { get; set; }
        public string CompanyName { get; set; }



    }
     public class BudgetRangeRepository : Repository<BudgetRange>, IBudgetRangeRepository
    {
        ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public BudgetRangeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<BudgetRange> GetBudgetRange(long ?accountId, DateTime ? startDate, DateTime ? endDate)
        {
            try
            {
                BudgetRange budgetRange = await(from e in _dbContext.BudgetRange
                                  where e.AccountId == accountId
                                  && e.StartDate == startDate
                                  && e.EndDate == endDate
                                  select e
                          ).FirstOrDefaultAsync<BudgetRange>();

                return budgetRange;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<BudgetRange> GetEntityById(long ? budgetRangeId)
        {
            try
            {
                return await _dbContext.FindAsync<BudgetRange>(budgetRangeId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<BudgetRange> GetEntityByNumber(long  budgetRangeNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.BudgetRange
                                   where detail.BudgetRangeNumber == budgetRangeNumber
                                   select detail).FirstOrDefaultAsync<BudgetRange>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
     
        public async Task<BudgetRange> FindEntityByExpression(Expression<Func<BudgetRange, bool>> predicate)
        {
            IQueryable<BudgetRange> result = _dbContext.Set<BudgetRange>().Where(predicate);

            return await result.FirstOrDefaultAsync<BudgetRange>();
        }
        public async Task<IList<BudgetRange>> FindEntitiesByExpression(Expression<Func<BudgetRange, bool>> predicate)
        {
            IQueryable<BudgetRange> result = _dbContext.Set<BudgetRange>().Where(predicate);

            return await result.ToListAsync<BudgetRange>();
        }

    }
}
