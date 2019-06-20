using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ShipmentsDomain;
using lssWebApi2.ShipmentsDomain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using lssWebApi2.SalesOrderDomain.Repository;
using lssWebApi2.SalesOrderDomain;
using lssWebApi2.AbstractFactory;

/*
 
1. Shipments are created from sales orders

*/
namespace lssXUnit3
{
    public class UnitShipments
    {
        private readonly ITestOutputHelper output;



        public UnitShipments(ITestOutputHelper output)
        {
            this.output = output;

        }

     
        [Fact]
        public async Task TestAddUpdatDeleteShipments()
        {
            SalesOrderModule salesOrderMod = new SalesOrderModule();

            Udc orderType = await salesOrderMod.SalesOrder.Query().GetUdc("ORDER_TYPE", SalesOrderEnum.CASH_SALES.ToString());
            Udc paymentTerms = await salesOrderMod.SalesOrder.Query().GetUdc("PAYMENTTERMS", PaymentTermsEnum.Net_2_10_30.ToString());
            NextNumber nnSalesOrder = await salesOrderMod.SalesOrder.Query().GetNextNumber();

            SalesOrderView view = new SalesOrderView()
            {
                Taxes = 0,
                Amount = 0,
                OrderType = orderType.KeyCode.ToString(),
                CustomerId = 2,
                TakenBy = "David Nishimoto",
                FreightAmount = 0,
                PaymentInstrument = "Check",
                PaymentTerms = paymentTerms.KeyCode.ToString()
            };

            List<SalesOrderDetailView> detailViews = new List<SalesOrderDetailView>() {
                new SalesOrderDetailView(){
                ItemId=11,
                Description ="Flower Bent Rod",
                Quantity =4,
                QuantityOpen =4,
                Amount= 31.52M,
                AmountOpen =31.52M,
                UnitOfMeasure ="Each",
                UnitPrice =7.88M,
                AccountId =5,
                ScheduledShipDate =DateTime.Parse("5/21/2019"),
                PromisedDate =DateTime.Parse("5/23/2019"),
                GLDate =DateTime.Parse("5/21/2019"),
                InvoiceDate=(DateTime?) null,
                ShippedDate=(DateTime?) null,
                GrossWeight = 4.1000M*4M,
                GrossWeightUnitOfMeasure = "LBS",
                //UnitVolume { get; set; }
                //UnitVolumeUnitOfMeasurement { get; set; }
                BusUnit ="700",
                CompanyNumber ="1000",
                LineNumber=1
                }
            };


            view.OrderNumber = nnSalesOrder.NextNumberValue.ToString();

            SalesOrder salesOrder = await salesOrderMod.SalesOrder.Query().MapToEntity(view);

            salesOrderMod.SalesOrder.AddSalesOrder(salesOrder).Apply();

            SalesOrder newSalesOrder = await salesOrderMod.SalesOrder.Query().GetEntityByNumber(view.OrderNumber);

            Assert.NotNull(newSalesOrder);

            newSalesOrder.Note = "sales order note test";

            SalesOrderView newSalesOrderView = await salesOrderMod.SalesOrder.Query().MapToView(newSalesOrder);

            salesOrderMod.SalesOrder.UpdateSalesOrder(newSalesOrder).Apply();

            SalesOrderView updateView = await salesOrderMod.SalesOrder.Query().GetViewById(newSalesOrder.SalesOrderId);

            Assert.Same(updateView.Note, "sales order note test");

            detailViews.ForEach(m => m.SalesOrderId = newSalesOrder.SalesOrderId);

            foreach (var item in detailViews)
            {
                NextNumber nn = await salesOrderMod.SalesOrderDetail.Query().GetNextNumber();
                item.SalesOrderDetailNumber = nn.NextNumberValue;
            }

            List<SalesOrderDetail> salesOrderDetails = await salesOrderMod.SalesOrderDetail.Query().MapToEntity(detailViews);

            salesOrderMod.SalesOrderDetail.AddSalesOrderDetails(salesOrderDetails).Apply();

            salesOrderDetails.ForEach(m => m.Description += " Updated");

            salesOrderMod.SalesOrderDetail.UpdateSalesOrderDetails(salesOrderDetails).Apply();


            List<SalesOrderDetail> soListDetails = await salesOrderMod.SalesOrderDetail.Query().GetDetailsBySalesOrderId(newSalesOrder.SalesOrderId);


            /************************************Shipments**************************/

        ShipmentCreationView shipmentCreation = new ShipmentCreationView() {
            SalesOrderId = newSalesOrder.SalesOrderId,
            ActualWeight =100,
            BillableWeight =100,
            ShippedFromLocationId =3,
            ShippedToLocationId =9,
            TrackingNumber ="123",
            WeightUOM="LBS",
            ShipmentDate = DateTime.Now,
            CarrierId =2
       
  
    };

            foreach (var item in soListDetails)
            {
                var newItem = new ItemsAdjustedQuantityShippedStruct() {
                    SalesOrderDetailId = item.SalesOrderDetailId,
                    AdjustedQuantityShipped = item.Quantity ?? 0,
                    AdjustedAmountShipped=item.Amount??0
                };
                shipmentCreation.ItemsAdjustedQuantityShipped.Add(newItem);
            }

        


            ShipmentsModule ShipmentsMod = new ShipmentsModule();
           
            Shipments newShipments = await ShipmentsMod.Shipments.Query().CreateShipmentBySalesOrder(shipmentCreation);

            List<ShipmentsDetail> newShipmentsDetails = await ShipmentsMod.ShipmentsDetail.Query().CreateShipmentsDetailBySalesOrder(shipmentCreation);
            //TODO Calculate the amount, duty, taxes, shipping cost

            ShipmentsMod.Shipments.AddShipments(newShipments).Apply();

            Shipments lookupShipments = await ShipmentsMod.Shipments.Query().GetEntityByNumber(newShipments.ShipmentNumber);

            Assert.NotNull(lookupShipments);

            ShipmentsView newShipmentsView = await ShipmentsMod.Shipments.Query().MapToView(lookupShipments);

            lookupShipments.TrackingNumber = "123";

            ShipmentsMod.Shipments.UpdateShipments(newShipments).Apply();

            ShipmentsView updateShipmentsView = await ShipmentsMod.Shipments.Query().GetViewById(newShipments.ShipmentId);

            Assert.Same(updateShipmentsView.TrackingNumber, "123");

            //List<ShipmentsDetail> newShipmentsDetails = await ShipmentsMod.ShipmentsDetail.Query().MapToEntity(newShipmentView.ShipmentsDetailViews);

            newShipmentsDetails.ForEach(m => m.ShipmentId = lookupShipments.ShipmentId);

            foreach (var item in newShipmentsDetails)
            {
                NextNumber nn = await ShipmentsMod.ShipmentsDetail.Query().GetNextNumber();
                item.ShipmentDetailNumber = nn.NextNumberValue;
            }
     
            //List<ShipmentsDetail> ShipmentsDetails = await ShipmentsMod.ShipmentsDetail.Query().MapToEntity(detailViews);

            ShipmentsMod.ShipmentsDetail.AddShipmentsDetails(newShipmentsDetails).Apply();
            
            newShipmentsDetails.ForEach(m => m.AmountShipped = 10);

            ShipmentsMod.ShipmentsDetail.UpdateShipmentsDetails(newShipmentsDetails).Apply();

            //Test Paging

            lssWebApi2.AbstractFactory.PageListViewContainer<ShipmentsView> container = await ShipmentsMod.Shipments.Query().GetViewsByPage(predicate: e => e.Amount==10, order: e => e.Amount, pageSize: 1, pageNumber: 1);

            Assert.True(container.Items.Count > 0);


            List<ShipmentsDetail> listShipmentDetails = await ShipmentsMod.ShipmentsDetail.Query().GetEntitiesByShipmentId(newShipments.ShipmentId);

            List<ShipmentsDetailView> listShipmentDetailViews= await ShipmentsMod.ShipmentsDetail.Query().GetViewsByShipmentId(newShipments.ShipmentId);

            Assert.True(listShipmentDetails.Any(m => m.Note.Contains("Updated")));

            ShipmentsMod.ShipmentsDetail.DeleteShipmentsDetails(listShipmentDetails).Apply();

            ShipmentsMod.Shipments.DeleteShipments(newShipments).Apply();

            Shipments lookup2Shipments = await ShipmentsMod.Shipments.Query().GetEntityById(newShipments.ShipmentId);

            Assert.Null(lookup2Shipments);

            /**********************remove sales Order detail******************************/
        
            List<SalesOrderDetailView> solistDetailViews = await salesOrderMod.SalesOrderDetail.Query().GetDetailViewsBySalesOrderId(newSalesOrder.SalesOrderId);

            Assert.True(soListDetails.Any(m => m.Description.Contains("Updated")));

            salesOrderMod.SalesOrderDetail.DeleteSalesOrderDetails(soListDetails).Apply();

            salesOrderMod.SalesOrder.DeleteSalesOrder(newSalesOrder).Apply();

            SalesOrder lookupSalesOrder = await salesOrderMod.SalesOrder.Query().GetEntityById(view.SalesOrderId);

            Assert.Null(lookupSalesOrder);
        }
        [Fact]
        public async Task TestShipmentsView()
        {
            ShipmentsModule invMod = new ShipmentsModule();

            long ShipmentsId = 21;
            ShipmentsView view = await invMod.Shipments.Query().GetViewById(ShipmentsId);

            Assert.NotNull(view);

        }
    }
}
