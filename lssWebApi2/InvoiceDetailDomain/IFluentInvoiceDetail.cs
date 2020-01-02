

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.InvoicesDomain;

namespace lssWebApi2.InvoiceDetailDomain
{ 

public interface IFluentInvoiceDetail
    {
        IFluentInvoiceDetailQuery Query();
        IFluentInvoiceDetail Apply();
        IFluentInvoiceDetail AddInvoiceDetail(InvoiceDetail invoiceDetail);
        IFluentInvoiceDetail UpdateInvoiceDetail(InvoiceDetail invoiceDetail);
        IFluentInvoiceDetail DeleteInvoiceDetail(InvoiceDetail invoiceDetail);
     	IFluentInvoiceDetail UpdateInvoiceDetails(List<InvoiceDetail> newObjects);
        IFluentInvoiceDetail AddInvoiceDetails(List<InvoiceDetail> newObjects);
        IFluentInvoiceDetail DeleteInvoiceDetails(List<InvoiceDetail> deleteObjects);
        IFluentInvoiceDetail CreateInvoiceDetailsByInvoiceView(InvoiceView invoiceView);
    }
}
