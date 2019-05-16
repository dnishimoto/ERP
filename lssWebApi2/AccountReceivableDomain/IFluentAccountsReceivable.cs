using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentAccountsReceivable
    {
        IFluentAccountsReceivable CreateAcctRecFromInvoice(InvoiceView invoiceView);
        IFluentAccountsReceivable Apply();
        IFluentAccountsReceivable UpdateAccountReceivable(GeneralLedgerView ledgerView);
        IFluentAccountsReceivable AdjustOpenAmount(AccountReceivableFlatView view);
        IFluentAccountsReceivable CreateLateFee(AccountReceivableFlatView view);
        IFluentAccountsReceivableQuery Query();
    }
}
