using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableDomain.Repository
{
    public interface IAccountReceivableRepository
    {
        bool HasLateFee(long? acctRecId);
        Task<CreateProcessStatus> AdjustOpenAmount(AccountReceivableFlatView view);
        Task<CreateProcessStatus> CreateLateFee(AccountReceivableFlatView view);
        bool IsPaymentLate(long? invoiceId, DateTime asOfDate);
        List<AccountReceivableFlatView> GetOpenAcctRec();
        Task<AcctRec> GetAcctRecByDocNumber(long docNumber);
        Task<CreateProcessStatus> UpdateReceivableByCashLedger(GeneralLedgerView ledgerView);
        Task<AccountReceiveableView> GetAccountReceivableViewByInvoiceId(long? invoiceId);
        Task<CreateProcessStatus> CreateAcctRecFromInvoice(InvoiceView invoiceView);
        Task<CreateProcessStatus> UpdateAcct(AcctRec acctRec);

    }
}
