using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SalesOrderManagementDomain;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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
            SalesOrderModule SalesOrderMod = new SalesOrderModule();

            Udc orderType = await SalesOrderMod.SalesOrder.Query().GetUdc("ORDER_TYPE", SalesOrderEnum.CASH_SALES.ToString());
            Udc paymentTerms = await SalesOrderMod.SalesOrder.Query().GetUdc("PAYMENTTERMS", PaymentTermsEnum.Net_2_10_30.ToString());
            NextNumber nnSalesOrder = await SalesOrderMod.SalesOrder.Query().GetSalesOrderNextNumber();
          
            SalesOrderView view = new SalesOrderView()
            {
                Taxes =0,
                Amount =0,
                OrderType =orderType.KeyCode.ToString(),
                CustomerId =2,
                TakenBy ="David Nishimoto",
                FreightAmount =0,
                PaymentInstrument="Check" ,
                PaymentTerms =paymentTerms.KeyCode.ToString()
            };


            view.OrderNumber = nnSalesOrder.NextNumberValue.ToString();

            SalesOrder SalesOrder = await SalesOrderMod.SalesOrder.Query().MapToSalesOrderEntity(view);

            SalesOrderMod.SalesOrder.AddSalesOrder(SalesOrder).Apply();

            SalesOrder newSalesOrder = await SalesOrderMod.SalesOrder.Query().GetSalesOrderByNumber(view.OrderNumber);

            Assert.NotNull(newSalesOrder);

            newSalesOrder.Note = "sales order note test";

            SalesOrderMod.SalesOrder.UpdateSalesOrder(newSalesOrder).Apply();

            SalesOrderView updateView = await SalesOrderMod.SalesOrder.Query().GetSalesOrderViewById(newSalesOrder.SalesOrderId);

            Assert.Same(updateView.Note, "sales order note test");

            SalesOrderMod.SalesOrder.DeleteSalesOrder(newSalesOrder).Apply();
            SalesOrder lookupSalesOrder = await SalesOrderMod.SalesOrder.Query().GetSalesOrderById(view.SalesOrderId);

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
