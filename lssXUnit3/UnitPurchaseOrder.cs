using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PurchaseOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.PurchaseOrderDomain
{

    public class UnitPurchaseOrder
    {

        private readonly ITestOutputHelper output;

        public UnitPurchaseOrder(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AddressBook addressBook = null;
            AddressBook buyerAddressBook = null;
            PurchaseOrderModule PurchaseOrderMod = new PurchaseOrderModule();
            ChartOfAccount account = await PurchaseOrderMod.ChartOfAccount.Query().GetEntityById(17);
            Supplier supplier = await PurchaseOrderMod.Supplier.Query().GetEntityById(3);
            if (supplier != null) {   addressBook = await PurchaseOrderMod.AddressBook.Query().GetEntityById(supplier.AddressId); }
            Contract contract = await PurchaseOrderMod.Contract.Query().GetEntityById(1);
            Poquote poquote = await PurchaseOrderMod.POQuote.Query().GetEntityById(2);
            Buyer buyer = await PurchaseOrderMod.Buyer.Query().GetEntityById(1);
            if (buyer != null) buyerAddressBook = await PurchaseOrderMod.AddressBook.Query().GetEntityById(buyer.AddressId);
            TaxRatesByCode taxRatesByCode = await PurchaseOrderMod.TaxRatesByCode.Query().GetEntityById(1);

            PurchaseOrderView view = new PurchaseOrderView()
           {
               DocType = "STD",
               PaymentTerms = "Net 30",
               Amount = 286.11M,
               AmountPaid=0,
               Remark = "PO Remark",
               Gldate=DateTime.Parse("11/29/2019"),
               AccountId=account.AccountId,
               Location = account.Location,
                BusUnit = account.BusUnit,
                Subsidiary = account.Subsidiary,
                SubSub = account.SubSub,
                Account = account.Account,
                AccountDescription = account.Description,
                SupplierId =supplier.SupplierId,
                CustomerId=contract?.CustomerId,
               SupplierName=addressBook.Name,
               ContractId=contract?.ContractId,
               PoquoteId=poquote?.PoquoteId,
               QuoteAmount=poquote?.QuoteAmount,
               Description ="PO Description",
               Ponumber ="PO-123",
               TakenBy ="David Nishimoto",
               ShippedToName =" shipped name",
               ShippedToAddress1 ="shipped to address1",
               ShippedToAddress2 ="shipped to address2",
               ShippedToCity="shipped city",
               ShippedToState ="ID",
               ShippedToZipcode ="83709",
               BuyerId = buyer.BuyerId,
               BuyerName= buyerAddressBook?.Name,
               RequestedDate =DateTime.Parse("11/29/2019"),
               PromisedDeliveredDate = DateTime.Parse("11/29/2019"),
               Tax=0M,
               TransactionDate = DateTime.Parse("11/29/2019"),
               TaxCode1 = taxRatesByCode.TaxCode,
               TaxCode2="",
               TaxRate=taxRatesByCode.TaxRate??0
            };
            NextNumber nnNextNumber = await PurchaseOrderMod.PurchaseOrder.Query().GetNextNumber();

            view.PurchaseOrderNumber = nnNextNumber.NextNumberValue;

            PurchaseOrder purchaseOrder = await PurchaseOrderMod.PurchaseOrder.Query().MapToEntity(view);

            PurchaseOrderMod.PurchaseOrder.AddPurchaseOrder(purchaseOrder).Apply();

            PurchaseOrder newPurchaseOrder = await PurchaseOrderMod.PurchaseOrder.Query().GetEntityByNumber(view.PurchaseOrderNumber);

            Assert.NotNull(newPurchaseOrder);

            newPurchaseOrder.TakenBy = "David Nishimoto Update";

            PurchaseOrderMod.PurchaseOrder.UpdatePurchaseOrder(newPurchaseOrder).Apply();

            PurchaseOrderView updateView = await PurchaseOrderMod.PurchaseOrder.Query().GetViewById(newPurchaseOrder.PurchaseOrderId);

            Assert.Same(updateView.TakenBy, "David Nishimoto Update");
              PurchaseOrderMod.PurchaseOrder.DeletePurchaseOrder(newPurchaseOrder).Apply();
            PurchaseOrder lookupPurchaseOrder= await PurchaseOrderMod.PurchaseOrder.Query().GetEntityById(view.PurchaseOrderId);

            Assert.Null(lookupPurchaseOrder);
        }
       
      

    }
}
