using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentInvoiceQuery
    {
        List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate);
        Task<IList<InvoiceView>> GetInvoiceViewsByCustomerId(long ?customerId, long? invoiceId = null);
        Task<InvoiceView> GetViewById(long ? invoiceId);
        Task<InvoiceView> GetViewByNumber(long invoiceNumber);
        Task<Invoice> GetEntityById(long ? invoiceId);
        Task<Invoice> GetEntityByNumber(long invoiceNumber);
        Task<InvoiceView> MapToView(Invoice inputObject);
        Task<IList<Invoice>> MapToEntity(IList<InvoiceView> inputObjects);
        Task<Invoice> MapToEntity(InvoiceView inputObject);
        Task<NextNumber> GetNextNumber();
    }
}
