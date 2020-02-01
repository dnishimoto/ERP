using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.JobMasterDomain
{
  public interface IFluentJobMasterQuery
  {
     Task<JobMaster> MapToEntity(JobMasterView inputObject);
     Task<IList<JobMaster>> MapToEntity(IList<JobMasterView> inputObjects);
     Task<JobMasterView> MapToView(JobMaster inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<JobMaster> GetEntityById(long ? jobMasterId);
	 Task<JobMaster> GetEntityByNumber(long jobMasterNumber);
	 Task<JobMasterView> GetViewById(long ? jobMasterId);
	 Task<JobMasterView> GetViewByNumber(long jobMasterNumber);
  }
}
