using MillenniumERP.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.Services
{

    public class ScheduleEventRepository: Repository<ScheduleEvent>
    {
        Entities _dbContext;
        public ScheduleEventRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
        public async Task<IQueryable<ScheduleEvent>> GetScheduleEvents(int employeeAddressId)
        {
           
            var list = await base.GetObjectsAsync(e => e.EmployeeAddressId == employeeAddressId, "EmployeeAddressBook").ToListAsync();
           
            return list.AsQueryable<ScheduleEvent>();
           
        }
    }
}
