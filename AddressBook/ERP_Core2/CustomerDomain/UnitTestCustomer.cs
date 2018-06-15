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
        public void TestContractsByCustomerId()
        {
            int customerId = 2;
            int? contractId = 1;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<ContractView> list = unitOfWork.customerRepository.GetContractsByCustomerId(customerId, contractId);
            List<string> collection = new List<string>();
            foreach (var item in list)
            {
                output.WriteLine($"{item.CustomerName}");
                collection.Add(item.CustomerName.ToUpper());

            }
            Assert.True(collection.Contains("NED SCARISBRICK"));
        }
        [Fact]
        public void TestScheduleEventsByCustomerId()
        {
            int customerId = 1;
            int? serviceId = 3;
            //int? invoiceId = null;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<ScheduleEventView> list = unitOfWork.customerRepository.GetScheduleEventsByCustomerId(customerId, serviceId);
            List<string> collection = new List<string>();
            foreach (var item in list)
            {
                    output.WriteLine($"{item.CustomerName}");
                    collection.Add(item.CustomerName.ToUpper());
       
            }

            Assert.True(collection.Contains("PAM NISHIMOTO"));
        }
        [Fact]
        public void TestInvoicesByCustomerId()
        {
            int customerId = 2;
            int? invoiceId = 5;
            //int? invoiceId = null;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<InvoiceView> list = unitOfWork.customerRepository.GetInvoicesByCustomerId(customerId, invoiceId);
            List<string> collection = new List<string>();
            foreach (var item in list)
            {
                foreach (InvoiceDetailView invoiceDetailView in item.InvoiceViewDetails)
                {
                    output.WriteLine($"{invoiceDetailView.ItemDescription}");
                    collection.Add(invoiceDetailView.ItemDescription.ToUpper());
                }
            }

            Assert.True(collection.Contains("EMPTY"));
        }
    }
}
