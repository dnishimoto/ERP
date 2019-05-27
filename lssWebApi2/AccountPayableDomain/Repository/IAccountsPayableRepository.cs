using ERP_Core2.AccountPayableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.PurchaseOrderDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AccountPayableDomain.Repository
{
    public interface IAccountsPayableRepository
    {
        Task<AcctPay> GetAcctPayByPONumber(string poNumber);
        Task<CreateProcessStatus> CreateAcctPayByPurchaseOrderView(PurchaseOrderView poView);
         Task<AcctPay> GetAcctPayableByDocNumber(long docNumber);
        Task<CreateProcessStatus> UpdatePayableByLedgerView(GeneralLedgerView ledgerView);
        Task<CreateProcessStatus> UpdateAcct(AcctPay acctPay);
         CreateProcessStatus DeleteAcctRec(AcctPay acctPay);
    }
}
