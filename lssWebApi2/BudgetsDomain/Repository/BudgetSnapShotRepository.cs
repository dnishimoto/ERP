using ERP_Core2.Services;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.BudgetDomain
{
    public class BudgetSnapShotRepository : Repository<BudgetSnapShot>
    {
        ListensoftwareDBContext _dbContext;
        public BudgetSnapShotRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
        }
    }
}
