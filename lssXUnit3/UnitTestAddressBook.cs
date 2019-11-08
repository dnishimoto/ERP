using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;

namespace ERP_Core2.AddressBookDomain
{
    public class UnitTestAddressBook
    {
        private readonly ITestOutputHelper output;

        public UnitTestAddressBook(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestGetBuyerByBuyerId()
        {
            int buyerId = 1;

            AddressBookModule abMod = new AddressBookModule();

            BuyerView buyerView = abMod.AddressBook.Query().GetBuyerByBuyerId(buyerId);
   
            Assert.Equal("Regional Purchasing Clerk",buyerView.BuyerTitle);
        }
        [Fact]
        public void TestGetCarrierByCarrierId()
        {
            int carrierId = 1;

            AddressBookModule abMod = new AddressBookModule();
            CarrierView carrierView = abMod.AddressBook.Query().GetCarrierByCarrierId(carrierId);
              
            Assert.Equal("United Parcel Service",carrierView.CarrierName.ToString());
        }

        [Fact]
        public void  TestGetSupplierBySupplierId()
        {
            int supplierId = 1;
            AddressBookModule abMod = new AddressBookModule();
            SupplierView supplierView = abMod.AddressBook.Query().GetSupplierBySupplierId(supplierId);

           Assert.True(supplierView.SupplierId != null);

        }
      
        [Fact]
        public void TestGetSupervisorBySupervisorId()
        {
            int supervisorId = 1;
            AddressBookModule abMod = new AddressBookModule();
            SupervisorView view = abMod.AddressBook.Query().GetSupervisorBySupervisorId(supervisorId);

            Assert.Equal(view.ParentSupervisorName.ToUpper().ToString() , "PAM NISHIMOTO".ToString());
        }
        [Fact]
        public void TestGetPhonesByAddressId()
        {
            long addressId = 1;
            AddressBookModule abMod = new AddressBookModule();
            List<Phones> list = abMod.AddressBook.Query().GetPhonesByAddressId(addressId);
  
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
            List<Emails> list = abMod.AddressBook.Query().GetEmailsByAddressId(addressId);
      
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
        public void TestGetAddressBooks()
        {
            //int addressId = 1;

            //UnitOfWork unitOfWork = new UnitOfWork();
            AddressBookModule abMod = new AddressBookModule();
            List<AddressBookView> addressBooks=abMod.AddressBook.Query().GetAddressBookByName("David");
     
            IList<string> list = new List<string>();
            foreach (var item in addressBooks)
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
        public void TestUpdateAddressBook()
        {
            long addressId = 1;

            AddressBookModule abMod = new AddressBookModule();
            //AddressBook addressBook=abMod.AddressBook.Query().GetAddressBookByAddressId(addressId);
            //AddressBookView addressBookView = abMod.AddressBook.Query().GetAddressBookViewByAddressId(addressId);

            AddressBookView addressBookView = new AddressBookView();
            addressBookView.AddressId = addressId;
            addressBookView.FirstName = "David2";
            addressBookView.LastName = "Nishimoto";
            addressBookView.Name = "David Nishimoto";

            AddressBook addressBook = new AddressBook();

            abMod.AddressBook.MapAddressBookEntity(ref addressBook, addressBookView);

            //AddressBookView view = new AddressBookView();
            //view.FirstName = "David2";

            abMod.AddressBook.UpdateAddressBook(addressBook).Apply();


            //unitOfWork.addressBookRepository.UpdateObject(addressBook);
            //unitOfWork.CommitChanges();
            AddressBook addressBook2=abMod.AddressBook.Query().GetEntityById(addressId);
    
            string name = addressBook2.FirstName;

            Assert.Equal("David2",name );

            //addressBook = resultTask.Result;
            addressBook2.FirstName = "David";
            abMod.AddressBook.UpdateAddressBook(addressBook).Apply();
            //unitOfWork.addressBookRepository.UpdateObject(addressBook2);
            //unitOfWork.CommitChanges();

            AddressBook addressBook3 = abMod.AddressBook.Query().GetEntityById(addressId);

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
            abMod.AddressBook.CreateAddressBook(addressBook).Apply();
            IQueryable<AddressBook> query =abMod.AddressBook.Query().GetAddressBooksByExpression(a => a.Name == "James Dean");
            //IQueryable<AddressBook> query = abMod.AddressBook.queryAddressBook;
            //IQueryable<AddressBook> query = unitOfWork.addressBookRepository.GetObjectsQueryable(a => a.Name == "James Dean");

            foreach (var item in query)
            {
                Assert.Equal("James Dean",item.Name );

                //unitOfWork.addressBookRepository.DeleteObject(item);
                abMod.AddressBook.DeleteAddressBook(item);
            }
            abMod.AddressBook.Apply(); //Avoid the thread problem

            //unitOfWork.CommitChanges();

            Assert.True(true);
           

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
            abMod.AddressBook.CreateAddressBooks(list).Apply();

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
