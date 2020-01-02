using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
{
    public interface IFluentProjectManagementProject
    {
        IFluentProjectManagementProjectQuery Query();
        IFluentProjectManagementProject AddProject(ProjectManagementProject newProject);
        IFluentProjectManagementProject DeleteProject(ProjectManagementProject deleteProject);
        IFluentProjectManagementProject UpdateProject(ProjectManagementProject updateProject);
        IFluentProjectManagementProject Apply();
    }
}
