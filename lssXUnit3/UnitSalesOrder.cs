using lssWebApi2.SalesOrderManagementDomain;
using System;
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
    }
}
