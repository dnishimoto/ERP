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

namespace ERP_Core2.CustomerDomain
{
    public class UnitTestCustomer
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestCustomer(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestCustomerClaimsByCustomerId()
        {
            int customerId = 2;
            UnitOfWork unitOfWork = new UnitOfWork();
            IList<CustomerClaimView> list = unitOfWork.customerRepository.GetCustomerClaimsByCustomerId(customerId);
            List<string> collection = new List<string>();
            foreach (CustomerClaimView customerClaimView in list)
            {
                output.WriteLine($"{customerClaimView.GroupId}");
                collection.Add(customerClaimView.GroupId.ToUpper());
            }
            Assert.True(collection.Contains("IDAHO WEB DEVELOPMENT CUSTOMERS"));
        }
        [Fact]
        public void TestInvoicesByCustomerId()
        {
            int customerId = 3;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<InvoiceView> list = unitOfWork.customerRepository.GetInvoicesByCustomerId(customerId);
            List<string> collection = new List<string>();
            foreach (var item in list)
            {
                foreach (InvoiceDetailView invoiceDetailView in item.InvoiceViewDetails)
                {
                    output.WriteLine($"{invoiceDetailView.ItemDescription}");
                    collection.Add(invoiceDetailView.ItemDescription.ToUpper());
                }
            }

            Assert.True(collection.Contains("GRASS"));
        }
    }
}
