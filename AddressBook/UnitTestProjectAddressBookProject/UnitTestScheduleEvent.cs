using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    using MillenniumERP.Services;
    using MillenniumERP.EntityFramework;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Summary description for UnitTestScheduleEvent
    /// </summary>
    [TestClass]
    public class UnitTestScheduleEvent
    {

        public UnitTestScheduleEvent()
        {
           

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetScheduleEvents()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int employeeAddressId = 3;
            Task<IQueryable<ScheduleEvent>> resultTask = Task.Run<IQueryable<ScheduleEvent>>(async () => await unitOfWork.scheduleEventRepository.GetScheduleEvents(employeeAddressId));

            IList<ScheduleEvent> list = new List<ScheduleEvent>();
            foreach (var item in resultTask.Result)
            {
                Console.WriteLine($"{item.EmployeeAddressBook.Name} Date: {item.EventDateTime} Duration: {item.DurationMinutes}");
                list.Add(item);
            }
            var Employee = list.Where(e => e.EmployeeAddressBook.Name == "dan brown").FirstOrDefault();

            Assert.IsTrue(Employee!=null);
        }
    }
}
