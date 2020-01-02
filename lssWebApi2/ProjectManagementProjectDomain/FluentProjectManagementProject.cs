using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
{
    public class FluentProjectManagementProject:IFluentProjectManagementProject
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public FluentProjectManagementProject()
        {

        }

        public IFluentProjectManagementProjectQuery Query()
        {
            return new FluentProjectManagementProjectQuery(unitOfWork) as IFluentProjectManagementProjectQuery;
        }
        public IFluentProjectManagementProject Apply()
        {

            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagementProject;
        }
       
        public IFluentProjectManagementProject AddProject(ProjectManagementProject newProject)
        {

            unitOfWork.projectManagementProjectRepository.AddObject(newProject);
            processStatus = CreateProcessStatus.Insert;

            return this as IFluentProjectManagementProject;
            
        }
        public IFluentProjectManagementProject DeleteProject(ProjectManagementProject deleteProject)
        {
            unitOfWork.projectManagementProjectRepository.DeleteObject(deleteProject);
            processStatus = CreateProcessStatus.Delete;

            return this as IFluentProjectManagementProject;
        }
      
       
        public IFluentProjectManagementProject UpdateProject(ProjectManagementProject updateProject)
        {
            unitOfWork.projectManagementProjectRepository.UpdateObject(updateProject);
            processStatus = CreateProcessStatus.Update;

            return this as IFluentProjectManagementProject;
        }
      
        
      
    }
}
