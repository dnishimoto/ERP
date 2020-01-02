using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{

    public interface IFluentGeneralLedger
    {

        IFluentGeneralLedger CreateGeneralLedgerByInvoiceView(InvoiceView invoiceView);
        IFluentGeneralLedger CreateGeneralLedgerByView(GeneralLedgerView ledgerView);
        IFluentGeneralLedger Apply();
        IFluentGeneralLedger UpdateLedgerBalances();
        IFluentGeneralLedger UpdateAccountBalances(GeneralLedgerView ledgerView);
        IFluentGeneralLedgerQuery Query();
        IFluentGeneralLedger AddGeneralLedgers(List<GeneralLedger> newObjects);
        IFluentGeneralLedger UpdateGeneralLedgers(List<GeneralLedger> newObjects);
        IFluentGeneralLedger AddGeneralLedger(GeneralLedger newObject);
        IFluentGeneralLedger UpdateGeneralLedger(GeneralLedger updateObject);
        IFluentGeneralLedger DeleteGeneralLedger(GeneralLedger deleteObject);
        IFluentGeneralLedger DeleteGeneralLedgers(List<GeneralLedger> deleteObjects);
   

    }
}
