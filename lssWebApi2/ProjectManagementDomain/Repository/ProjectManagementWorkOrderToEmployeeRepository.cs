using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain.Repository
{
    public class ProjectManagementWorkOrderToEmployeeRepository : Repository<ProjectManagementWorkOrderToEmployee>
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementWorkOrderToEmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

    }
}
