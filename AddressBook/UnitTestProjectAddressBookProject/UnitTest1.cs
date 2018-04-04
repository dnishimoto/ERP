using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectAddressBookProject
{
    using Millennium.Services;
    using Millennium.EntityFramework;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetAddress()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Task<AddressBook> resultTask = Task.Run<AddressBook>(async () => await unitOfWork.addressBookRepository.GetAddressBook(1));

            Console.WriteLine($"{resultTask.Result.FirstName}");

            Assert.AreEqual(resultTask.Result.FirstName, "David");
        }
    }
}
