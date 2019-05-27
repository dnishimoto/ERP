using ERP_Core2.AccountPayableDomain;
using ERP_Core2.CustomerLedgerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.GeneralLedgerDomain.Repository
{
    public interface ISupplierLedgerRepository
    {
         Task<CreateProcessStatus> CreateSupplierLedgerFromView(SupplierLedgerView view);
         Task<CreateProcessStatus> UpdateSupplierLedger(SupplierLedgerView supplierLedgerView);
        bool DeleteSupplierLedger(SupplierLedger SupplierLedger);
    }
}
