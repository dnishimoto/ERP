using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountReceivableDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using lssWebApi2.AccountReceivableDomain;

namespace lssWebApi2.AccountReceivableDetailDomain
{

    public class UnitAccountReceivableDetail
    {

        private readonly ITestOutputHelper output;

        public UnitAccountReceivableDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AccountReceivableDetailModule AccountReceivableDetailMod = new AccountReceivableDetailModule();
            Invoice invoice = await AccountReceivableDetailMod.Invoice.Query().GetEntityById(18);
            Customer customer = await AccountReceivableDetailMod.Customer.Query().GetEntityById(9);
            AddressBook addressBookCustomer = await AccountReceivableDetailMod.AddressBook.Query().GetEntityById(customer?.AddressId);
            PurchaseOrder purchaseOrder = await AccountReceivableDetailMod.PurchaseOrder.Query().GetEntityById(20);
            Udc udc = await AccountReceivableDetailMod.Udc.Query().GetEntityById(66);
            ChartOfAccount chartOfAccount = await AccountReceivableDetailMod.ChartOfAccount.Query().GetEntityById(3);

            AccountReceivableView acctRecView = new AccountReceivableView()
            {
                DiscountDueDate = DateTime.Parse("2/8/2020"),
                Gldate = DateTime.Parse("7/16/2018"),
                InvoiceId = invoice.InvoiceId,
                CreateDate = DateTime.Parse("7/16/2018"),
                DocNumber = (await AccountReceivableDetailMod.AccountReceivable.Query().GetDocNumber()).NextNumberValue,
                Remark = "VNW Fixed Asset project",
                PaymentTerms = "Net 30",
                CustomerId = customer?.CustomerId ?? 0,
                PurchaseOrderId = purchaseOrder?.PurchaseOrderId ?? 0,
                Description = "Fixed Asset Project",
                AcctRecDocTypeXrefId = udc.XrefId,
                AccountId = chartOfAccount.AccountId,
                Amount = 1500M,
                DebitAmount = 189.6300M,
                CreditAmount = 1500,
                OpenAmount = 1335.37M,
                DiscountPercent = 0,
                DiscountAmount = 0,
                AcctRecDocType = "INV",
                InterestPaid = 0,
                LateFee = 25.0000M,
                AccountReceivableNumber = (await AccountReceivableDetailMod.AccountReceivable.Query().GetNextNumber()).NextNumberValue,
                CustomerPurchaseOrder = "PO-321",
                Tax = 0,
                InvoiceDocument = invoice.InvoiceDocument,
                CustomerName = addressBookCustomer?.Name,
                DocType=udc.KeyCode
            };

            AccountReceivable newAccountReceivable = await AccountReceivableDetailMod.AccountReceivable.Query().MapToEntity(acctRecView);
            AccountReceivableDetailMod.AccountReceivable.AddAccountReceivable(newAccountReceivable).Apply();
            AccountReceivable lookupAccountReceivable = await AccountReceivableDetailMod.AccountReceivable.Query().GetEntityByNumber(newAccountReceivable.AccountReceivableNumber);
            InvoiceDetail invoiceDetail = await AccountReceivableDetailMod.InvoiceDetail.Query().GetEntityById(6);
            PurchaseOrderDetail purchaseOrderDetail = await AccountReceivableDetailMod.PurchaseOrderDetail.Query().GetEntityById(33);
            ItemMaster itemMaster = await AccountReceivableDetailMod.ItemMaster.Query().GetEntityById(4);

            AccountReceivableDetailView acctRecDetailView = new AccountReceivableDetailView()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDetailId = invoiceDetail.InvoiceDetailId,
                AccountReceivableId = lookupAccountReceivable.AccountReceivableId,
                UnitPrice = invoiceDetail.UnitPrice,
                Quantity = invoiceDetail.Quantity,
                UnitOfMeasure = invoiceDetail.UnitOfMeasure,
                Amount = invoiceDetail.Amount,
                AmountReceived = 189.6300M,
                PurchaseOrderDetailId = purchaseOrderDetail.PurchaseOrderDetailId,
                ItemId = itemMaster.ItemId,
                AccountReceivableDetailNumber = (await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetNextNumber()).NextNumberValue,
                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                CustomerId = customer.CustomerId,
                QuantityDelivered = invoiceDetail.Quantity,
                Comment = "possible write off",
                TypeOfPayment = "Partial"
            };

            AccountReceivableDetail accountReceivableDetail = await AccountReceivableDetailMod.AccountReceivableDetail.Query().MapToEntity(acctRecDetailView);

            AccountReceivableDetailMod.AccountReceivableDetail.AddAccountReceivableDetail(accountReceivableDetail).Apply();

            AccountReceivableDetail newAccountReceivableDetail = await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetEntityByNumber(acctRecDetailView.AccountReceivableDetailNumber);

            Assert.NotNull(newAccountReceivableDetail);

            newAccountReceivableDetail.Comment = "AccountReceivableDetail Test Update";

            AccountReceivableDetailMod.AccountReceivableDetail.UpdateAccountReceivableDetail(newAccountReceivableDetail).Apply();

            AccountReceivableDetailView updateView = await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetViewById(newAccountReceivableDetail.AccountReceivableDetailId);

            Assert.Same(updateView.Comment, "AccountReceivableDetail Test Update");
            AccountReceivableDetailMod.AccountReceivableDetail.DeleteAccountReceivableDetail(newAccountReceivableDetail).Apply();
            AccountReceivableDetail lookupAccountReceivableDetail = await AccountReceivableDetailMod.AccountReceivableDetail.Query().GetEntityById(acctRecDetailView.AccountReceivableDetailId);

            Assert.Null(lookupAccountReceivableDetail);

            AccountReceivableDetailMod.AccountReceivable.DeleteAccountReceivable(lookupAccountReceivable).Apply();
            AccountReceivable lookupAccountReceivable2 = await AccountReceivableDetailMod.AccountReceivable.Query().GetEntityByNumber(newAccountReceivable.AccountReceivableNumber);

            Assert.Null(lookupAccountReceivable2);
        }



    }
}
