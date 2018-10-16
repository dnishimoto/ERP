using ERP_Core2.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IInvoiceQuery
    {
        List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate);

    }
}
