using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountsPayableDomain;
using ERP_Core2.PurchaseOrderDomain;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.PurchaseOrderDomain.PurchaseOrderRepository;
using ERP_Core2.SupplierInvoicesDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.FluentAPI;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.CustomerLedgerDomain;

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
