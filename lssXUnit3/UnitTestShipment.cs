using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ShipmentsDomain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using lssWebApi2.SalesOrderDomain;
using lssWebApi2.AbstractFactory;
using lssWebApi2.TaxRatesByCodeDomain;
using lssWebApi2.SalesOrderDetailDomain;

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
                CompanyCode ="1000",
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

            List<SalesOrderDetail> salesOrderDetails = (await salesOrderMod.SalesOrderDetail.Query().MapToEntity(detailViews)).ToList<SalesOrderDetail>();

            salesOrderMod.SalesOrderDetail.AddSalesOrderDetails(salesOrderDetails).Apply();

            salesOrderDetails.ForEach(m => m.Description += " Updated");

            salesOrderMod.SalesOrderDetail.UpdateSalesOrderDetails(salesOrderDetails).Apply();


            List<SalesOrderDetail> soListDetails = (await salesOrderMod.SalesOrderDetail.Query().GetDetailsBySalesOrderId(newSalesOrder.SalesOrderId)).ToList<SalesOrderDetail>();


            /************************************Shipments**************************/
            ShipmentModule ShipmentMod = new ShipmentModule();
            NextNumber nnShipment = await ShipmentMod.Shipment.Query().GetNextNumber();

            ShipmentView shipmentCreation = new ShipmentView() {
                SalesOrderId = newSalesOrder.SalesOrderId,
                ActualWeight = 100,
                BillableWeight = 100,
                ShippedFromLocationId = 3,
                ShippedToLocationId = 9,
                TrackingNumber = "123",
                WeightUOM = "LBS",
                ShipmentDate = DateTime.Now,
                CarrierId = 2,
                ShipmentNumber = nnShipment.NextNumberValue

            };
            shipmentCreation.ItemsAdjustedQuantityShipped =
                new List<ItemsAdjustedQuantityShippedStruct>();
            foreach (var item in soListDetails)
            {
                var newItem = new ItemsAdjustedQuantityShippedStruct() {
                    SalesOrderDetailId = item.SalesOrderDetailId,
                    AdjustedQuantityShipped = item.Quantity ?? 0,
                    AdjustedAmountShipped = item.Amount
                };
                shipmentCreation.ItemsAdjustedQuantityShipped.Add(newItem);
            }


            bool result = await ShipmentMod.CreateBySalesOrder(shipmentCreation);

            Shipment lookupShipment = await ShipmentMod.Shipment.Query().GetEntityByNumber(shipmentCreation.ShipmentNumber);

            lookupShipment.TrackingNumber = "123";

            ShipmentMod.Shipment.UpdateShipment(lookupShipment).Apply();

            ShipmentView updateShipmentsView = await ShipmentMod.Shipment.Query().GetViewById(lookupShipment.ShipmentId);

            Assert.Same(updateShipmentsView.TrackingNumber, "123");

            List<ShipmentDetail> listShipmentDetail = (await ShipmentMod.ShipmentDetail.Query().GetEntitiesByShipmentId(lookupShipment.ShipmentId)).ToList<ShipmentDetail>();

            listShipmentDetail.ForEach(m => m.AmountShipped = 10);
            listShipmentDetail.ForEach(m => m.QuantityShipped = 1);

            ShipmentMod.ShipmentDetail.UpdateShipmentDetails(listShipmentDetail).Apply();

            //Test Paging

            //lssWebApi2.AbstractFactory.PageListViewContainer<ShipmentView> container = await ShipmentMod.Shipment.Query().GetViewsByPage(predicate: e => e.TrackingNumber == "123", order: e => e.Amount, pageSize: 1, pageNumber: 1);

            //Assert.True(container.Items.Count > 0);


            List<ShipmentDetail> listShipmentDetails = (await ShipmentMod.ShipmentDetail.Query().GetEntitiesByShipmentId(lookupShipment.ShipmentId)).ToList<ShipmentDetail>();

            IList<ShipmentDetailView> listShipmentDetailViews = await ShipmentMod.ShipmentDetail.Query().GetViewsByShipmentId(lookupShipment.ShipmentId);

            ShipmentMod.ShipmentDetail.DeleteShipmentDetails(listShipmentDetails).Apply();

            ShipmentMod.Shipment.DeleteShipment(lookupShipment).Apply();

            Shipment lookup2Shipments = await ShipmentMod.Shipment.Query().GetEntityById(lookupShipment.ShipmentId);

            Assert.Null(lookup2Shipments);

            /**********************remove sales Order detail******************************/

            IList<SalesOrderDetailView> solistDetailViews = await salesOrderMod.SalesOrderDetail.Query().GetDetailViewsBySalesOrderId(newSalesOrder.SalesOrderId);

            if (solistDetailViews.ToList<SalesOrderDetailView>().Any(m => m.Description.Contains("Updated")) == false) { Assert.True(false); }

            salesOrderMod.SalesOrderDetail.DeleteSalesOrderDetails(soListDetails).Apply();

            salesOrderMod.SalesOrder.DeleteSalesOrder(newSalesOrder).Apply();

            SalesOrder lookupSalesOrder = await salesOrderMod.SalesOrder.Query().GetEntityById(view.SalesOrderId);

            Assert.Null(lookupSalesOrder);
        }
        [Fact]
        public async Task TestShipmentsView()
        {
            ShipmentModule invMod = new ShipmentModule();

            long ShipmentsId = 21;
            ShipmentView view = await invMod.Shipment.Query().GetViewById(ShipmentsId);

            Assert.NotNull(view);

        }
        [Fact]
        public async Task TestShipmentPaging()
       {
            ShipmentModule ShipmentMod = new ShipmentModule();
            lssWebApi2.AbstractFactory.PageListViewContainer<ShipmentView> container = await ShipmentMod.Shipment.Query().GetViewsByPage(predicate: e => e.TrackingNumber == "123", order: e => e.Amount, pageSize: 1, pageNumber: 1);
            Assert.True(container.Items.Count > 0);
        }


    }
}
