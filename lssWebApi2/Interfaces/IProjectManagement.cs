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
        IFluentProjectManagement DeleteProject(ProjectManagementProject deleteProject);
        IFluentProjectManagement UpdateProject(ProjectManagementProject updateProject);
        IFluentProjectManagement AddWorkOrder(ProjectManagementWorkOrder newWorkOrder);
        IFluentProjectManagement UpdateWorkOrder(ProjectManagementWorkOrder updateWorkOrder);
        IFluentProjectManagement DeleteWorkOrder(ProjectManagementWorkOrder deleteWorkOrder);
        IFluentProjectManagement Apply();
    }
}
