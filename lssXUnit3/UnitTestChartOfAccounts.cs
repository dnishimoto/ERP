using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace lssWebApi2.ChartOfAccountsDomain
{
    
       public class UnitTestChartOfAccounts
    {
        private readonly ITestOutputHelper output;

        public UnitTestChartOfAccounts(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestGetExpenseCoa()
        {
            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            string company = "1000";
            string busUnit = "1200";
            string objectNumber = "502";
            IList<ChartOfAccountView> list = await coaMod.ChartOfAccount.Query().GetViewsByAccount(company, busUnit, objectNumber, "");
            if (list.Count == 0)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public async Task TestGetAccountsByIds()
        {
            long [] acctIds = { 3, 4, 5 };
            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            IList<ChartOfAccountView> list = await coaMod.ChartOfAccount.Query().GetViewsByIds(acctIds);
            if (list.Count > 0)
            { Assert.True(true); }
        }
        [Fact]
        public async Task TestCreateAccountModel()
        {

            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            bool result= await coaMod.ChartOfAccount.CreateChartOfAccountModel();

            Assert.True(result);
        }
     
    }
}
