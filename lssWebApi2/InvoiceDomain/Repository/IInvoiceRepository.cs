using ERP_Core2.AccountPayableDomain;
using ERP_Core2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InvoiceDomain.Repository
{
    public interface IInvoiceRepository
    {
        List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate);
        Task<Invoice> GetInvoiceByInvoiceNumber(string invoiceNumber);
        Task<CreateProcessStatus> CreateInvoiceByView(InvoiceView invoiceView);
        Task<bool> AddInvoice(Invoice invoice);
        Task<bool> UpdateInvoice(Invoice invoice);
         bool DeleteInvoice(Invoice invoice);
    }
}
