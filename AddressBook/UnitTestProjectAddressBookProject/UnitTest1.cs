using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectAddressBookProject
{
    using Millennium.Services;
    using Millennium.EntityFramework;
    using System.Linq;
    using System.Collections.Generic;

    [TestClass]
    public class UnitTest1
    {
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

            Assert.AreEqual(resultTask.Result.FirstName, "David");
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
