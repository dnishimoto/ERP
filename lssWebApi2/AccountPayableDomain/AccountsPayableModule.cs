using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.AccountPayableDomain
{
    public enum CreateProcessStatus
    {
        Insert,
        Create,
        AlreadyExists,
        Update,
        Delete,
        Failed
    }


  
    public class AccountsPayableModule : AbstractModule
    {
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentSupplierLedger SupplierLedger = new FluentSupplierLedger();

    }
}
