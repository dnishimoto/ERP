using MillenniumERP.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IGeneralLedger
    {

        IGeneralLedger CreateGeneralLedger(InvoiceView invoiceView);
        IGeneralLedger Apply();
        IGeneralLedger UpdateLedgerBalances();
    }
}
