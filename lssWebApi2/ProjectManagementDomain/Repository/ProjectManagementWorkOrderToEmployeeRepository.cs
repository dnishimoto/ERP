using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
{
    public class ProjectManagementWorkOrderToEmployeeRepository : Repository<ProjectManagementWorkOrderToEmployee>, IProjectManagementWorkOrderToEmployeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementWorkOrderToEmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IEnumerable<EmployeeView>> GetEmployeeByWorkOrderId(long workOrderId)
        {

            IEnumerable<EmployeeView> views = await (from employee in _dbContext.Employee
                                 join workOrder in _dbContext.ProjectManagementWorkOrderToEmployee
                                 on employee.EmployeeId equals workOrder.EmployeeId
                                 where workOrderId == workOrderId
                                 select new EmployeeView
                                 {
                                     EmployeeId = employee.EmployeeId,
                                     EmployeeName = employee.Address.Name,
                                     EmployeeTitle = employee.JobTitleXref.Value,
                                     EmployeeStatus = employee.EmploymentStatusXref.Value,
                                     JobCode = employee.JobTitleXref.KeyCode,
                                     }
                                ).ToListAsync<EmployeeView>();

            return views;

        }

    }
}
