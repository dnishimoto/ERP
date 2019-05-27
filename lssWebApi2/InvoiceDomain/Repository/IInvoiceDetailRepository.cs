using ERP_Core2.AccountPayableDomain;
using ERP_Core2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InvoiceDomain.Repository
{
    public interface IInvoiceDetailRepository
    {
        Task<CreateProcessStatus> CreateInvoiceDetailsByView(InvoiceView invoiceView);
        Task<bool> AddInvoiceDetail(InvoiceDetail invoiceDetail);
        Task<bool> UpdateInvoiceDetail(InvoiceDetail invoiceDetail);
        bool DeleteInvoiceDetail(InvoiceDetail invoiceDetail);
    }
}
