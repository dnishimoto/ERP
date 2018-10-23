using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ScheduleEventsDomain
{

    public class ScheduleEventRepository: Repository<ScheduleEvent>
    {
        ListensoftwaredbContext _dbContext;
        public ScheduleEventRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IQueryable<ScheduleEvent>> GetScheduleEventsByEmployeeId(long employeeId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.EmployeeId == employeeId, "Employee").ToListAsync();

                return list.AsQueryable<ScheduleEvent>();
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
           
        }
    }
}
