using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
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
        IGeneralLedger CreateGeneralLedger(GeneralLedgerView ledgerView);
        IGeneralLedger Apply();
        IGeneralLedger UpdateLedgerBalances();
        IGeneralLedger UpdateAccountBalances(GeneralLedgerView ledgerView);
    }
}
