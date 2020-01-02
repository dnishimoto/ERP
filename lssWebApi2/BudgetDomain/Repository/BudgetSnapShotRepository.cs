using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace lssWebApi2.BudgetDomain
{
    public class BudgetSnapShotRepository : Repository<BudgetSnapShot>
    {
        ListensoftwaredbContext _dbContext;
        public BudgetSnapShotRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    }
}
