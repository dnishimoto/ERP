using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.InvoiceDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.InvoiceDetailDomain
{

    public class UnitInvoiceDetail
    {

        private readonly ITestOutputHelper output;

        public UnitInvoiceDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            InvoiceDetailModule InvoiceDetailMod = new InvoiceDetailModule();
            ItemMaster itemMaster = await InvoiceDetailMod.ItemMaster.Query().GetEntityById(12);
            Invoice invoice = await InvoiceDetailMod.Invoice.Query().GetEntityById(18);
            PurchaseOrder purchaseOrder = await InvoiceDetailMod.PurchaseOrder.Query().GetEntityById(-1);
            InvoiceDetailView view = new InvoiceDetailView()
            {
                Quantity = 1,
                UnitOfMeasure = "Each",
                UnitPrice = 101.1M,
                Amount = 101.1M,
                DiscountPercent = 0.02M,
                DiscountAmount = 2.01M,
                DiscountDueDate = DateTime.Parse("12/1/2019"),
                ItemId = itemMaster.ItemId,
                ItemDescription = itemMaster.Description,
                ItemDescription2 = itemMaster.Description2,
                InvoiceId = invoice.InvoiceId,
                ExtendedDescription = "test extend description",
                PONumber=purchaseOrder?.Ponumber

            };
            NextNumber nnNextNumber = await InvoiceDetailMod.InvoiceDetail.Query().GetNextNumber();

            view.InvoiceDetailNumber = nnNextNumber.NextNumberValue;

            InvoiceDetail invoiceDetail = await InvoiceDetailMod.InvoiceDetail.Query().MapToEntity(view);

            InvoiceDetailMod.InvoiceDetail.AddInvoiceDetail(invoiceDetail).Apply();

            InvoiceDetail newInvoiceDetail = await InvoiceDetailMod.InvoiceDetail.Query().GetEntityByNumber(view.InvoiceDetailNumber);

            Assert.NotNull(newInvoiceDetail);

            newInvoiceDetail.ExtendedDescription = "Description Update";

            InvoiceDetailMod.InvoiceDetail.UpdateInvoiceDetail(newInvoiceDetail).Apply();

            InvoiceDetailView updateView = await InvoiceDetailMod.InvoiceDetail.Query().GetViewById(newInvoiceDetail.InvoiceDetailId);

            Assert.Same(updateView.ExtendedDescription, "Description Update");
            InvoiceDetailMod.InvoiceDetail.DeleteInvoiceDetail(newInvoiceDetail).Apply();
            InvoiceDetail lookupInvoiceDetail = await InvoiceDetailMod.InvoiceDetail.Query().GetEntityById(view.InvoiceDetailId);

            Assert.Null(lookupInvoiceDetail);
        }



    }
}
