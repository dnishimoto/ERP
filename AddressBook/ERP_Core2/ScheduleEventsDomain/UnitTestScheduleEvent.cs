
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.ScheduleEventsDomain
{
    enum GenderType { Male, Female }

    struct Person
    {
        public int Id;
        public string Name;
        public int Age;
        public GenderType Gender;
    }
    public class UnitTestScheduleEvent
    {
        private readonly ITestOutputHelper output;
        public UnitTestScheduleEvent(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestFindOnList()
        {
            List<Person> list = new List<Person>() {
                new Person() {Id=1, Name="Bob Jones",Age=31,Gender=GenderType.Male },
                new Person() {Id=2,Name="Dan Smith",Age=40,Gender=GenderType.Male },
                new Person() {Id=3, Name="Susan Bright", Age=44,Gender=GenderType.Female },
                new Person() {Id=4, Name="Jimmy Davis", Age=50,Gender=GenderType.Female }
            };
            Person person = list.Find((Person p) => p.Id == 1);
            Assert.Equal("Bob Jones",person.Name);

            List<Person> matches = null;

            matches = list.FindAll(p => p.Gender == GenderType.Female);

            foreach (var item in matches)
            {
                output.WriteLine($"Name: {item.Name}\t Gender: {item.Gender}");
            }
            Assert.True(matches.Count > 0);

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
                output.WriteLine($"{index} pow 3 = {item}");
                index++;
            }
        }
        [Fact]
        public async Task TestConfigureAwaitAsync()
        {
            await DoLongRunningFnc().ConfigureAwait(false);
        }
        [Fact]
        public async Task TestResultAsync()
        {
            Task<IList<double>> pow2ResultTask = Task.Run(() => DoCalculationFnc1());
            await pow2ResultTask;
            int index = 0;
            foreach (var item in pow2ResultTask.Result)
            {
                output.WriteLine($"pow {index}^2={ item}");
                index++;
            }
            Assert.True(true);

        }

        private void DoSomethingFnc1()
        {
            output.WriteLine("Fnc1");
        }
        private void DoSomethingFnc2()
        {
            output.WriteLine("Fnc2");
        }
        [Fact]
        public void TestAwaitAsync()
        {
            List<Task> listTask = new List<Task>();
            Task firstTask = Task.Run(() => { DoSomethingFnc1(); });
            Task secondTask = Task.Run(() => { DoSomethingFnc2(); });
            listTask.Add(firstTask);
            listTask.Add(secondTask);
            Task.WaitAll(listTask.ToArray());

            output.WriteLine("Done");
            Assert.True(true);
        }



        [Fact]
        public async Task TestGetScheduleEvents()
        {
           
            long employeeId = 3;

            ScheduleEventModule seMod = new ScheduleEventModule();

            IQueryable<ScheduleEvent> query = await seMod.GetScheduleEventsByEmployeeId(employeeId);
     
            IList<ScheduleEvent> list = new List<ScheduleEvent>();
            foreach (var item in query)
            {
                output.WriteLine($"{item.Employee.EmployeeId} Date: {item.EventDateTime} Duration: {item.DurationMinutes}");
                list.Add(item);
            }
            var Employee = list.Where(e => e.Employee.EmployeeId == employeeId).FirstOrDefault();

            Assert.True(Employee != null);
        }
    }
}
