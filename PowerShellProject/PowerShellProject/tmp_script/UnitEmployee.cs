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
        public async Task TestAddUpdatDelete()
        {
            EmployeeModule EmployeeMod = new EmployeeModule();

           EmployeeView view = new EmployeeView()
            {
                    Description = 'Employee Test',
                    EmployeeCode=99

            };
            NextNumber nnNextNumber = await EmployeeMod.Employee.Query().GetNextNumber();

            view.EmployeeNumber = nnNextNumber.NextNumberValue;

            Employee employee = await EmployeeMod.Employee.Query().MapToEntity(view);

            EmployeeMod.Employee.AddEmployee(employee).Apply();

            Employee newEmployee = await EmployeeMod.Employee.Query().GetEntityByNumber(view.EmployeeNumber);

            Assert.NotNull(newEmployee);

            newEmployee.Description = 'Employee Test Update';

            EmployeeMod.Employee.UpdateEmployee(newEmployee).Apply();

            EmployeeView updateView = await EmployeeMod.Employee.Query().GetViewById(newEmployee.EmployeeId);

            Assert.Same(updateView.Description, 'Employee Test Update');
              EmployeeMod.Employee.DeleteEmployee(newEmployee).Apply();
            Employee lookupEmployee= await EmployeeMod.Employee.Query().GetEntityById(view.EmployeeId);

            Assert.Null(lookupEmployee);
        }
       
      

    }
}
