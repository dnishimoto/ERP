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
    public class BudgetRangeView
    {
        public BudgetRangeView() { }
        public BudgetRangeView(BudgetRange budgetRange)
        {
            this.RangeId = budgetRange.RangeId;
            this.StartDate = budgetRange.StartDate;
            this.EndDate = budgetRange.EndDate;
            this.Location = budgetRange.Location;
            this.GenCode = budgetRange.GenCode;
            this.SubCode = budgetRange.SubCode;
            this.CompanyCode = budgetRange.CompanyCode;
            this.BusinessUnit = budgetRange.BusinessUnit;
            this.ObjectNumber = budgetRange.ObjectNumber;
            this.Subsidiary = budgetRange.Subsidiary;
            this.AccountId = budgetRange.AccountId;
            this.SupervisorCode = budgetRange.SupervisorCode;
    }
        public long RangeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string GenCode { get; set; }
        public string SubCode { get; set; }
        public string CompanyCode { get; set; }
        public string BusinessUnit { get; set; }
        public string ObjectNumber { get; set; }
        public string Subsidiary { get; set; }
        public long? AccountId { get; set; }
        public string SupervisorCode { get; set; }
    }

    public class BudgetRangeRepository : Repository<BudgetRange>
    {
        Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public BudgetRangeRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
        public async Task<BudgetRangeView> GetBudgetRange(long ?accountId, DateTime ? startDate, DateTime ? endDate)
        {
            try
            {
                BudgetRange budgetRange = await(from e in _dbContext.BudgetRanges
                                  where e.AccountId == accountId
                                  && e.StartDate == startDate
                                  && e.EndDate == endDate
                                  select e
                          ).FirstOrDefaultAsync<BudgetRange>();

                return applicationViewFactory.MapBudgetRangeView(budgetRange);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<CreateProcessStatus> CreateBudgetRange(BudgetRangeView budgetRangeView)
        {
            try
            {
                var query = await (from e in _dbContext.BudgetRanges
                             where e.AccountId == budgetRangeView.AccountId
                             && e.StartDate == budgetRangeView.StartDate
                             && e.EndDate == budgetRangeView.EndDate
                             select e
                          ).FirstOrDefaultAsync<BudgetRange>();
                if (query == null)
                {
                    BudgetRange budgetRange = new BudgetRange();
                    applicationViewFactory.MapBudgetRangeEntity(ref budgetRange, budgetRangeView);
                    AddObject(budgetRange);
                    return CreateProcessStatus.Insert;
                }
                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
