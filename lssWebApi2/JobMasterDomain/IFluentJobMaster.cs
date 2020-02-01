

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.JobMasterDomain;

namespace lssWebApi2.JobMasterDomain
{ 

public interface IFluentJobMaster
    {
        IFluentJobMasterQuery Query();
        IFluentJobMaster Apply();
        IFluentJobMaster AddJobMaster(JobMaster jobMaster);
        IFluentJobMaster UpdateJobMaster(JobMaster jobMaster);
        IFluentJobMaster DeleteJobMaster(JobMaster jobMaster);
     	IFluentJobMaster UpdateJobMasters(List<JobMaster> newObjects);
        IFluentJobMaster AddJobMasters(List<JobMaster> newObjects);
        IFluentJobMaster DeleteJobMasters(List<JobMaster> deleteObjects);
    }
}
