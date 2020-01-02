using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SalesOrderDomain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using lssWebApi2.SalesOrderDetailDomain;
using lssWebApi2.AbstractFactory;

/*
 
 1. Sales order fully documents what the customer wants to buy
2. Sales orders are later analyzed to gain information about the customer
3. An invoice includes details and costs and unpaid balance of the sale
4. A sales order includes valueable information for those preparing the delivery.
*/
namespace lssXUnit3
{
    public class UnitTestSalesOrder
    {
        private readonly ITestOutputHelper output;



        public UnitTestSalesOrder(ITestOutputHelper output)
        {
            this.output = output;

        }

        [Fact]
        public void TestCurry()
        {
            Func<int, Func<int, int>> curriedAdd = x => y => x + y;
            int b = curriedAdd(2)(3);

            output.WriteLine($"{b} ");

        }

     
        [Fact]
        public async Task TestAddUpdatDeleteSalesOrder()
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
     
            List<SalesOrderDetail> salesOrderDetails = await salesOrderMod.SalesOrderDetail.Query().MapToEntity(detailViews);

            salesOrderMod.SalesOrderDetail.AddSalesOrderDetails(salesOrderDetails).Apply();

            salesOrderDetails.ForEach(m => m.Description += " Updated");

            salesOrderMod.SalesOrderDetail.UpdateSalesOrderDetails(salesOrderDetails).Apply();

            //Test Paging - TODO -figure out why paging causes a severed relationship between the order and the detail

            //SalesOrderViewContainer container = await salesOrderMod.SalesOrder.Query().GetViewsByPage(predicate: e => e.Note.Contains("test"), order: e => e.SalesOrderId, pageSize: 1, pageNumber: 1);

           // Assert.True(container.items.Count > 0);


            List<SalesOrderDetail> listDetails = (await salesOrderMod.SalesOrderDetail.Query().GetDetailsBySalesOrderId(newSalesOrder.SalesOrderId)).ToList<SalesOrderDetail>();

            List<SalesOrderDetailView> listDetailViews= (await salesOrderMod.SalesOrderDetail.Query().GetDetailViewsBySalesOrderId(newSalesOrder.SalesOrderId)).ToList<SalesOrderDetailView>();

            Assert.True(listDetails.Any(m => m.Description.Contains("Updated")));

            salesOrderMod.SalesOrderDetail.DeleteSalesOrderDetails(listDetails).Apply();

            salesOrderMod.SalesOrder.DeleteSalesOrder(newSalesOrder).Apply();

            SalesOrder lookupSalesOrder = await salesOrderMod.SalesOrder.Query().GetEntityById(view.SalesOrderId);

            Assert.Null(lookupSalesOrder);
        }
        [Fact]
        public async Task TestSalesOrderView()
        {
            SalesOrderModule invMod = new SalesOrderModule();

            long SalesOrderId = 21;
            SalesOrderView view = await invMod.SalesOrder.Query().GetViewById(SalesOrderId);

            Assert.NotNull(view);

        }
        [Fact]
        public async Task TestSalesOrderPaging()
        {
            SalesOrderModule salesOrderMod = new SalesOrderModule();
            long? salesOrderId = 69;

            PageListViewContainer<SalesOrderView>  container = await salesOrderMod.SalesOrder.Query().GetViewsByPage(predicate: e => e.SalesOrderId==salesOrderId, order: e => e.SalesOrderId, pageSize: 1, pageNumber: 1);

            Assert.True(container.TotalItemCount > 0);
        }
    }
}
