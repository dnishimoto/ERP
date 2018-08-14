using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using MillenniumERP.AddressBookDomain;

namespace ERP_Core2.AddressBookDomain
{
    public class UnitTestAddressBook
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestAddressBook(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestGetBuyerByBuyerId()
        {
            int buyerId = 1;

            AddressBookModule abMod = new AddressBookModule();

            BuyerView buyerView = await abMod.GetBuyerByBuyerId(buyerId);
            Assert.Equal("Regional Purchasing Clerk",buyerView.BuyerTitle);
        }
        [Fact]
        public async Task TestGetCarrierByCarrierId()
        {
            int carrierId = 1;

            AddressBookModule abMod = new AddressBookModule();

            CarrierView carrierView = await abMod.GetCarrierByCarrierId(carrierId);
          
            Assert.Equal("United Parcel Service",carrierView.CarrierName.ToString());
        }

        [Fact]
        public async Task   TestGetSupplierBySupplierId()
        {
            int supplierId = 1;
            AddressBookModule abMod = new AddressBookModule();
            SupplierView supplierView = await abMod.GetSupplierBySupplierId(supplierId);
           Assert.True(supplierView.SupplierId != null);

        }
        [Fact]
        public async Task TestGetEmployeeByEmployeeId()
        {
            int employeeId = 3;
            AddressBookModule abMod = new AddressBookModule();
            EmployeeView employeeView =await abMod.GetEmployeeByEmployeeId(employeeId);
            Assert.True(employeeView.EmployeeId != null);
        }
        [Fact]
        public void TestGetEmployeesBySupervisorId()
        {
            
            int supervisorId = 1;
            AddressBookModule abMod = new AddressBookModule();

            List<EmployeeView> list =  abMod.GetEmployeesBySupervisorId(supervisorId);

         
            Assert.True(list.Count>0);
            
        }
        [Fact]
        public async Task TestGetSupervisorBySupervisorId()
        {
            int supervisorId = 1;
            AddressBookModule abMod = new AddressBookModule();
            SupervisorView view = await abMod.GetSupervisorBySupervisorId(supervisorId);

            Assert.Equal(view.ParentSupervisorName.ToUpper().ToString() , "PAM NISHIMOTO".ToString());
        }
        [Fact]
        public void TestGetPhonesByAddressId()
        {
            long addressId = 1;
            AddressBookModule abMod = new AddressBookModule();

            List<Phone> list =  abMod.GetPhonesByAddressId(addressId);


            List<string> intCollection = new List<string>();
            foreach (var item in list)
            {
                output.WriteLine($"{item.PhoneNumber}");
                intCollection.Add(item.PhoneNumber);

            }
            bool results = intCollection.Any(s => s.Contains("401-4333"));
            Assert.True(results);

        }
        [Fact]
        public void TestGetEmailsByAddressId()
        {
            long addressId = 1;
            AddressBookModule abMod = new AddressBookModule();

            List<Email> list =  abMod.GetEmailsByAddressId(addressId);

            List<string> intCollection = new List<string>();
            foreach (var item in list)
            {
                output.WriteLine($"{item.Email1}");
                intCollection.Add(item.Email1);
       
            }
            bool results = intCollection.Any(s => s.Contains("dnishimoto@listensoftware.com"));
            Assert.True(results);

        }
        
        private void DoSomething1(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                output.WriteLine("Do Something one");
                Thread.Sleep(1);
            }

        }
        private void DoSomething2(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                output.WriteLine("Do Something two");
                Thread.Sleep(2);
            }
        }
        //The state.Break() exist out of the Parallel.ForEach loop when the 
        //application sets the cancelForEach boolean flag.
        private void ProcessUsingParallelForEach(List<string> intCollection)
        {

            Parallel.ForEach(intCollection, (integer, state) =>
            {
                output.WriteLine($"Parallel.Foreach={integer}");
                if (cancelForEach == true)
                {
                    output.WriteLine($"Exit Parallel.ForEach {integer}");
                    state.Break();

                }

            });

        }
        private bool cancelForEach = false;
        [Fact]
        public void TestThreadPooling()
        {
            int numberOfProcessors = Environment.ProcessorCount;

            Parallel.Invoke(() => DoSomething1(5)
                        , () => DoSomething2(10));

            List<string> intCollection = new List<string>();

            for (int i = 0; i < 500; i++)
            {
                intCollection.Add(i.ToString());
            }

            var TaskForEach = Task.Run(() => ProcessUsingParallelForEach(intCollection));

            Thread.Sleep(500);
            cancelForEach = true;

            Task.WaitAll(TaskForEach);

        }
        [Fact]
        public void TestGetAddressBooks()
        {
            //int addressId = 1;

            //UnitOfWork unitOfWork = new UnitOfWork();
            AddressBookModule abMod = new AddressBookModule();

            Task<List<AddressBook>> resultTask = Task.Run<List<AddressBook>>(async () => await abMod.GetAddressBookByName("David"));

            IList<string> list = new List<string>();
            foreach (var item in resultTask.Result)
            {
                output.WriteLine($"{item.Name}");
                list.Add(item.Name.ToUpper());
            }
            Assert.True(list.Contains("DAVID NISHIMOTO") );
        }
        [Fact]
        public void TestGetAddressBook()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Task<AddressBook> resultTask = Task.Run<AddressBook>(async () => await unitOfWork.addressBookRepository.GetObjectAsync(1));

            output.WriteLine($"{resultTask.Result.FirstName}");

            Assert.Equal("David",resultTask.Result.FirstName);
        }
        [Fact]
        public async Task TestUpdateAddressBook()
        {
            long addressId = 1;

            AddressBookModule abMod = new AddressBookModule();

            AddressBook addressBook = await abMod.GetAddressBookByAddressId(addressId);
            //UnitOfWork unitOfWork = new UnitOfWork();
            //Task<AddressBook> resultTask = Task.Run<AddressBook>(async () => await unitOfWork.addressBookRepository.GetObjectAsync(1));

            //AddressBook addressBook = resultTask.Result;

            addressBook.FirstName = "David2";
            bool results =  abMod.UpdateAddressBook(addressBook);

           
            //unitOfWork.addressBookRepository.UpdateObject(addressBook);
            //unitOfWork.CommitChanges();

            AddressBook addressBook2 = await abMod.GetAddressBookByAddressId(addressId);

            string name = addressBook2.FirstName;

            Assert.Equal("David2",name );

            //addressBook = resultTask.Result;
            addressBook2.FirstName = "David";
            results =  abMod.UpdateAddressBook(addressBook);
            //unitOfWork.addressBookRepository.UpdateObject(addressBook2);
            //unitOfWork.CommitChanges();

            AddressBook addressBook3 = await abMod.GetAddressBookByAddressId(addressId);


            name = addressBook3.FirstName;

            Assert.Equal("David",name);
        }
        [Fact]
        public void TestAddandDeleteAddressBook()
        {
            //UnitOfWork unitOfWork = new UnitOfWork();
            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = "James";
            addressBook.LastName = "Dean";
            addressBook.Name = "James Dean";
    

            AddressBookModule abMod = new AddressBookModule();
            bool result =  abMod.CreateAddressBook(addressBook);

            IQueryable<AddressBook> query =  abMod.GetAddressBooksByExpression(a => a.Name == "James Dean");
            //IQueryable<AddressBook> query = unitOfWork.addressBookRepository.GetObjectsAsync();

            foreach (var item in query)
            {
                Assert.Equal("James Dean",item.Name );

                //unitOfWork.addressBookRepository.DeleteObject(item);
                result = abMod.DeleteAddressBook(item);
            }
            //unitOfWork.CommitChanges();

            Assert.True(result);
           

        }
        [Fact]
        public void TestDeleteAddressBooks()
        {
            List<AddressBook> list = new List<AddressBook>();
            //UnitOfWork unitOfWork = new UnitOfWork();
            AddressBookModule abMod = new AddressBookModule();

            for (int i = 1; i < 10; i++)
            {
                AddressBook addressBook = new AddressBook();
                addressBook.Name = "Test" + i.ToString();
                list.Add(addressBook);

            }
            bool result = abMod.CreateAddressBooks(list);
  

            IQueryable<AddressBook> query =  abMod.GetAddressBooksByExpression(a => a.Name.Contains("Test"));

            list.Clear();

            foreach (var item in query)
            {
                list.Add(item);
            }

            result = abMod.DeleteAddressBooks(list);


            Assert.True(result);

        }
    }
}
