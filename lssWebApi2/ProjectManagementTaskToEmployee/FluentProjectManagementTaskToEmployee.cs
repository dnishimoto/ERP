using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{

public class FluentProjectManagementTaskToEmployee :IFluentProjectManagementTaskToEmployee
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentProjectManagementTaskToEmployee() { }
        public IFluentProjectManagementTaskToEmployeeQuery Query()
        {
            return new FluentProjectManagementTaskToEmployeeQuery(unitOfWork) as IFluentProjectManagementTaskToEmployeeQuery;
        }
        public IFluentProjectManagementTaskToEmployee Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagementTaskToEmployee;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentProjectManagementTaskToEmployee AddProjectManagementTaskToEmployees(List<ProjectManagementTaskToEmployee> newObjects)
        {
            unitOfWork.projectManagementTaskToEmployeeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementTaskToEmployee;
        }
        public IFluentProjectManagementTaskToEmployee UpdateProjectManagementTaskToEmployees(IList<ProjectManagementTaskToEmployee> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.projectManagementTaskToEmployeeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementTaskToEmployee;
        }
        public IFluentProjectManagementTaskToEmployee AddProjectManagementTaskToEmployee(ProjectManagementTaskToEmployee newObject) {
            unitOfWork.projectManagementTaskToEmployeeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementTaskToEmployee;
        }
        public IFluentProjectManagementTaskToEmployee UpdateProjectManagementTaskToEmployee(ProjectManagementTaskToEmployee updateObject) {
            unitOfWork.projectManagementTaskToEmployeeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementTaskToEmployee;

        }
        public IFluentProjectManagementTaskToEmployee DeleteProjectManagementTaskToEmployee(ProjectManagementTaskToEmployee deleteObject) {
            unitOfWork.projectManagementTaskToEmployeeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementTaskToEmployee;
        }
   	public IFluentProjectManagementTaskToEmployee DeleteProjectManagementTaskToEmployees(List<ProjectManagementTaskToEmployee> deleteObjects)
        {
            unitOfWork.projectManagementTaskToEmployeeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementTaskToEmployee;
        }
    }
}
