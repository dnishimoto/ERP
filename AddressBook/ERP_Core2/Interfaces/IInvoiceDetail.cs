using ERP_Core2.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IInvoiceDetail
    {

        IInvoiceDetail CreateInvoiceDetails(InvoiceView invoiceView);
        IInvoiceDetail Apply();
        IQuery Query();

    }
}
