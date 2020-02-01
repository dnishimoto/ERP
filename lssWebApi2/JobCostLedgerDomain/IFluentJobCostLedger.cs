

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.JobCostLedgerDomain;

namespace lssWebApi2.JobCostLedgerDomain
{ 

public interface IFluentJobCostLedger
    {
        IFluentJobCostLedgerQuery Query();
        IFluentJobCostLedger Apply();
        IFluentJobCostLedger AddJobCostLedger(JobCostLedger jobCostLedger);
        IFluentJobCostLedger UpdateJobCostLedger(JobCostLedger jobCostLedger);
        IFluentJobCostLedger DeleteJobCostLedger(JobCostLedger jobCostLedger);
     	IFluentJobCostLedger UpdateJobCostLedgers(List<JobCostLedger> newObjects);
        IFluentJobCostLedger AddJobCostLedgers(List<JobCostLedger> newObjects);
        IFluentJobCostLedger DeleteJobCostLedgers(List<JobCostLedger> deleteObjects);
    }
}
