

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.SupervisorDomain
{ 

public interface IFluentSupervisor
    {
        IFluentSupervisorQuery Query();
        IFluentSupervisor Apply();
        IFluentSupervisor AddSupervisor(Supervisor supervisor);
        IFluentSupervisor UpdateSupervisor(Supervisor supervisor);
        IFluentSupervisor DeleteSupervisor(Supervisor supervisor);
     	IFluentSupervisor UpdateSupervisors(List<Supervisor> newObjects);
        IFluentSupervisor AddSupervisors(List<Supervisor> newObjects);
        IFluentSupervisor DeleteSupervisors(List<Supervisor> deleteObjects);
    }
}
