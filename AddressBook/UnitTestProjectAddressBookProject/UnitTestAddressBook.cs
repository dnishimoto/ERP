using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    using MillenniumERP.Services;
    using ERP_Core2.EntityFramework;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;

    [TestClass]
    public class UnitTestAddressBook
    {/*
        [TestMethod]
        public void TestAddressBookPhones()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Phone> resultTask = unitOfWork.addressBookRepository.GetPhonesByAddressId(1);

            List<string> intCollection = new List<string>();
            foreach (var item in resultTask)
            {
                Console.WriteLine($"{item.PhoneNumber}");
                intCollection.Add(item.PhoneNumber);

            }
            Assert.IsTrue(intCollection.Contains("2086066785"));

        }
        [TestMethod]
        public void TestAddressBookEmails()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Email> resultTask = unitOfWork.addressBookRepository.GetEmailsByAddressId(1);

            List<string> intCollection = new List<string>();
            foreach (var item in resultTask)
            {
                Console.WriteLine($"{item.Email1}");
                intCollection.Add(item.Email1);
       
            }
           Assert.IsTrue(intCollection.Contains("dnishimoto@listensoftware.com"));

        }
        */
        private void DoSomething1(int iterations)
        {
            for(int i=0; i<iterations;i++)
            {
                Console.WriteLine("Do Something one");
                Thread.Sleep(1);
            }
         
        }
        private void DoSomething2(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("Do Something two");
                Thread.Sleep(2);
            }
        }
        //The state.Break() exist out of the Parallel.ForEach loop when the 
        //application sets the cancelForEach boolean flag.
        private void ProcessUsingParallelForEach(List<string> intCollection)
        {
          
            Parallel.ForEach(intCollection, (integer,state) =>
            {
                Console.WriteLine($"Parallel.Foreach={integer}");
                if (cancelForEach==true) {
                    Console.WriteLine($"Exit Parallel.ForEach {integer}");
                    state.Break();
                    
                }
          
            });

        }
        private bool cancelForEach = false;
        [TestMethod]
        public void TestThreadPooling()
        {
            int numberOfProcessors = Environment.ProcessorCount;

            Parallel.Invoke(() => DoSomething1(5)
                        ,()=>DoSomething2(10));

            List<string> intCollection = new List<string>();

            for (int i = 0; i < 500; i++)
            {
                intCollection.Add(i.ToString());
            }

            var TaskForEach=Task.Run(()=> ProcessUsingParallelForEach(intCollection));

            Thread.Sleep(500);
            cancelForEach = true;

            Task.WaitAll(TaskForEach);

        }
        [TestMethod]
        public void TestGetAddressBooks()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Task<List<AddressBook>> resultTask = Task.Run<List<AddressBook>>(async () => await unitOfWork.addressBookRepository.GetAddressBooks("customer"));

            IList<string> list = new List<string>();
            foreach (var item in resultTask.Result)
            {
                Console.WriteLine($"{item.Name}");
                list.Add(item.Name.ToUpper());
            }
            Assert.IsTrue(list.Contains("BOB SMITH") && list.Contains("PAM NISHIMOTO"));
        }
        [TestMethod]
        public void TestGetAddressBook()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Task<AddressBook> resultTask = Task.Run<AddressBook>(async () => await unitOfWork.addressBookRepository.GetObjectAsync(1));

            Console.WriteLine($"{resultTask.Result.FirstName}");

            Assert.AreEqual(resultTask.Result.FirstName, "David2");
        }
        [TestMethod]
        public void TestUpdateAddressBook()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Task<AddressBook> resultTask = Task.Run<AddressBook>(async () => await unitOfWork.addressBookRepository.GetObjectAsync(1));

            AddressBook addressBook = resultTask.Result;
            addressBook.FirstName = "David2";
            unitOfWork.addressBookRepository.UpdateObject(addressBook);
            unitOfWork.CommitChanges();

            var query = unitOfWork.addressBookRepository.GetObjectAsync(1);

            string name = query.Result.FirstName;

            Assert.AreEqual(name, "David2");


        }
        [TestMethod]
        public void TestAddandDeleteAddressBook()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            AddressBook addressBook = new AddressBook();
            addressBook.FirstName = "James";
            addressBook.LastName = "Dean";
            addressBook.Name = "James Dean";
            unitOfWork.addressBookRepository.AddObject(addressBook);
            unitOfWork.CommitChanges();

            IQueryable<AddressBook> query = unitOfWork.addressBookRepository.GetObjectsAsync(a => a.Name == "James Dean");

            foreach (var item in query)
            {
                Assert.AreEqual(item.Name, "James Dean");

                unitOfWork.addressBookRepository.DeleteObject(item);
            }
            unitOfWork.CommitChanges();

        }
        [TestMethod]
        public void TestDeleteAddressBooks()
        {
            List<AddressBook> list = new List<AddressBook>();
            UnitOfWork unitOfWork = new UnitOfWork();
           
            for (int i = 1; i < 10; i++)
            {
                AddressBook addressBook = new AddressBook();
                addressBook.Name = "Test" + i.ToString();
                list.Add(addressBook);
           
            }
            unitOfWork.addressBookRepository.AddObjects(list);
            unitOfWork.CommitChanges();

            IQueryable<AddressBook> query = unitOfWork.addressBookRepository.GetObjectsAsync(a => a.Name.Contains("Test"));

            list.Clear();

            foreach (var item in query)
            {
                list.Add(item);
            }

            unitOfWork.addressBookRepository.DeleteObjects(list);
            unitOfWork.CommitChanges();

        }
    }
}
