using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.SupplierInvoiceDetailDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentSupplierInvoiceDetailQuery
{
    Task<SupplierInvoiceDetail> MapToEntity(SupplierInvoiceDetailView inputObject);
    Task<IList<SupplierInvoiceDetail>> MapToEntity(IList<SupplierInvoiceDetailView> inputObjects);
    Task<SupplierInvoiceDetailView> MapToView(SupplierInvoiceDetail inputObject);
    Task<NextNumber> GetNextNumber();
    Task<SupplierInvoiceDetail> GetEntityById(long ? supplierInvoiceDetailId);
    Task<SupplierInvoiceDetail> GetEntityByNumber(long supplierInvoiceDetailNumber);
    Task<SupplierInvoiceDetailView> GetViewById(long ? supplierInvoiceDetailId);
    Task<SupplierInvoiceDetailView> GetViewByNumber(long supplierInvoiceDetailNumber);
}
