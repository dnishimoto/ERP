using ERP_Core2.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentInvoiceDetail
    {

        IFluentInvoiceDetail CreateInvoiceDetails(InvoiceView invoiceView);
        IFluentInvoiceDetail Apply();
        IQuery Query();

    }
}
