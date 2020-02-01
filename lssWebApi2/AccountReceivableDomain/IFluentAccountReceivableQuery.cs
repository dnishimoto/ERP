using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AccountsReceivableDomain
{
    public interface IFluentAccountReceivableQuery
    {
        Task<IList<AccountReceivableFlatView>> GetOpenAccountReceivables();
        bool IsPaymentLate(long? invoiceId,DateTime asOfDate);
        bool HasLateFee(long? invoiceId);
        Task<IList<AccountReceivableView>> GetAccountReceivableViewsByCustomerId(long customerId);
        Task<AccountReceivable> GetEntityById(long? accountReceivableId);
        Task<AccountReceivableView> GetViewById(long? accountReceivableId);
        Task<NextNumber> GetNextNumber();
        Task<NextNumber> GetDocNumber();
        Task<AccountReceivableView> MapToView(AccountReceivable inputObject);
        Task<AccountReceivable> MapToEntity(AccountReceivableView inputObject);
        Task<IList<AccountReceivable>> MapToEntity(IList<AccountReceivableView> inputObjects);
        Task<AccountReceivable> GetEntityByPurchaseOrderId(long? purchaseOrderId);
   }
}
