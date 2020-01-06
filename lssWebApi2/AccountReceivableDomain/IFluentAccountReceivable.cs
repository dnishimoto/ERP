using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentAccountReceivable
    {
        IFluentAccountReceivable CreateAcctRecByInvoiceView(InvoiceView invoiceView);
        IFluentAccountReceivable Apply();
        IFluentAccountReceivable UpdateAccountReceivableByGeneralLedgerView(GeneralLedgerView ledgerView);
        IFluentAccountReceivable AdjustOpenAmount(AccountReceivableFlatView view);
        IFluentAccountReceivableQuery Query();
        IFluentAccountReceivable AddAccountReceivablesByList(List<AccountReceivable> newObjects);
        IFluentAccountReceivable UpdateAccountReceivablesByList(IList<AccountReceivable> newObjects);
        IFluentAccountReceivable AddAccountReceivable(AccountReceivable newObject);
        IFluentAccountReceivable UpdateAccountReceivable(AccountReceivable updateObject);
        IFluentAccountReceivable DeleteAccountReceivable(AccountReceivable deleteObject);
        IFluentAccountReceivable DeleteAccountReceivablesByList(List<AccountReceivable> deleteObjects);
    
    }
}
