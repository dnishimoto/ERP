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

namespace lssWebApi2.AccountPayableDetailDomain
{

    public class UnitAccountPayableDetail
    {

        private readonly ITestOutputHelper output;

        public UnitAccountPayableDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AccountPayableDetailModule AccountPayableDetailMod = new AccountPayableDetailModule();

            Invoice invoice = await AccountPayableDetailMod.Invoice.Query().GetEntityById(20);
            InvoiceDetail invoiceDetail = await AccountPayableDetailMod.InvoiceDetail.Query().GetEntityById(21);
            PurchaseOrderDetail purchaseOrderDetail = await AccountPayableDetailMod.PurchaseOrderDetail.Query().GetEntityById(invoiceDetail.PurchaseOrderDetailId);
            AccountPayable accountPayable = await AccountPayableDetailMod.AccountPayable.Query().GetEntityById(2);
            AccountPayableDetailView view = new AccountPayableDetailView()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDetailId = invoiceDetail.InvoiceDetailId,
                UnitPrice = invoiceDetail.UnitPrice,
                Quantity = invoiceDetail.Quantity,
                //QuantityReceived =invoiceDetail.
                Amount = purchaseOrderDetail.Amount,
                AmountPaid = invoiceDetail.Amount,
                PurchaseOrderDetailId = invoiceDetail.PurchaseOrderDetailId,
                SalesOrderDetailId = invoiceDetail.SalesOrderDetailId,
                ItemId = invoiceDetail.ItemId,
                ExtendedDescription = invoiceDetail.ExtendedDescription,
                PurchaseOrderId = invoiceDetail.PurchaseOrderId,
                CustomerId = invoiceDetail.CustomerId,
                SupplierId = invoiceDetail.SupplierId,
                AccountPayableId = accountPayable.AccountPayableId,
                AccountPayableDetailNumber = (await AccountPayableDetailMod.AccountPayableDetail.Query().GetNextNumber()).NextNumberValue
            };

            AccountPayableDetail accountPayableDetail = await AccountPayableDetailMod.AccountPayableDetail.Query().MapToEntity(view);

            AccountPayableDetailMod.AccountPayableDetail.AddAccountPayableDetail(accountPayableDetail).Apply();

            AccountPayableDetail newAccountPayableDetail = await AccountPayableDetailMod.AccountPayableDetail.Query().GetEntityByNumber(view.AccountPayableDetailNumber);

            Assert.NotNull(newAccountPayableDetail);

            newAccountPayableDetail.ExtendedDescription = "AccountPayableDetail Test Update";

            AccountPayableDetailMod.AccountPayableDetail.UpdateAccountPayableDetail(newAccountPayableDetail).Apply();

            AccountPayableDetailView updateView = await AccountPayableDetailMod.AccountPayableDetail.Query().GetViewById(newAccountPayableDetail.AccountPayableDetailId);

            Assert.Same(updateView.ExtendedDescription, "AccountPayableDetail Test Update");
              AccountPayableDetailMod.AccountPayableDetail.DeleteAccountPayableDetail(newAccountPayableDetail).Apply();
            AccountPayableDetail lookupAccountPayableDetail= await AccountPayableDetailMod.AccountPayableDetail.Query().GetEntityById(view.AccountPayableDetailId);

            Assert.Null(lookupAccountPayableDetail);
        }
       
      

    }
}
