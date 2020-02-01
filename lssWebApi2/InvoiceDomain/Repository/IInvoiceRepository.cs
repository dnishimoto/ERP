using lssWebApi2.AccountPayableDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.InvoiceDomain.Repository
{
    public interface IInvoiceRepository
    {
        List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate);
        Task<Invoice> GetEntityByInvoiceDocument(string invoiceNumber);
        Task<bool> AddInvoice(Invoice invoice);
        Task<bool> UpdateInvoice(Invoice invoice);
         bool DeleteInvoice(Invoice invoice);
        Task<Invoice> FindEntityByInvoiceDocument(string invoiceNumber);
        Task<Invoice> GetEntityById(long ? invoiceId);
        Task<Invoice> GetEntityByNumber(long? invoiceNumber);
        Task<Invoice> FindEntityByExpression(Expression<Func<Invoice, bool>> predicate);
        Task<IList<Invoice>> GetEntitiesByExpression(Expression<Func<Invoice, bool>> predicate);
        Task<IList<Invoice>> GetEntitiesByPurchaseOrderId(long? purchaseOrderId);
        Task<Decimal> GetInvoicedAmountByPurchaseOrderId(long? purchaseOrderId);
    }
}
