using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PurchaseOrderDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.PurchaseOrderDetailDomain
{

    public class UnitPurchaseOrderDetail
    {

        private readonly ITestOutputHelper output;

        public UnitPurchaseOrderDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PurchaseOrderDetailModule PurchaseOrderDetailMod = new PurchaseOrderDetailModule();
            PurchaseOrder purchaseOrder = await PurchaseOrderDetailMod.PurchaseOrder.Query().GetEntityById(2);
            ItemMaster itemMaster = await PurchaseOrderDetailMod.ItemMaster.Query().GetEntityById(11);
           PurchaseOrderDetailView view = new PurchaseOrderDetailView()
            {
                   PurchaseOrderId=purchaseOrder.PurchaseOrderId,
                   Amount=101.1M,
                  OrderedQuantity=5,
                  ItemId=itemMaster.ItemId,
                  Description=itemMaster.Description,
                  ItemDescription2=itemMaster.Description2,
                  ItemCode=itemMaster.ItemCode,
                  UnitPrice=itemMaster.UnitPrice,
                  UnitOfMeasure=itemMaster.UnitOfMeasure,
                  ReceivedDate=DateTime.Parse("11/30/2019"),
                  ExpectedDeliveryDate=DateTime.Parse("12/1/2019"),
                  OrderDate=DateTime.Parse("11/30/2019"),
                  ReceivedQuantity=0,
                  RemainingQuantity=5
            };
            NextNumber nnNextNumber = await PurchaseOrderDetailMod.PurchaseOrderDetail.Query().GetNextNumber();

            view.PurchaseOrderDetailNumber = nnNextNumber.NextNumberValue;

            PurchaseOrderDetail purchaseOrderDetail = await PurchaseOrderDetailMod.PurchaseOrderDetail.Query().MapToEntity(view);

            PurchaseOrderDetailMod.PurchaseOrderDetail.AddPurchaseOrderDetail(purchaseOrderDetail).Apply();

            PurchaseOrderDetail newPurchaseOrderDetail = await PurchaseOrderDetailMod.PurchaseOrderDetail.Query().GetEntityByNumber(view.PurchaseOrderDetailNumber);

            Assert.NotNull(newPurchaseOrderDetail);

            newPurchaseOrderDetail.Description = "PurchaseOrderDetail Test Update";

            PurchaseOrderDetailMod.PurchaseOrderDetail.UpdatePurchaseOrderDetail(newPurchaseOrderDetail).Apply();

            PurchaseOrderDetailView updateView = await PurchaseOrderDetailMod.PurchaseOrderDetail.Query().GetViewById(newPurchaseOrderDetail.PurchaseOrderDetailId);

            Assert.Same(updateView.Description, "PurchaseOrderDetail Test Update");
              PurchaseOrderDetailMod.PurchaseOrderDetail.DeletePurchaseOrderDetail(newPurchaseOrderDetail).Apply();
            PurchaseOrderDetail lookupPurchaseOrderDetail= await PurchaseOrderDetailMod.PurchaseOrderDetail.Query().GetEntityById(view.PurchaseOrderDetailId);

            Assert.Null(lookupPurchaseOrderDetail);
        }
       
      

    }
}
