using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.JobCostLedgerDomain
{
  public interface IFluentJobCostLedgerQuery
  {
     Task<JobCostLedger> MapToEntity(JobCostLedgerView inputObject);
     Task<IList<JobCostLedger>> MapToEntity(IList<JobCostLedgerView> inputObjects);
     Task<JobCostLedgerView> MapToView(JobCostLedger inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<JobCostLedger> GetEntityById(long ? jobCostLedgerId);
	 Task<JobCostLedger> GetEntityByNumber(long jobCostLedgerNumber);
	 Task<JobCostLedgerView> GetViewById(long ? jobCostLedgerId);
	 Task<JobCostLedgerView> GetViewByNumber(long jobCostLedgerNumber);
  }
}
