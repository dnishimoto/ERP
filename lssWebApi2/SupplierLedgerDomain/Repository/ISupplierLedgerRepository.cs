

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.SupplierLedgerDomain
{
public interface ISupplierLedgerRepository
    {
        Task<SupplierLedger> GetEntityById(long ? supplierLedgerId);
        Task<SupplierLedger> GetEntityByView(SupplierLedgerView view);
        Task<SupplierLedger> GetEntityByDocNumber(long? docNumber, string docType);
    }
}
