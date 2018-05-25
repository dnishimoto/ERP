using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    using MillenniumERP.Services;
    using ERP_Core2.EntityFramework;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;

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

        [TestMethod]
        public void TestUniqueCharacters()
        {

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

        enum GenderType { Male, Female}

        struct Person {
            public int Id;
            public string Name;
            public int Age;
            public GenderType Gender;
       }
        [TestMethod]
        public void TestFindOnList()
        {
            List<Person> list = new List<Person>() {
                new Person() {Id=1, Name="Bob Jones",Age=31,Gender=GenderType.Male },
                new Person() {Id=2,Name="Dan Smith",Age=40,Gender=GenderType.Male },
                new Person() {Id=3, Name="Susan Bright", Age=44,Gender=GenderType.Female },
                new Person() {Id=4, Name="Jimmy Davis", Age=50,Gender=GenderType.Female }
            };
            Person person = list.Find((Person p) => p.Id == 1);
            Assert.AreEqual(person.Name , "Bob Jones");

            List<Person> matches = null;

            matches = list.FindAll (p => p.Gender == GenderType.Female);

            foreach (var item in matches)
            {
                Console.WriteLine($"Name: {item.Name}\t Gender: {item.Gender}");
            }
            Assert.IsTrue(matches.Count>0);
 
        }
        IList<double> DoCalculationFnc1()
        {
            IList<double> list = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(Math.Pow((double)i, 2.0));

            }
            return list;
        }


        private async Task DoLongRunningFnc()
        {
            IList<double> list = new List<double>();
            Task longRunningTask = Task.Run(() =>
            {
                for (int i = 0; i < 60; i++)
                {
                    list.Add(Math.Pow((double)i, 3.0));
                    Task.Delay(1000);
                }
            });
            await longRunningTask;
            int index = 0;
            foreach (var item in list)
            {
                Console.WriteLine($"{index} pow 3 = {item}");
                index++;
            }
        }
        [TestMethod]
    public async Task TestConfigureAwaitAsync()
    {
        await DoLongRunningFnc().ConfigureAwait(false);
    }
    [TestMethod]
    public async Task TestResultAsync()
    {
        Task<IList<double>> pow2ResultTask = Task.Run(() => DoCalculationFnc1());
        await pow2ResultTask;
        int index = 0;
        foreach (var item in pow2ResultTask.Result)
        {
            Console.WriteLine($"pow {index}^2={ item}");
            index++;
        }

    }

    private void DoSomethingFnc1()
    {
        Console.WriteLine("Fnc1");
    }
    private void DoSomethingFnc2()
    {
        Console.WriteLine("Fnc2");
    }
    [TestMethod]
    public async Task TestAwaitAsync()
    {
        List<Task> listTask = new List<Task>();
        Task firstTask = Task.Run(() => { DoSomethingFnc1(); });
        Task secondTask = Task.Run(() => { DoSomethingFnc2(); });
        listTask.Add(firstTask);
        listTask.Add(secondTask);
        Task.WaitAll(listTask.ToArray());

        Console.WriteLine("Done");
    }



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

        Assert.IsTrue(Employee != null);
    }
}
}
