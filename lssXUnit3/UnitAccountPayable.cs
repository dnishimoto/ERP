using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountPayableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using lssWebApi2.AccountPayableDetailDomain;

namespace lssWebApi2.AccountPayableDomain
{

    public class UnitAccountPayable
    {

        private readonly ITestOutputHelper output;

        public UnitAccountPayable(ITestOutputHelper output)
        {
            this.output = output;
        }
       // [Fact]
        //public async Task TestPartialPayment()
       //{
       // }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AccountPayableModule AccountPayableMod = new AccountPayableModule();
            Supplier supplier = await AccountPayableMod.Supplier.Query().GetEntityById(3);
            ChartOfAccount chartOfAccount = await AccountPayableMod.ChartOfAccount.Query().GetEntityById(17);


            AccountPayableView view = new AccountPayableView()
            {
                DocNumber = 17,
                GrossAmount = 299.99M,
                DiscountAmount = null,
                Remark = null,
                Gldate = DateTime.Now,
                SupplierId = supplier.SupplierId,
                ContractId = null,
                PoquoteId = null,
                Description = "back to school",
                PurchaseOrderId = null,
                Tax = 16.08M,
                AccountId = chartOfAccount.AccountId,
                DocType = "STD",
                PaymentTerms = "Net 30",
                DiscountPercent = 0,
                AmountOpen = 0M,
                OrderNumber = "PO-2",
                AmountPaid = 299.99M,
            };
            NextNumber nnNextNumber = await AccountPayableMod.AccountPayable.Query().GetNextNumber();

            view.AccountPayableNumber = nnNextNumber.NextNumberValue;

            AccountPayable accountPayable = await AccountPayableMod.AccountPayable.Query().MapToEntity(view);

            AccountPayableMod.AccountPayable.AddAccountPayable(accountPayable).Apply();

            AccountPayable newAccountPayable = await AccountPayableMod.AccountPayable.Query().GetEntityByNumber(view.AccountPayableNumber);

            Assert.NotNull(newAccountPayable);

            //Add Account Payable Detail for an invoice detail payment
            AccountPayableDetailModule AccountPayableDetailMod = new AccountPayableDetailModule();

            Invoice invoice = await AccountPayableDetailMod.Invoice.Query().GetEntityById(20);
            InvoiceDetail invoiceDetail = await AccountPayableDetailMod.InvoiceDetail.Query().GetEntityById(21);
            PurchaseOrderDetail purchaseOrderDetail = await AccountPayableDetailMod.PurchaseOrderDetail.Query().GetEntityById(invoiceDetail.PurchaseOrderDetailId);
            AccountPayableDetailView accountPayableDetailView = new AccountPayableDetailView()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDetailId = invoiceDetail.InvoiceDetailId,
                UnitPrice = invoiceDetail.UnitPrice,
                Quantity = invoiceDetail.Quantity,
                QuantityReceived =purchaseOrderDetail.ReceivedQuantity,
                Amount = purchaseOrderDetail.Amount,
                AmountPaid = invoiceDetail.Amount,
                PurchaseOrderDetailId = invoiceDetail.PurchaseOrderDetailId,
                SalesOrderDetailId = invoiceDetail.SalesOrderDetailId,
                ItemId = invoiceDetail.ItemId,
                ExtendedDescription = invoiceDetail.ExtendedDescription,
                PurchaseOrderId = invoiceDetail.PurchaseOrderId,
                CustomerId = invoiceDetail.CustomerId,
                SupplierId = invoiceDetail.SupplierId,
                AccountPayableId = newAccountPayable.AccountPayableId,
                AccountPayableDetailNumber=(await AccountPayableDetailMod.AccountPayableDetail.Query().GetNextNumber()).NextNumberValue
            };

            AccountPayableDetail accountPayableDetail = await AccountPayableDetailMod.AccountPayableDetail.Query().MapToEntity(accountPayableDetailView);

            AccountPayableDetailMod.AccountPayableDetail.AddAccountPayableDetail(accountPayableDetail).Apply();

            AccountPayableDetail newAccountPayableDetail = await AccountPayableDetailMod.AccountPayableDetail.Query().GetEntityByNumber(accountPayableDetailView.AccountPayableDetailNumber);

            Assert.NotNull(newAccountPayableDetail);

    
            newAccountPayable.PaymentTerms = "Net 30 Update";

            AccountPayableMod.AccountPayable.UpdateAccountPayable(newAccountPayable).Apply();

            AccountPayableView updateView = await AccountPayableMod.AccountPayable.Query().GetViewById(newAccountPayable.AccountPayableId);

            Assert.Same(updateView.PaymentTerms, "Net 30 Update");

            AccountPayableDetailMod.AccountPayableDetail.DeleteAccountPayableDetail(newAccountPayableDetail).Apply();
            AccountPayableDetail lookupAccountPayableDetail = await AccountPayableDetailMod.AccountPayableDetail.Query().GetEntityById(accountPayableDetailView.AccountPayableDetailId);
            Assert.Null(lookupAccountPayableDetail);

            AccountPayableMod.AccountPayable.DeleteAccountPayable(newAccountPayable).Apply();
            AccountPayable lookupAccountPayable = await AccountPayableMod.AccountPayable.Query().GetEntityById(view.AccountPayableId);

            Assert.Null(lookupAccountPayable);

          
        }



    }
}
