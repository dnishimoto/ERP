

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.AccountPayableDomain
{
public interface IAccountPayableRepository
    {
        Task<AccountPayable> GetEntityById(long ? accountPayableId);
        Task<AccountPayable> GetAcctPayableByDocNumber(long docNumber);
        Task<AccountPayable> GetEntityByInvoiceId(long? docNumber);
        Task<AccountPayable> GetAcctPayByPONumber(string poNumber);
        Task<AccountPayable> GetEntityByGeneralLedger(GeneralLedgerView ledgerView);
        Task<AccountPayable> GetEntityByPurchaseOrderView(PurchaseOrderView poView);
  }
}
