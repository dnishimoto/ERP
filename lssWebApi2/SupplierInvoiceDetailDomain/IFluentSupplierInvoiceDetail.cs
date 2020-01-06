

using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{ 

public interface IFluentSupplierInvoiceDetail
    {
        IFluentSupplierInvoiceDetailQuery Query();
        IFluentSupplierInvoiceDetail Apply();
        IFluentSupplierInvoiceDetail AddSupplierInvoiceDetail(SupplierInvoiceDetail supplierInvoiceDetail);
        IFluentSupplierInvoiceDetail UpdateSupplierInvoiceDetail(SupplierInvoiceDetail supplierInvoiceDetail);
        IFluentSupplierInvoiceDetail DeleteSupplierInvoiceDetail(SupplierInvoiceDetail supplierInvoiceDetail);
     	IFluentSupplierInvoiceDetail UpdateSupplierInvoiceDetails(IList<SupplierInvoiceDetail> newObjects);
        IFluentSupplierInvoiceDetail AddSupplierInvoiceDetails(List<SupplierInvoiceDetail> newObjects);
        IFluentSupplierInvoiceDetail DeleteSupplierInvoiceDetails(List<SupplierInvoiceDetail> deleteObjects);
        IFluentSupplierInvoiceDetail CreateSupplierInvoiceDetailsByView(SupplierInvoiceView view);
    }
}
