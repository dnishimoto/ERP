using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentAccountReceivableQuery
    {
        Task<IList<AccountReceivableFlatView>> GetOpenAccountReceivables();
        bool IsPaymentLate(long? invoiceId,DateTime asOfDate);
        bool HasLateFee(long? invoiceId);
        Task<IList<AccountReceivableView>> GetAccountReceivableViewsByCustomerId(long customerId);
        Task<AccountReceivable> GetEntityById(long? accountReceivableId);
        Task<AccountReceivableView> GetViewById(long? accountReceivableId);
  
    }
}
