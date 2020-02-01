using lssWebApi2.AutoMapper;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentInvoiceDetailQuery
{
    Task<InvoiceDetail> MapToEntity(InvoiceDetailView inputObject);
    Task<IList<InvoiceDetail>> MapToEntity(IList<InvoiceDetailView> inputObjects);
    Task<InvoiceDetailView> MapToView(InvoiceDetail inputObject);
    Task<NextNumber> GetNextNumber();
    Task<InvoiceDetail> GetEntityById(long ? invoiceDetailId);
    Task<InvoiceDetail> GetEntityByNumber(long invoiceDetailNumber);
    Task<InvoiceDetailView> GetViewById(long ? invoiceDetailId);
    Task<InvoiceDetailView> GetViewByNumber(long invoiceDetailNumber);
    Task<IList<InvoiceDetail>> GetEntitiesByInvoiceId(long? invoiceId);
}
