using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.InventoryDomain;
using lssWebApi2.SupplierLedgerDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.SupplierInvoiceDetailDomain;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.ChartOfAccountsDomain;

namespace lssWebApi2.AccountPayableDomain
{
    public class AccountPayableModule : AbstractModule, IAccountPayableModule
    {
        public FluentAccountPayable AccountPayable = new FluentAccountPayable();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentPackingSlipDetail PackingSlipDetail = new FluentPackingSlipDetail();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentPurchaseOrderDetail PurchaseOrderDetail = new FluentPurchaseOrderDetail();
        public FluentSupplierLedger SupplierLedger = new FluentSupplierLedger();
        public FluentSupplierInvoice SupplierInvoice = new FluentSupplierInvoice();
        public FluentSupplierInvoiceDetail SupplierInvoiceDetail = new FluentSupplierInvoiceDetail();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentInventory Inventory = new FluentInventory();

        public bool CreateByPurchaseOrderView(PurchaseOrderView purchaseOrderView)

        {

            try

            {

                PurchaseOrder

                    .CreatePurchaseOrderByView(purchaseOrderView)

                    .Apply();
                PurchaseOrderDetail

                    .CreatePurchaseOrderDetailsByView(purchaseOrderView)

                    .Apply();
                AccountPayable

                    .CreateAcctPayByPurchaseOrderView(purchaseOrderView)

                    .Apply();

                return true;

            }

            catch (Exception ex) { throw new Exception("CreatePurchaseOrder", ex); }

        }

        public bool CreatePackingSlip(PackingSlipView packingSlipView)

        {

            try

            {

                PackingSlip.CreatePackingSlipByView(packingSlipView).Apply();

                PackingSlipDetail.CreatePackingSlipDetailsByView(packingSlipView).Apply();

                Inventory.CreateInventoryByPackingSlipView(packingSlipView).Apply();

                return true;

            }

            catch (Exception ex) { throw new Exception("CreatePackingSlip", ex); }

        }

        public bool CreateSupplierInvoice(SupplierInvoiceView supplierInvoiceView)

        {

            try

            {

                SupplierInvoice

                        .CreateSupplierInvoiceByView(supplierInvoiceView)

                        .Apply();

                 SupplierInvoiceDetail

                        .CreateSupplierInvoiceDetailsByView(supplierInvoiceView)

                        .Apply();

                return true;

            }

            catch (Exception ex) { throw new Exception("CreateSupplierInvoice", ex); }

        }

        public bool CreateAccountPayable(GeneralLedgerView ledgerView)

        {

            try

            {
                GeneralLedger.CreateGeneralLedgerByView(ledgerView).Apply();
                Task<GeneralLedgerView> generalLedgerViewTask =Task.Run(async()=> await GeneralLedger.Query().GetViewByDocNumber(ledgerView.DocNumber, ledgerView.DocType));
                Task.WaitAll(generalLedgerViewTask);
                SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerViewTask.Result);
                supplierLedgerView.GeneralLedgerId = generalLedgerViewTask.Result.GeneralLedgerId;
                SupplierLedger.CreateEntityByView(supplierLedgerView);
                SupplierLedger.Apply();
                SupplierLedger.CreateSupplierLedgerWithGeneralLedgerView(ledgerView).Apply();
                AccountPayable.UpdatePayableByLedgerView(ledgerView).Apply();
                GeneralLedger.UpdateAccountBalances(ledgerView);
              return true;

            }

            catch (Exception ex) { throw new Exception("CreateAccountPayable", ex); }

        }

    }
}
