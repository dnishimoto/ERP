using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using ERP_Core2.AddressBookDomain;

namespace ERP_Core2.AddressBookDomain
{
    public class UnitUDC
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        List<string> intCollection = new List<string>();

        public UnitUDC(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestGetUDCValues()
        {
            string productCode = "JOBCODE";
            UnitOfWork unitOfWork = new UnitOfWork();
            Task<IQueryable<UDC>> query = unitOfWork.udcRepository.GetUDCValuesByProductCode(productCode);
            foreach (var item in query.Result)
            {
                output.WriteLine($"{item.Value}");
                intCollection.Add(item.Value);
            }
            bool results = intCollection.Any(s => s.Contains("IT Manager"));
            Assert.True(results);
        }
    
    }
}
