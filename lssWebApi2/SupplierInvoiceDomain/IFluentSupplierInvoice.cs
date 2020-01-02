

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.SupplierInvoiceDomain
{ 

public interface IFluentSupplierInvoice
    {
        IFluentSupplierInvoiceQuery Query();
        IFluentSupplierInvoice Apply();
        IFluentSupplierInvoice AddSupplierInvoice(SupplierInvoice supplierInvoice);
        IFluentSupplierInvoice UpdateSupplierInvoice(SupplierInvoice supplierInvoice);
        IFluentSupplierInvoice DeleteSupplierInvoice(SupplierInvoice supplierInvoice);
     	IFluentSupplierInvoice UpdateSupplierInvoices(List<SupplierInvoice> newObjects);
        IFluentSupplierInvoice AddSupplierInvoices(List<SupplierInvoice> newObjects);
        IFluentSupplierInvoice DeleteSupplierInvoices(List<SupplierInvoice> deleteObjects);
    }
}
