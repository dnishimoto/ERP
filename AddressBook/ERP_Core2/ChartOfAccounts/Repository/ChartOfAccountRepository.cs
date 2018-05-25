using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.Services
{
    public class ChartOfAccountRepository : Repository<ChartOfAcct>
    {
        Entities _dbContext;
        public ChartOfAccountRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
    }
}
