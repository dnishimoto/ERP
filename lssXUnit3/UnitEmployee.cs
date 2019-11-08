using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.AddressBookDomain
{

    public class UnitEmployee
    {

        private readonly ITestOutputHelper output;

        public UnitEmployee(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task TestGetEmployeeByEmployeeId()
        {
            int employeeId = 3;
            AddressBookModule abMod = new AddressBookModule();
            EmployeeView employeeView = await abMod.Employee.Query().GetViewById(employeeId);

            Assert.True(employeeView.EmployeeId != null);
        }
        [Fact]
        public async Task TestGetEmployeesBySupervisorId()
        {

            int supervisorId = 1;
            AddressBookModule abMod = new AddressBookModule();

            List<EmployeeView> list = await abMod.Employee.Query().GetViewsBySupervisorId(supervisorId);


            Assert.True(list.Count > 0);

        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            EmployeeModule EmployeeMod = new EmployeeModule();

            EmployeeView view = new EmployeeView()
            {
                AddressId = 1,
                JobTitleXrefId = 25,
                EmploymentStatusXrefId = 27,
                HiredDate = DateTime.Parse("5/1/2019"),
                TaxIdentification = "999999",
                PayRollGroupCode = 1,
                Salary = 99999M,
                HourlyRate = 99M,
                SalaryPerPayPeriod = 999M,
                EmployeeNumber = 1
            };
            NextNumber nnNextNumber = await EmployeeMod.Employee.Query().GetNextNumber();

            view.EmployeeNumber = nnNextNumber.NextNumberValue;

            Employee employee = await EmployeeMod.Employee.Query().MapToEntity(view);

            EmployeeMod.Employee.AddEmployee(employee).Apply();

            Employee newEmployee = await EmployeeMod.Employee.Query().GetEntityByNumber(view.EmployeeNumber);

            Assert.NotNull(newEmployee);

            newEmployee.TaxIdentification = "999999 Update";

            EmployeeMod.Employee.UpdateEmployee(newEmployee).Apply();

            EmployeeView updateView = await EmployeeMod.Employee.Query().GetViewById(newEmployee.EmployeeId);

            Assert.Same(updateView.TaxIdentification, "999999 Update");
            EmployeeMod.Employee.DeleteEmployee(newEmployee).Apply();
            Employee lookupEmployee = await EmployeeMod.Employee.Query().GetEntityById(view.EmployeeId);

            Assert.Null(lookupEmployee);
        }



    }
}
