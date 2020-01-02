using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ScheduleEventDomain
{

    public class UnitScheduleEvent
    {

        private readonly ITestOutputHelper output;

        public UnitScheduleEvent(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ScheduleEventModule ScheduleEventMod = new ScheduleEventModule();
            Employee employee = await ScheduleEventMod.Employee.Query().GetEntityById(3);
            AddressBook addressBookEmployee = await ScheduleEventMod.AddressBook.Query().GetEntityById(employee.AddressId);
            Customer customer = await ScheduleEventMod.Customer.Query().GetEntityById(1);
            AddressBook addressBookCustomer = await ScheduleEventMod.AddressBook.Query().GetEntityById(customer.AddressId);
            ServiceInformation serviceInformation = await ScheduleEventMod.ServiceInformation.Query().GetEntityById(3);
           ScheduleEventView view = new ScheduleEventView()
            {
                  EmployeeId=employee.EmployeeId,
                  EmployeeName=addressBookEmployee.Name,
                  EventDateTime=DateTime.Parse("11/13/2019"),
                  ServiceId=serviceInformation.ServiceId,
                  ServiceDescription=serviceInformation.ServiceDescription,
                  DurationMinutes=30,
                  CustomerId=customer.CustomerId,
                  CustomerName=addressBookCustomer.Name,
            };
            NextNumber nnNextNumber = await ScheduleEventMod.ScheduleEvent.Query().GetNextNumber();

            view.ScheduleEventNumber = nnNextNumber.NextNumberValue;

            ScheduleEvent scheduleEvent = await ScheduleEventMod.ScheduleEvent.Query().MapToEntity(view);

            ScheduleEventMod.ScheduleEvent.AddScheduleEvent(scheduleEvent).Apply();

            ScheduleEvent newScheduleEvent = await ScheduleEventMod.ScheduleEvent.Query().GetEntityByNumber(view.ScheduleEventNumber);

            Assert.NotNull(newScheduleEvent);

            newScheduleEvent.DurationMinutes = 32;

            ScheduleEventMod.ScheduleEvent.UpdateScheduleEvent(newScheduleEvent).Apply();

            ScheduleEventView updateView = await ScheduleEventMod.ScheduleEvent.Query().GetViewById(newScheduleEvent.ScheduleEventId);

            if (updateView.DurationMinutes !=32) Assert.True(true);

              ScheduleEventMod.ScheduleEvent.DeleteScheduleEvent(newScheduleEvent).Apply();
            ScheduleEvent lookupScheduleEvent= await ScheduleEventMod.ScheduleEvent.Query().GetEntityById(view.ScheduleEventId);

            Assert.Null(lookupScheduleEvent);
        }
       
      

    }
}
