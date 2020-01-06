using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ProjectManagementTaskDomain
{

public class FluentProjectManagementTask :IFluentProjectManagementTask
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentProjectManagementTask() { }
        public IFluentProjectManagementTaskQuery Query()
        {
            return new FluentProjectManagementTaskQuery(unitOfWork) as IFluentProjectManagementTaskQuery;
        }
        public IFluentProjectManagementTask Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagementTask;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentProjectManagementTask AddProjectManagementTasks(List<ProjectManagementTask> newObjects)
        {
            unitOfWork.projectManagementTaskRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementTask;
        }
        public IFluentProjectManagementTask UpdateProjectManagementTasks(IList<ProjectManagementTask> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.projectManagementTaskRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementTask;
        }
        public IFluentProjectManagementTask AddProjectManagementTask(ProjectManagementTask newObject) {
            unitOfWork.projectManagementTaskRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementTask;
        }
        public IFluentProjectManagementTask UpdateProjectManagementTask(ProjectManagementTask updateObject) {
            unitOfWork.projectManagementTaskRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementTask;

        }
        public IFluentProjectManagementTask DeleteProjectManagementTask(ProjectManagementTask deleteObject) {
            unitOfWork.projectManagementTaskRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementTask;
        }
   	public IFluentProjectManagementTask DeleteProjectManagementTasks(List<ProjectManagementTask> deleteObjects)
        {
            unitOfWork.projectManagementTaskRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementTask;
        }
    }
}
