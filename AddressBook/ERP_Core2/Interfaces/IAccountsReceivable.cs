using MillenniumERP.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IAccountsReceivable
    {
        IAccountsReceivable CreateAcctRecFromInvoice(InvoiceView invoiceView);
        IAccountsReceivable Apply();
        IQuery Query();
    }
}
