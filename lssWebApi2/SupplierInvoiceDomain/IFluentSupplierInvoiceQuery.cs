using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentSupplierInvoiceQuery
{
    Task<SupplierInvoice> MapToEntity(SupplierInvoiceView inputObject);
    Task<List<SupplierInvoice>> MapToEntity(List<SupplierInvoiceView> inputObjects);
    Task<SupplierInvoiceView> MapToView(SupplierInvoice inputObject);
    Task<NextNumber> GetNextNumber();
    Task<SupplierInvoice> GetEntityById(long ? supplierInvoiceId);
    Task<SupplierInvoice> GetEntityByNumber(long supplierInvoiceNumber);
    Task<SupplierInvoiceView> GetViewById(long ? supplierInvoiceId);
    Task<SupplierInvoiceView> GetViewByNumber(long supplierInvoiceNumber);
    Task<SupplierInvoice> GetEntityByPONumber(string poNumber);
}
