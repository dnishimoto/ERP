using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{

public class FluentProjectManagementWorkOrder :IFluentProjectManagementWorkOrder
    {
 private UnitOfWork unitOfWork ;
        private CreateProcessStatus processStatus;

        public FluentProjectManagementWorkOrder(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentProjectManagementWorkOrderQuery Query()
        {
            return new FluentProjectManagementWorkOrderQuery(unitOfWork) as IFluentProjectManagementWorkOrderQuery;
        }
        public IFluentProjectManagementWorkOrder Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentProjectManagementWorkOrder;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentProjectManagementWorkOrder AddProjectManagementWorkOrders(List<ProjectManagementWorkOrder> newObjects)
        {
            unitOfWork.projectManagementWorkOrderRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementWorkOrder;
        }
        public IFluentProjectManagementWorkOrder UpdateProjectManagementWorkOrders(IList<ProjectManagementWorkOrder> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.projectManagementWorkOrderRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementWorkOrder;
        }
        public IFluentProjectManagementWorkOrder AddProjectManagementWorkOrder(ProjectManagementWorkOrder newObject) {
            unitOfWork.projectManagementWorkOrderRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentProjectManagementWorkOrder;
        }
        public IFluentProjectManagementWorkOrder UpdateProjectManagementWorkOrder(ProjectManagementWorkOrder updateObject) {
            unitOfWork.projectManagementWorkOrderRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentProjectManagementWorkOrder;

        }
        public IFluentProjectManagementWorkOrder DeleteProjectManagementWorkOrder(ProjectManagementWorkOrder deleteObject) {
            unitOfWork.projectManagementWorkOrderRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementWorkOrder;
        }
   	public IFluentProjectManagementWorkOrder DeleteProjectManagementWorkOrders(List<ProjectManagementWorkOrder> deleteObjects)
        {
            unitOfWork.projectManagementWorkOrderRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentProjectManagementWorkOrder;
        }
    }
}
