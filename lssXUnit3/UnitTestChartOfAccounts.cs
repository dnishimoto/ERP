using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace ERP_Core2.ChartOfAccountsDomain
{
    
       public class UnitTestChartOfAccounts
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestChartOfAccounts(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestGetAccountsByIds()
        {
            long [] acctIds = { 3, 4, 5 };
            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            List<ChartOfAccountView> list = coaMod.ChartOfAccount.Query().GetChartOfAccountViewsByIds(acctIds);
            if (list.Count > 0)
            { Assert.True(true); }
        }
        [Fact]
        public void TestCreateAccountModel()
        {

            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            coaMod.ChartOfAccount.CreateChartOfAccountModel().Apply();

            Assert.True(true);
        }
     
    }
}
