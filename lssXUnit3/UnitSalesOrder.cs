using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SalesOrderManagementDomain;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

/*
 
 1. Sales order fully documents what the customer wants to buy
2. Sales orders are later analyzed to gain information about the customer
3. An invoice includes details and costs and unpaid balance of the sale
4. A sales order includes valueable information for those preparing the delivery.
*/
namespace lssXUnit3
{
    public class UnitSalesOrder
    {
        private readonly ITestOutputHelper output;



        public UnitSalesOrder(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestGetSalesOrder()
        {
            SalesOrderModule soMod = new SalesOrderModule();
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
            NextNumber nnSalesOrder = await salesOrderMod.SalesOrder.Query().GetSalesOrderNextNumber();

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

            SalesOrder SalesOrder = await salesOrderMod.SalesOrder.Query().MapToSalesOrderEntity(view);

            salesOrderMod.SalesOrder.AddSalesOrder(SalesOrder).Apply();

            SalesOrder newSalesOrder = await salesOrderMod.SalesOrder.Query().GetSalesOrderByNumber(view.OrderNumber);

            Assert.NotNull(newSalesOrder);

            newSalesOrder.Note = "sales order note test";

            SalesOrderView newSalesOrderView = await salesOrderMod.SalesOrder.Query().MapToSalesOrderView(newSalesOrder);

            salesOrderMod.SalesOrder.UpdateSalesOrder(newSalesOrder).Apply();

            SalesOrderView updateView = await salesOrderMod.SalesOrder.Query().GetSalesOrderViewById(newSalesOrder.SalesOrderId);

            Assert.Same(updateView.Note, "sales order note test");
            
            detailViews.ForEach(m => m.SalesOrderId = newSalesOrder.SalesOrderId);

            List<SalesOrderDetail> salesOrderDetails = await salesOrderMod.SalesOrderDetail.Query().MapToSalesOrderDetailEntity(detailViews);

            salesOrderMod.SalesOrderDetail.AddSalesOrderDetails(salesOrderDetails).Apply();

            salesOrderDetails.ForEach(m => m.Description += " Updated");

            salesOrderMod.SalesOrderDetail.UpdateSalesOrderDetails(salesOrderDetails).Apply();

            List<SalesOrderDetail> listDetails = await salesOrderMod.SalesOrderDetail.Query().GetDetailsBySalesOrderId(newSalesOrder.SalesOrderId);

            List<SalesOrderDetailView> listDetailViews= await salesOrderMod.SalesOrderDetail.Query().GetDetailViewsBySalesOrderId(newSalesOrder.SalesOrderId);

            Assert.True(listDetails.Any(m => m.Description.Contains("Updated")));

            salesOrderMod.SalesOrderDetail.DeleteSalesOrderDetails(listDetails).Apply();

            salesOrderMod.SalesOrder.DeleteSalesOrder(newSalesOrder).Apply();
            SalesOrder lookupSalesOrder = await salesOrderMod.SalesOrder.Query().GetSalesOrderById(view.SalesOrderId);

            Assert.Null(lookupSalesOrder);
        }
        [Fact]
        public async Task TestSalesOrderView()
        {
            SalesOrderModule invMod = new SalesOrderModule();

            long SalesOrderId = 21;
            SalesOrderView view = await invMod.SalesOrder.Query().GetSalesOrderViewById(SalesOrderId);

            Assert.NotNull(view);

        }
    }
}
