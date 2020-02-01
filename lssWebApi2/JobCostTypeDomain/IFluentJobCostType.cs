

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.JobCostTypeDomain;

namespace lssWebApi2.JobCostTypeDomain
{ 

public interface IFluentJobCostType
    {
        IFluentJobCostTypeQuery Query();
        IFluentJobCostType Apply();
        IFluentJobCostType AddJobCostType(JobCostType jobCostType);
        IFluentJobCostType UpdateJobCostType(JobCostType jobCostType);
        IFluentJobCostType DeleteJobCostType(JobCostType jobCostType);
     	IFluentJobCostType UpdateJobCostTypes(List<JobCostType> newObjects);
        IFluentJobCostType AddJobCostTypes(List<JobCostType> newObjects);
        IFluentJobCostType DeleteJobCostTypes(List<JobCostType> deleteObjects);
    }
}
