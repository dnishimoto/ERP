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
        public void TestGetCustomerClaimsByCustomerId()
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
        public void TestGetContractsByCustomerId()
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
        public void TestGetEmailByCustomerId()
        {
            int customerId = 3;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<EmailView> list = unitOfWork.customerRepository.GetEmailsByCustomerId(customerId);
            List<string> collection = new List<string>();

            foreach (var item in list)
            {
                output.WriteLine($"{item.EmailText}");
                collection.Add(item.EmailText.ToUpper());
            }
            Assert.True(collection.Contains("DNISHIMOTO@LISTENSOFTWARE.COM"));
        }
        [Fact]
        public void TestGetPhoneByCustomerId()
        {
            int customerId = 3;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<PhoneView> list = unitOfWork.customerRepository.GetPhonesByCustomerId(customerId);
            List<string> collection = new List<string>();

            foreach (var item in list)
            {
                output.WriteLine($"{item.PhoneNumber}");
                collection.Add(item.PhoneNumber.ToUpper());
            }
            Assert.True(collection.Contains("401-4333"));
        }
        [Fact]
        public void TestGetLocationAddressByCustomerId()
        {
            int customerId = 3;

            UnitOfWork unitOfWork = new UnitOfWork();

            IList<LocationAddressView> list = unitOfWork.customerRepository.GetLocationAddressByCustomerId(customerId);
            List<string> collection = new List<string>();

            foreach (var item in list)
            {
                output.WriteLine($"{item.City}");
                collection.Add(item.City.ToUpper());
            }
            Assert.True(collection.Contains("BOISE"));
        }
        [Fact]
        public void TestGetScheduleEventsByCustomerId()
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
        public void TestGetInvoicesByCustomerId()
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
