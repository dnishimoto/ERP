using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.AccountPayableDomain
{
    public class AccountPayableModule : AbstractModule, IAccountPayableModule
    {
        private UnitOfWork unitOfWork;
        public FluentAccountPayable AccountPayable;
        public FluentSupplier Supplier;
        public FluentPackingSlip PackingSlip;
        public FluentPackingSlipDetail PackingSlipDetail;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentPurchaseOrderDetail PurchaseOrderDetail;
        public FluentSupplierLedger SupplierLedger;
        public FluentSupplierInvoice SupplierInvoice;
        public FluentSupplierInvoiceDetail SupplierInvoiceDetail;
        public FluentGeneralLedger GeneralLedger;
        private ApplicationViewFactory applicationViewFactory;
        public FluentAddressBook AddressBook;
        public FluentCustomer Customer;
        public FluentChartOfAccount ChartOfAccount;
        public FluentInventory Inventory;
        public AccountPayableModule()
        {
            unitOfWork = new UnitOfWork();
            AccountPayable = new FluentAccountPayable(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            PackingSlip = new FluentPackingSlip(unitOfWork);
            PackingSlipDetail = new FluentPackingSlipDetail(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            PurchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
            SupplierLedger = new FluentSupplierLedger(unitOfWork);
            SupplierInvoice = new FluentSupplierInvoice(unitOfWork);
            SupplierInvoiceDetail = new FluentSupplierInvoiceDetail(unitOfWork);
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            applicationViewFactory = new ApplicationViewFactory();
            AddressBook = new FluentAddressBook(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Inventory = new FluentInventory(unitOfWork);
        }


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
                Task<GeneralLedgerView> generalLedgerViewTask = Task.Run(async () => await GeneralLedger.Query().GetViewByDocNumber(ledgerView.DocNumber, ledgerView.DocType));
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
