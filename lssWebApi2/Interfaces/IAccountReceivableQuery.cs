using ERP_Core2.AccountsReceivableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IQueryAccountReceivable
    {
        List<AccountReceivableFlatView> GetOpenAccountReceivables();
        bool IsPaymentLate(long? invoiceId,DateTime asOfDate);
        bool HasLateFee(long? invoiceId);
    }
}
