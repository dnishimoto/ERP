using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using MillenniumERP.CustomerDomain;
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
        public void TestCreateAccountModel()
        {
            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            bool result = coaMod.CreateChartOfAccountModel();

            Assert.True(result);
        }
     
    }
}
