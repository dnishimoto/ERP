using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.JobCostTypeDomain
{
  public interface IFluentJobCostTypeQuery
  {
     Task<JobCostType> MapToEntity(JobCostTypeView inputObject);
     Task<IList<JobCostType>> MapToEntity(IList<JobCostTypeView> inputObjects);
     Task<JobCostTypeView> MapToView(JobCostType inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<JobCostType> GetEntityById(long ? jobCostTypeId);
	 Task<JobCostType> GetEntityByNumber(long jobCostTypeNumber);
	 Task<JobCostTypeView> GetViewById(long ? jobCostTypeId);
	 Task<JobCostTypeView> GetViewByNumber(long jobCostTypeNumber);
  }
}
