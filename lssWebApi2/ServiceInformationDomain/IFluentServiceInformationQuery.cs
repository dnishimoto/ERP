using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.ServiceInformationDomain;

public interface IFluentServiceInformationQuery
{
    Task<ServiceInformation> MapToEntity(ServiceInformationView inputObject);
    Task<IList<ServiceInformation>> MapToEntity(IList<ServiceInformationView> inputObjects);
    Task<ServiceInformationView> MapToView(ServiceInformation inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ServiceInformation> GetEntityById(long? serviceInformationId);
    Task<ServiceInformation> GetEntityByNumber(long serviceInformationNumber);
    Task<ServiceInformationView> GetViewById(long? serviceInformationId);
    Task<ServiceInformationView> GetViewByNumber(long serviceInformationNumber);
}
