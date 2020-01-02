using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{

public class FluentProjectManagementWorkOrderToEmployee :IFluentProjectManagementWorkOrderToEmployee
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentProjectManagementWorkOrderToEmployee() { }
        public IFluentProjectManagementWorkOrderToEmployeeQuery Query()
        {
            return new FluentProjectManagementWorkOrderToEmployeeQuery(unitOfWork) as IFluentProjectManagementWorkOrderToEmployeeQuery;
        }
        public IFluentProjectManagementWorkOrderToEmployee Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagementWorkOrderToEmployee;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        
        public IFluentProjectManagementWorkOrderToEmployee UpdateProjectManagementWorkOrderToEmployees(List<ProjectManagementWorkOrderToEmployee> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.projectManagementWorkOrderToEmployeeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementWorkOrderToEmployee;
        }
        public IFluentProjectManagementWorkOrderToEmployee AddProjectManagementWorkOrderToEmployee(ProjectManagementWorkOrderToEmployee newObject) {
            unitOfWork.projectManagementWorkOrderToEmployeeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementWorkOrderToEmployee;
        }
        public IFluentProjectManagementWorkOrderToEmployee AddProjectManagementWorkOrderToEmployees(List<ProjectManagementWorkOrderToEmployee> newObjects)
        {
            unitOfWork.projectManagementWorkOrderToEmployeeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementWorkOrderToEmployee;
        }

        public IFluentProjectManagementWorkOrderToEmployee UpdateProjectManagementWorkOrderToEmployee(ProjectManagementWorkOrderToEmployee updateObject) {
            unitOfWork.projectManagementWorkOrderToEmployeeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementWorkOrderToEmployee;

        }
        public IFluentProjectManagementWorkOrderToEmployee DeleteProjectManagementWorkOrderToEmployee(ProjectManagementWorkOrderToEmployee deleteObject) {
            unitOfWork.projectManagementWorkOrderToEmployeeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementWorkOrderToEmployee;
        }
   	public IFluentProjectManagementWorkOrderToEmployee DeleteProjectManagementWorkOrderToEmployees(List<ProjectManagementWorkOrderToEmployee> deleteObjects)
        {
            unitOfWork.projectManagementWorkOrderToEmployeeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementWorkOrderToEmployee;
        }
    }
}
