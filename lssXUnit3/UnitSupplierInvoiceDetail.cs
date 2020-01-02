using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SupplierInvoiceDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{

    public class UnitSupplierInvoiceDetail
    {

        private readonly ITestOutputHelper output;

        public UnitSupplierInvoiceDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            SupplierInvoiceDetailModule SupplierInvoiceDetailMod = new SupplierInvoiceDetailModule();
            SupplierInvoice supplierInvoice = await SupplierInvoiceDetailMod.SupplierInvoice.Query().GetEntityById(2);
            Invoice invoice = await SupplierInvoiceDetailMod.Invoice.Query().GetEntityById(20);
            InvoiceDetail invoiceDetail = await SupplierInvoiceDetailMod.InvoiceDetail.Query().GetEntityById(21);
            ItemMaster itemMaster = await SupplierInvoiceDetailMod.ItemMaster.Query().GetEntityById(11);


           SupplierInvoiceDetailView view = new SupplierInvoiceDetailView()
            {
                  InvoiceId=invoice?.InvoiceId,
                  InvoiceDocument=invoice?.InvoiceDocument,
                  InvoiceDetailId=invoiceDetail?.InvoiceDetailId,
                  SupplierInvoiceId=supplierInvoice.SupplierInvoiceId,
                  UnitPrice=31.50M,
                  Quantity=4,
                  UnitOfMeasure="Each",
                  ExtendedCost=268M,
                  ItemId=itemMaster.ItemId,
                  Description = itemMaster.Description


            };
            NextNumber nnNextNumber = await SupplierInvoiceDetailMod.SupplierInvoiceDetail.Query().GetNextNumber();

            view.SupplierInvoiceDetailNumber = nnNextNumber.NextNumberValue;

            SupplierInvoiceDetail supplierInvoiceDetail = await SupplierInvoiceDetailMod.SupplierInvoiceDetail.Query().MapToEntity(view);

            SupplierInvoiceDetailMod.SupplierInvoiceDetail.AddSupplierInvoiceDetail(supplierInvoiceDetail).Apply();

            SupplierInvoiceDetail newSupplierInvoiceDetail = await SupplierInvoiceDetailMod.SupplierInvoiceDetail.Query().GetEntityByNumber(view.SupplierInvoiceDetailNumber);

            Assert.NotNull(newSupplierInvoiceDetail);

            newSupplierInvoiceDetail.Description = "SupplierInvoiceDetail Test Update";

            SupplierInvoiceDetailMod.SupplierInvoiceDetail.UpdateSupplierInvoiceDetail(newSupplierInvoiceDetail).Apply();

            SupplierInvoiceDetailView updateView = await SupplierInvoiceDetailMod.SupplierInvoiceDetail.Query().GetViewById(newSupplierInvoiceDetail.SupplierInvoiceDetailId);

            Assert.Same(updateView.Description, "SupplierInvoiceDetail Test Update");
              SupplierInvoiceDetailMod.SupplierInvoiceDetail.DeleteSupplierInvoiceDetail(newSupplierInvoiceDetail).Apply();
            SupplierInvoiceDetail lookupSupplierInvoiceDetail= await SupplierInvoiceDetailMod.SupplierInvoiceDetail.Query().GetEntityById(view.SupplierInvoiceDetailId);

            Assert.Null(lookupSupplierInvoiceDetail);
        }
       
      

    }
}
