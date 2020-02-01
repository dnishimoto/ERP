using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.JobPhaseDomain
{
    public interface IFluentJobPhaseQuery
    {
        Task<JobPhase> MapToEntity(JobPhaseView inputObject);
        Task<IList<JobPhase>> MapToEntity(IList<JobPhaseView> inputObjects);
        Task<JobPhaseView> MapToView(JobPhase inputObject);
        Task<NextNumber> GetNextNumber();
        Task<JobPhase> GetEntityById(long? jobPhaseId);
        Task<JobPhase> GetEntityByNumber(long jobPhaseNumber);
        Task<JobPhaseView> GetViewById(long? jobPhaseId);
        Task<JobPhaseView> GetViewByNumber(long jobPhaseNumber);
        Task<IList<JobPhase>> GetEntitiesByJobMasterId(long? jobMasterId);
        Task<JobPhase> GetEntityByJobIdAndPhase(long? jobMasterId, string phase);
    }
}
