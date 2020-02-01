

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.JobPhaseDomain;

namespace lssWebApi2.JobPhaseDomain
{ 

public interface IFluentJobPhase
    {
        IFluentJobPhaseQuery Query();
        IFluentJobPhase Apply();
        IFluentJobPhase AddJobPhase(JobPhase jobPhase);
        IFluentJobPhase UpdateJobPhase(JobPhase jobPhase);
        IFluentJobPhase DeleteJobPhase(JobPhase jobPhase);
     	IFluentJobPhase UpdateJobPhases(List<JobPhase> newObjects);
        IFluentJobPhase AddJobPhases(List<JobPhase> newObjects);
        IFluentJobPhase DeleteJobPhases(List<JobPhase> deleteObjects);
    }
}
