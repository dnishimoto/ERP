using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.ServiceInformationInvoiceDomain;

public interface IFluentServiceInformationInvoiceQuery
{
    Task<ServiceInformationInvoice> MapToEntity(ServiceInformationInvoiceView inputObject);
    Task<IList<ServiceInformationInvoice>> MapToEntity(IList<ServiceInformationInvoiceView> inputObjects);
    Task<ServiceInformationInvoiceView> MapToView(ServiceInformationInvoice inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ServiceInformationInvoice> GetEntityById(long? serviceInformationInvoiceId);
    Task<ServiceInformationInvoice> GetEntityByNumber(long serviceInformationInvoiceNumber);
    Task<ServiceInformationInvoiceView> GetViewById(long? serviceInformationInvoiceId);
    Task<ServiceInformationInvoiceView> GetViewByNumber(long serviceInformationInvoiceNumber);
    Task<IList<ServiceInformationInvoiceView>> GetViewsByServiceId(long? serviceId);
}
