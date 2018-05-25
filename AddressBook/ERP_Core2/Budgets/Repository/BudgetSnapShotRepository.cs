using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.Services
{
    public class BudgetSnapShotRepository : Repository<BudgetSnapShot>
    {
        Entities _dbContext;
        public BudgetSnapShotRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
    }
}
