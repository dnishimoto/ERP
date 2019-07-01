using ERP_Core2.AbstractFactory;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.FluentAPI;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.PurchaseOrderDomain;
using ERP_Core2.Services;
using ERP_Core2.SupplierInvoicesDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.FluentAPI;
using System;
using System.Threading.Tasks;

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

   
   
   
   

  
    public class AccountPayableModule : AbstractModule
    {
        public FluentAccountPayable AccountPayable = new FluentAccountPayable();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentSupplierLedger SupplierLedger = new FluentSupplierLedger();

        public bool CreatePurchaseOrder(PurchaseOrderView purchaseOrderView)
        {
            try
            {
                PurchaseOrder
                    .CreatePurchaseOrder(purchaseOrderView)
                    .Apply()
                    .CreatePurchaseOrderDetails(purchaseOrderView)
                    .Apply()
                    .CreateAcctPayByPurchaseOrderNumber(purchaseOrderView)
                    .Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception("CreatePurchaseOrder", ex); }
        }
        public bool CreatePackingSlip(PackingSlipView packingSlipView)
        {
            try
            {
                PackingSlip.CreatePackingSlip(packingSlipView).Apply()
                        .CreatePackingSlipDetails(packingSlipView)
                        .Apply()
                        .CreateInventoryByPackingSlip(packingSlipView)
                        .Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception("CreatePackingSlip", ex); }
        }
        public bool CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView)
        {
            try
            {
                SupplierLedger
                        .CreateSupplierInvoice(supplierInvoiceView)
                        .Apply()
                        .CreateSupplierInvoiceDetail(supplierInvoiceView)
                        .Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateSupplierInvoice", ex); }
        }
        public bool CreateAccountPayable(GeneralLedgerView ledgerView)
        {
            try
            {

                Supplier
                   .GeneralLedger.CreateGeneralLedger(ledgerView).Apply();


                Supplier
                 .CreateSupplierLedger(ledgerView)
                 .Apply();

                Supplier
                      .UpdateAccountsPayable(ledgerView)
                             .Apply();

                Supplier
                        .GeneralLedger
                            .UpdateAccountBalances(ledgerView);
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateAccountPayable", ex); }
        }
    }
}
