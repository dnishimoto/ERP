using lssWebApi2.BuyerDomain;
using lssWebApi2.CarrierDomain;
using lssWebApi2.Services;
using lssWebApi2.SupervisorDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.AddressBookDomain
{
    public class UnitTestAddressBook
    {
        private readonly ITestOutputHelper output;

        [Fact]
        public async Task TestAddressBookPaging()
        {
            long addressId = 1;
            AddressBookModule abMod = new AddressBookModule();

            PageListViewContainer<AddressBookView> container = await abMod.AddressBook.Query().GetViewsByPage(predicate: e => e.AddressId == addressId, order: e => e.AddressId, pageSize: 1, pageNumber: 1);

            Assert.True(container.TotalItemCount > 0);
        }
        public UnitTestAddressBook(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestGetBuyerByBuyerId()
        {
            int buyerId = 1;

            AddressBookModule abMod = new AddressBookModule();

            BuyerView buyerView = await abMod.Buyer.Query().GetViewById(buyerId);
   
            Assert.Equal("Regional Purchasing Clerk",buyerView.Title);
        }
        [Fact]
        public async Task TestGetCarrierByCarrierId()
        {
            int carrierId = 1;

            AddressBookModule abMod = new AddressBookModule();
            CarrierView carrierView = await abMod.Carrier.Query().GetViewById(carrierId);
              
            Assert.Equal("United Parcel Service",carrierView.CarrierName.ToString());
        }

        [Fact]
        public async Task  TestGetSupplierBySupplierId()
        {
            int supplierId = 1;
            AddressBookModule abMod = new AddressBookModule();
            SupplierView supplierView = await abMod.Supplier.Query().GetViewById(supplierId);

           Assert.True(supplierView != null);

        }
      
        [Fact]
        public async Task TestGetSupervisorBySupervisorId()
        {
            int supervisorId = 1;
            AddressBookModule abMod = new AddressBookModule();
            SupervisorView view = await abMod.Supervisor.Query().GetViewById(supervisorId);

            Assert.Equal(view.ParentSupervisorName.ToUpper().ToString() , "PAM NISHIMOTO".ToString());
        }
        [Fact]
        public async Task TestGetPhonesByAddressId()
        {
            long addressId = 1;
            AddressBookModule abMod = new AddressBookModule();
            IList<PhoneEntity> list = await abMod.Phone.Query().GetPhonesByAddressId(addressId);
  
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
        public async Task TestGetEmailsByAddressId()
        {
            long addressId = 1;
            AddressBookModule abMod = new AddressBookModule();
            IList<EmailEntity> list = await abMod.Email.Query().GetEmailsByAddressId(addressId);
      
            List<string> intCollection = new List<string>();
            foreach (var item in list)
            {
                output.WriteLine($"{item.Email}");
                intCollection.Add(item.Email);
       
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
        public async Task TestGetAddressBooks()
        {
            AddressBookModule abMod = new AddressBookModule();
            List<AddressBookView> addressBooks=await abMod.AddressBook.Query().GetAddressBookByName("David");
     
            IList<string> list = new List<string>();
            foreach (var item in addressBooks)
            {
                output.WriteLine($"{item.Name}");
                list.Add(item.Name.ToUpper());
            }
            Assert.True(list.Contains("DAVID NISHIMOTO") );
        }
       
        [Fact]
        public async Task TestUpdateAddressBook()
        {
            long addressId = 1;

            AddressBookModule abMod = new AddressBookModule();

            AddressBookView addressBookView = new AddressBookView();
            addressBookView.AddressId = addressId;
            addressBookView.FirstName = "David2";
            addressBookView.LastName = "Nishimoto";
            addressBookView.Name = "David Nishimoto";

            abMod.AddressBook.UpdateAddressBookByView(addressBookView).Apply();

            AddressBook addressBook2=await abMod.AddressBook.Query().GetEntityById(addressId);
    
            string name = addressBook2.FirstName;

            Assert.Equal("David2",name );

            addressBook2.FirstName = "David";
            abMod.AddressBook.UpdateAddressBook(addressBook2).Apply();


            AddressBook addressBook3 = await abMod.AddressBook.Query().GetEntityById(addressId);

           name = addressBook3.FirstName;

            Assert.Equal("David",name);
        }
        [Fact]
        public void TestAddandDeleteAddressBook()
        {
             AddressBook addressBook = new AddressBook();
            addressBook.FirstName = "James";
            addressBook.LastName = "Dean";
            addressBook.Name = "James Dean";
    

            AddressBookModule abMod = new AddressBookModule();
            abMod.AddressBook.AddAddressBook(addressBook).Apply();
            IQueryable<AddressBook> query =abMod.AddressBook.Query().GetAddressBooksByExpression(a => a.Name == "James Dean");
      
            foreach (var item in query)
            {
                Assert.Equal("James Dean",item.Name );


                abMod.AddressBook.DeleteAddressBook(item);
            }
            abMod.AddressBook.Apply(); //Avoid the thread problem



            Assert.True(true);
           

        }
        [Fact]
        public void TestDeleteAddressBooks()
        {
            List<AddressBook> list = new List<AddressBook>();
            AddressBookModule abMod = new AddressBookModule();

            for (int i = 1; i < 10; i++)
            {
                AddressBook addressBook = new AddressBook();
                addressBook.Name = "Test" + i.ToString();
                list.Add(addressBook);

            }
            abMod.AddressBook.AddAddressBooks(list).Apply();

             list[0].CompanyName = "test";
       
            abMod.AddressBook.UpdateAddressBook(list[0]).Apply();

            IQueryable<AddressBook> query=abMod.AddressBook.Query().GetAddressBooksByExpression(a => a.Name.Contains("Test"));

            list.Clear();

            foreach (var item in query)
            {
                list.Add(item);
            }

            abMod.AddressBook.DeleteAddressBooks(list).Apply();


            Assert.True(true);

        }
    }
}
