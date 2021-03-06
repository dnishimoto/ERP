﻿
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using ERP_Core2.Services;

namespace ERP_Core2.ScheduleEventsDomain
{

    public class ScheduleEventRepository: Repository<ScheduleEvent>
    {
        Entities _dbContext;
        public ScheduleEventRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
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
