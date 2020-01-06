

using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.SupplierLedgerDomain
{

    public interface IFluentSupplierLedger
    {
        IFluentSupplierLedgerQuery Query();
        IFluentSupplierLedger Apply();
        IFluentSupplierLedger AddSupplierLedger(SupplierLedger supplierLedger);
        IFluentSupplierLedger UpdateSupplierLedger(SupplierLedger supplierLedger);
        IFluentSupplierLedger DeleteSupplierLedger(SupplierLedger supplierLedger);
        IFluentSupplierLedger UpdateSupplierLedgers(IList<SupplierLedger> newObjects);
        IFluentSupplierLedger AddSupplierLedgers(List<SupplierLedger> newObjects);
        IFluentSupplierLedger DeleteSupplierLedgers(List<SupplierLedger> deleteObjects);
        IFluentSupplierLedger CreateSupplierLedgerWithGeneralLedgerView(GeneralLedgerView generalLedgerView);
        IFluentSupplierLedger UpdateSupplierLedgerWithGeneralLedger(GeneralLedgerView generalLedgerView);
         IFluentSupplierLedger CreateEntityByView(SupplierLedgerView view);
        IFluentSupplierLedger UpdateEntityByView(SupplierLedgerView supplierLedgerView);
        IFluentSupplierLedger UpdateBalanceByAccountId(long? accountId, int? fiscalYear, int? fiscalPeriod);
        IFluentSupplierLedger UpdateSupplierLedgerByGeneralLedgerView(GeneralLedgerView ledgerView);
    }
}
