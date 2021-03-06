using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;

namespace lssWebApi2.EmployeeDomain
{

public class FluentEmployee :IFluentEmployee
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentEmployee(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentEmployeeQuery Query()
        {
            return new FluentEmployeeQuery(unitOfWork) as IFluentEmployeeQuery;
        }
        public IFluentEmployee Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentEmployee;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentEmployee AddEmployees(List<Employee> newObjects)
        {
            unitOfWork.employeeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentEmployee;
        }
        public IFluentEmployee UpdateEmployees(IList<Employee> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.employeeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentEmployee;
        }
        public IFluentEmployee AddEmployee(Employee newObject) {
            unitOfWork.employeeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentEmployee;
        }
        public IFluentEmployee UpdateEmployee(Employee updateObject) {
            unitOfWork.employeeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentEmployee;

        }
        public IFluentEmployee DeleteEmployee(Employee deleteObject) {
            unitOfWork.employeeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentEmployee;
        }
   	public IFluentEmployee DeleteEmployees(List<Employee> deleteObjects)
        {
            unitOfWork.employeeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentEmployee;
        }
    }
}
