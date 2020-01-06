

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ProjectManagementWorkOrderDomain;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{ 

public interface IFluentProjectManagementWorkOrder
    {
        IFluentProjectManagementWorkOrderQuery Query();
        IFluentProjectManagementWorkOrder Apply();
        IFluentProjectManagementWorkOrder AddProjectManagementWorkOrder(ProjectManagementWorkOrder projectManagementWorkOrder);
        IFluentProjectManagementWorkOrder UpdateProjectManagementWorkOrder(ProjectManagementWorkOrder projectManagementWorkOrder);
        IFluentProjectManagementWorkOrder DeleteProjectManagementWorkOrder(ProjectManagementWorkOrder projectManagementWorkOrder);
     	IFluentProjectManagementWorkOrder UpdateProjectManagementWorkOrders(IList<ProjectManagementWorkOrder> newObjects);
        IFluentProjectManagementWorkOrder AddProjectManagementWorkOrders(List<ProjectManagementWorkOrder> newObjects);
        IFluentProjectManagementWorkOrder DeleteProjectManagementWorkOrders(List<ProjectManagementWorkOrder> deleteObjects);
    }
}
