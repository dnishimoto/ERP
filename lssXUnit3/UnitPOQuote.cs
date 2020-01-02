using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.POQuoteDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.POQuoteDomain
{

    public class UnitPOQuote
    {

        private readonly ITestOutputHelper output;

        public UnitPOQuote(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AddressBook customerAddress = null;
            AddressBook supplierAddress = null;
            POQuoteModule POQuoteMod = new POQuoteModule();
            PurchaseOrder purchaseOrder = await POQuoteMod.PurchaseOrder.Query().GetEntityById(2);
            Customer customer = await POQuoteMod.Customer.Query().GetEntityById(1);
            Supplier supplier = await POQuoteMod.Supplier.Query().GetEntityById(3);
            customerAddress = await POQuoteMod.AddressBook.Query().GetEntityById(customer.AddressId);
            supplierAddress = await POQuoteMod.AddressBook.Query().GetEntityById(supplier.AddressId);
            

           POQuoteView view = new POQuoteView()
            {
                 QuoteAmount=1234M,
                 SubmittedDate=DateTime.Parse("12/7/2019"),
                 PurchaseOrderId=purchaseOrder.PurchaseOrderId,
                 Remarks="Remarks",
                 Sku="sku123",
                 Description="description 123",
                 SupplierId=supplier.SupplierId,
                 SupplierName=supplierAddress?.Name,
                 CustomerId=customer.CustomerId,
                 CustomerName=customerAddress?.Name

            };
            NextNumber nnNextNumber = await POQuoteMod.POQuote.Query().GetNextNumber();

            view.PoquoteNumber = nnNextNumber.NextNumberValue;

            Poquote poQuote = await POQuoteMod.POQuote.Query().MapToEntity(view);

            POQuoteMod.POQuote.AddPOQuote(poQuote).Apply();

            Poquote newPOQuote = await POQuoteMod.POQuote.Query().GetEntityByNumber(view.PoquoteNumber);

            Assert.NotNull(newPOQuote);

            newPOQuote.Remarks = "Remarks Update";

            POQuoteMod.POQuote.UpdatePOQuote(newPOQuote).Apply();

            POQuoteView updateView = await POQuoteMod.POQuote.Query().GetViewById(newPOQuote.PoquoteId);

            Assert.Same(updateView.Remarks, "Remarks Update");
              POQuoteMod.POQuote.DeletePOQuote(newPOQuote).Apply();
            Poquote lookupPOQuote= await POQuoteMod.POQuote.Query().GetEntityById(view.PoquoteId);

            Assert.Null(lookupPOQuote);
        }
       
      

    }
}
