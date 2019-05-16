using ERP_Core2.InvoicesDomain;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentInvoice
    {
        IFluentInvoice CreateInvoice(InvoiceView invoiceView);
        IFluentInvoice MergeWithInvoiceNumber(ref InvoiceView invoiceView);
        IFluentInvoice Apply();

        IFluentInvoiceQuery Query();
    }
}
