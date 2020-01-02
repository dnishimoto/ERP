using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{

    public class UnitProjectManagementWorkOrderToEmployee
    {

        private readonly ITestOutputHelper output;

        public UnitProjectManagementWorkOrderToEmployee(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ProjectManagementWorkOrderToEmployeeModule ProjectManagementWorkOrderToEmployeeMod = new ProjectManagementWorkOrderToEmployeeModule();

            AddressBook addressBook = null;
            Task<Employee> employeeTask = ProjectManagementWorkOrderToEmployeeMod.Employee.Query().GetEntityById(3);
            Task<ProjectManagementWorkOrder> workOrderTask = ProjectManagementWorkOrderToEmployeeMod.WorkOrder.Query().GetEntityById(5);
            Task.WaitAll(employeeTask, workOrderTask);
            addressBook = await ProjectManagementWorkOrderToEmployeeMod.AddressBook.Query().GetEntityById(employeeTask.Result.AddressId);

            ProjectManagementWorkOrderToEmployeeView view = new ProjectManagementWorkOrderToEmployeeView()
            {
                EmployeeName = addressBook.Name,
                WorkOrderDescription = workOrderTask.Result.Description,
                WorkOrderLocation = workOrderTask.Result.Location,
                WorkOrderStartDate = workOrderTask.Result.StartDate,
                WorkOrderEndDate = workOrderTask.Result.EndDate,
                WorkOrderInstructions = workOrderTask.Result.Instructions
           };
            NextNumber nnNextNumber = await ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.Query().GetNextNumber();

            view.WorkOrderToEmployeeNumber = nnNextNumber.NextNumberValue;

            ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee = await ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.Query().MapToEntity(view);

            ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.AddProjectManagementWorkOrderToEmployee(projectManagementWorkOrderToEmployee).Apply();

            ProjectManagementWorkOrderToEmployee newProjectManagementWorkOrderToEmployee = await ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.Query().GetEntityByNumber(view.WorkOrderToEmployeeNumber);

            Assert.NotNull(newProjectManagementWorkOrderToEmployee);

            newProjectManagementWorkOrderToEmployee.EmployeeId = 1;
            
            ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.UpdateProjectManagementWorkOrderToEmployee(newProjectManagementWorkOrderToEmployee).Apply();

            ProjectManagementWorkOrderToEmployeeView updateView = await ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.Query().GetViewById(newProjectManagementWorkOrderToEmployee.WorkOrderToEmployeeId);

            if (updateView.EmployeeId == 1) Assert.True(true);

            ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.DeleteProjectManagementWorkOrderToEmployee(newProjectManagementWorkOrderToEmployee).Apply();
            ProjectManagementWorkOrderToEmployee lookupProjectManagementWorkOrderToEmployee= await ProjectManagementWorkOrderToEmployeeMod.ProjectManagementWorkOrderToEmployee.Query().GetEntityById(view.WorkOrderToEmployeeId);

            Assert.Null(lookupProjectManagementWorkOrderToEmployee);
        }
       
      

    }
}
