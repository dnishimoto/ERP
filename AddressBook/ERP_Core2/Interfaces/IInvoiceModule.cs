using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IInvoiceModule
    {
        IInvoice Invoice();
        IInvoiceDetail InvoiceDetail();
        IAccountsReceivable AccountsReceivable();
        IGeneralLedger GeneralLedger();
    }
}
