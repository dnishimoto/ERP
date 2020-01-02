using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SupplierInvoiceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.SupplierInvoiceDomain
{

    public class UnitSupplierInvoice
    {

        private readonly ITestOutputHelper output;

        public UnitSupplierInvoice(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            SupplierInvoiceModule SupplierInvoiceMod = new SupplierInvoiceModule();
            Supplier supplier = await SupplierInvoiceMod.Supplier.Query().GetEntityById(3);
            AddressBook addressBook = await SupplierInvoiceMod.AddressBook.Query().GetEntityById(supplier?.AddressId);
            PurchaseOrder purchaseOrder = await SupplierInvoiceMod.PurchaseOrder.Query().GetEntityById(2);
            Invoice invoice = await SupplierInvoiceMod.Invoice.Query().GetEntityById(20);
            SupplierInvoiceView view = new SupplierInvoiceView()
            {
                SupplierId = supplier.SupplierId,
                SupplierName = addressBook?.Name,
                SupplierInvoiceDate = DateTime.Parse("12/3/2019"),
                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                PONumber = purchaseOrder.Ponumber,
                Amount = 268M,
                Description = "Back to School supplies",
                TaxAmount = 16.08M,
                PaymentDueDate=DateTime.Parse("12/4/2019"),
                PaymentTerms="Net 30",
                DiscountDueDate=DateTime.Parse("12/4/2019"),
                FreightCost=4.98M,
                InvoiceId=invoice?.InvoiceId,
                InvoiceDocument=invoice?.InvoiceDocument
                

            };
            NextNumber nnNextNumber = await SupplierInvoiceMod.SupplierInvoice.Query().GetNextNumber();

            view.SupplierInvoiceNumber = nnNextNumber.NextNumberValue;

            SupplierInvoice supplierInvoice = await SupplierInvoiceMod.SupplierInvoice.Query().MapToEntity(view);

            SupplierInvoiceMod.SupplierInvoice.AddSupplierInvoice(supplierInvoice).Apply();

            SupplierInvoice newSupplierInvoice = await SupplierInvoiceMod.SupplierInvoice.Query().GetEntityByNumber(view.SupplierInvoiceNumber);

            Assert.NotNull(newSupplierInvoice);

            newSupplierInvoice.Description = "SupplierInvoice Test Update";

            SupplierInvoiceMod.SupplierInvoice.UpdateSupplierInvoice(newSupplierInvoice).Apply();

            SupplierInvoiceView updateView = await SupplierInvoiceMod.SupplierInvoice.Query().GetViewById(newSupplierInvoice.SupplierInvoiceId);

            Assert.Same(updateView.Description, "SupplierInvoice Test Update");
              SupplierInvoiceMod.SupplierInvoice.DeleteSupplierInvoice(newSupplierInvoice).Apply();
            SupplierInvoice lookupSupplierInvoice= await SupplierInvoiceMod.SupplierInvoice.Query().GetEntityById(view.SupplierInvoiceId);

            Assert.Null(lookupSupplierInvoice);
        }
       
      

    }
}
