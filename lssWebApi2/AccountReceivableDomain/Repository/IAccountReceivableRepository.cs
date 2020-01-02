using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableDomain.Repository
{
    public interface IAccountReceivableRepository
    {
        IQueryable<AccountReceivable> GetQueryableByCustomerId(long? customerId);
        bool HasLateFee(long? acctRecId);
        bool IsPaymentLate(long? invoiceId, DateTime asOfDate);
        Task<IList<AccountReceivableFlatView>> GetOpenAcctRec();
        Task<AccountReceivable> GetAcctRecByDocNumber(long docNumber);
        Task<AccountReceivable> GetEntityByInvoiceId(long? invoiceId);
        Task<AccountReceivable> GetEntityById(long? accountReceivableId);
    }
}
