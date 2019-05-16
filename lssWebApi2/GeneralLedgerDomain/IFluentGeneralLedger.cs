using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
 
    public interface IFluentGeneralLedger
    {

        IFluentGeneralLedger CreateGeneralLedger(InvoiceView invoiceView);
        IFluentGeneralLedger CreateGeneralLedger(GeneralLedgerView ledgerView);
        IFluentGeneralLedger Apply();
        IFluentGeneralLedger UpdateLedgerBalances();
        IFluentGeneralLedger UpdateAccountBalances(GeneralLedgerView ledgerView);
        IFluentGeneralLedgerQuery Query();
    }
}
