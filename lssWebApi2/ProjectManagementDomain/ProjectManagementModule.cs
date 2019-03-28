using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.FluentAPI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.ProjectManagementDomain
{
    public class ProjectManagementModule : AbstractModule
    {
        public FluentProjectManagement ProjectManagement = new FluentProjectManagement();
    }
}
