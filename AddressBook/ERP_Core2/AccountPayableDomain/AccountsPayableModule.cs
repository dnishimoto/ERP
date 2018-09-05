using ERP_Core2.AbstractFactory;
using MillenniumERP.AccountsPayableDomain;
using MillenniumERP.PurchaseOrderDomain;
using MillenniumERP.PackingSlipDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MillenniumERP.PurchaseOrderDomain.PurchaseOrderRepository;
using MillenniumERP.SupplierInvoicesDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.FluentAPI;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.CustomerLedgerDomain;
using ERP_Core2.EntityFramework;

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
