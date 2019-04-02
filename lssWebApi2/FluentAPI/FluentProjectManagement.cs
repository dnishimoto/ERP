using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentProjectManagement:IFluentProjectManagement
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public FluentProjectManagement()
        {

        }

        public IFluentProjectManagementQuery Query()
        {
            return new FluentProjectManagementQuery(unitOfWork) as IFluentProjectManagementQuery;
        }
        public IFluentProjectManagement Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagement;
        }
        public IFluentProjectManagement AddProject(ProjectManagementProject newProject)
        {

            unitOfWork.projectManagementProjectRepository.AddObject(newProject);
            processStatus = CreateProcessStatus.Insert;

            return this as IFluentProjectManagement;
            
        }
        public IFluentProjectManagement DeleteProject(ProjectManagementProject deleteProject)
        {
            unitOfWork.projectManagementProjectRepository.DeleteObject(deleteProject);
            processStatus = CreateProcessStatus.Delete;

            return this as IFluentProjectManagement;
        }

        public IFluentProjectManagement UpdateProject(ProjectManagementProject updateProject)
        {
            unitOfWork.projectManagementProjectRepository.UpdateObject(updateProject);
            processStatus = CreateProcessStatus.Update;

            return this as IFluentProjectManagement;
        }
        public IFluentProjectManagement AddWorkOrder(ProjectManagementWorkOrder newWorkOrder)
        {
            unitOfWork.projectManagementWorkOrderRepository.AddObject(newWorkOrder);
            processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagement;
        }
        public IFluentProjectManagement DeleteWorkOrder(ProjectManagementWorkOrder deleteWorkOrder)
        {
            unitOfWork.projectManagementWorkOrderRepository.DeleteObject(deleteWorkOrder);
            processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagement;
        }
        public IFluentProjectManagement UpdateWorkOrder(ProjectManagementWorkOrder updateWorkOrder)
        {
            unitOfWork.projectManagementWorkOrderRepository.UpdateObject(updateWorkOrder);
            processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagement;
        }
    }
}
