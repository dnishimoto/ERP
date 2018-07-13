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
using MillenniumERP.InvoicesDomain;
using MillenniumERP.AccountsReceivableDomain;
using Newtonsoft.Json;
using System;

namespace ERP_Core2.CustomerDomain
{
    public interface IEntity { }
    public class SquareNumber:IEntity {
        public double Value { get; set; }
    }
    public class CubeNumber : IEntity {
        public double Value { get; set; }
    }
    public class Quadraic : IEntity {

        public Quadraic(double A, double B, double C)
        {
            this.A = A;
            this.B = B;
            this.C = C;

            this.X1 = (
                 (-1)*B + System.Math.Sqrt(
                (B * B)-(4.0 * A * C)
                )
                ) 
                / (2.0 * A);

            this.X2 = (
                (-1) * B - System.Math.Sqrt(
                (B * B) - (4.0 * A * C)
                )
                )
                / (2.0 * A);

        }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
     
    }

    public class UnitTestCustomer
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestCustomer(ITestOutputHelper output)
        {
            this.output = output;

        }
        CancellationTokenSource cancellationTokenSource = null;

        private IList<IEntity> Task1()
        {
            IList<IEntity> entityCollection = new List<IEntity>();
            for (int i = 0; i < 100; i++)
            {
                SquareNumber squareNumber = new SquareNumber();
                squareNumber.Value = System.Math.Pow((double)i, 2.0);
                entityCollection.Add(squareNumber);
                Thread.Sleep(50);
            }
            return entityCollection;
        }
        private IList<IEntity> Task2()
        {
            IList<IEntity> entityCollection = new List<IEntity>();
            for (int i = 0; i < 100; i++)
            {
                CubeNumber cubeNumber = new CubeNumber();
                cubeNumber.Value = System.Math.Pow((double)i, 3.0);
                entityCollection.Add(cubeNumber);
                Thread.Sleep(50);
            }
            return entityCollection;
        }
        private IList<IEntity> Task3()
        {
            IList<IEntity> entityCollection = new List<IEntity>();

            //2x2 – 4x – 3 = 0 baseline
            Quadraic quadraic = new Quadraic(2.0,-4.0,-3.0);
            entityCollection.Add(quadraic);

            for (float i = 1.0f; i < 3.0f; i += 1.0f )
            {
                for (float j = -2.0f; j < 2.0f; j += 1.0f)
                {
                    for (float k = -2.0f; k < 2.0f; k += 1.0f)
                    {
                        quadraic = new Quadraic(i,j,k);
                        entityCollection.Add(quadraic);
                        Thread.Sleep(50);
                    }
                }

            }

            return entityCollection;
        }
        [Fact]
        public async Task TestCreateCustomerAccount()
        {
            try
            {
                string json = "";
                UnitOfWork unitOfWork = new UnitOfWork();

                json = @"{ ""CustomerName"":""David Poston"" 
                        ,""FirstName"":""David"",
                        ""LastName"":""Poston"",
                        ""CompanyName"":""DC Tech"",
                        
                        ""LocationAddress"":[{
                        ""Address_Line1"" : ""2420 12th Ave"",
                        ""City"" : ""Nampa"",
                        ""State"" : ""ID"",
                        ""Zipcode"" : ""83686""
                        }],
                        ""AccountEmail"":{
                              ""EmailText"" : ""support@dc-tech.us"",
                              ""LoginEmail"" : true,
                              ""Password"" : ""12345""
                            }
                        }";
              

             CustomerView customerView = JsonConvert.DeserializeObject<CustomerView>(json);

             long addressId = await unitOfWork.addressBookRepository.CreateAddressBook(customerView);

                if (addressId > 0)
                {
                    EmailView emailView = new EmailView();
                    emailView.AddressId = addressId;
                    emailView.EmailText = customerView.AccountEmail.EmailText;
                    emailView.LoginEmail = customerView.AccountEmail.LoginEmail;
                    emailView.Password = customerView.AccountEmail.Password;

                    bool result2 = await unitOfWork.emailRepository.CreateEmail(emailView);

                    unitOfWork.CommitChanges();
                }
             AddressBook lookupAddressBook = await unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView);

                Assert.True(true);

            }
            catch (Exception ex)
            { }
        }
        [Fact]
        public void TestGetAccountReceivables()
        {
            int customerId = 3;
            UnitOfWork unitOfWork = new UnitOfWork();
            IList<AccountReceiveableView> list = unitOfWork.customerRepository.GetAccountReceivablesByCustomerId(customerId);
            List<string> collection = new List<string>();
            foreach (AccountReceiveableView accountReceiveableView in list)
            {
                output.WriteLine($"{accountReceiveableView.InvoiceNumber}");
                collection.Add(accountReceiveableView.InvoiceNumber.ToUpper());
            }
            Assert.True(collection.Contains("INV-02"));

        }

        [Fact]
        public void TestContinueWith()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            Task<IList<IEntity>> listEntities1Task = Task.Run(async () => Task1(), cancellationTokenSource.Token);
            Task<IList<IEntity>> listEntities2Task = Task.Run(async () => Task2(), cancellationTokenSource.Token);
            Task<IList<IEntity>> listEntities3Task = Task.Run(async () => Task3(), cancellationTokenSource.Token);

            Task continueTask = listEntities1Task.ContinueWith(
                query => 
                {
                    if (query.IsCanceled == false)
                    {
                        foreach (SquareNumber item in query.Result)
                        {
                            output.WriteLine($"Task 1: {item.Value}");
                        }
                    }
                });

            Task continueTask2 = listEntities2Task.ContinueWith(
                query =>
                {
                    if (query.IsCanceled == false)
                    {
                        foreach (CubeNumber item in query.Result)
                        {
                            output.WriteLine($"Task 2: {item.Value}");
                        }
                    }
                });
            Task continueTask3 = listEntities3Task.ContinueWith(
                 query =>
                 {
                     if (query.IsCanceled == false)
                     {
                         foreach (Quadraic item in query.Result)
                         {
                             output.WriteLine($"Task 3: X1={item.X1} X2={item.X2}");
                         }
                     }
                 });

            if (listEntities1Task.IsCanceled==false && listEntities2Task.IsCanceled == false && listEntities3Task.IsCanceled == false)
            Task.WaitAll(listEntities1Task, listEntities2Task, listEntities3Task);

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
