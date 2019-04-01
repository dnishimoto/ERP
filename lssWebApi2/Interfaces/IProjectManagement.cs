using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentProjectManagement
    {
        IFluentProjectManagementQuery Query();
        IFluentProjectManagement AddProject(ProjectManagementProject newProject);
        IFluentProjectManagement AddWorkOrder(ProjectManagementWorkOrder newWorkOrder);
        IFluentProjectManagement Apply();
    }
}
