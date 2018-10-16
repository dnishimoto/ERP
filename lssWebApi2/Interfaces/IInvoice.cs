using ERP_Core2.InvoicesDomain;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IInvoice
    {
        IInvoice CreateInvoice(InvoiceView invoiceView);
        IInvoice MergeWithInvoiceNumber(ref InvoiceView invoiceView);
        IInvoice Apply();

        IInvoiceQuery Query();
    }
}
