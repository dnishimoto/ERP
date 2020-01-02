using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.SupervisorDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentSupervisorQuery
{
    Task<Supervisor> MapToEntity(SupervisorView inputObject);
    Task<List<Supervisor>> MapToEntity(List<SupervisorView> inputObjects);
    Task<SupervisorView> MapToView(Supervisor inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Supervisor> GetEntityById(long ? supervisorId);
    Task<Supervisor> GetEntityByNumber(long supervisorNumber);
    Task<SupervisorView> GetViewById(long ? supervisorId);
    Task<SupervisorView> GetViewByNumber(long supervisorNumber);
}
