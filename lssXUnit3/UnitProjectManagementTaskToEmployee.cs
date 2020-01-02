using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{

    public class UnitProjectManagementTaskToEmployee
    {

        private readonly ITestOutputHelper output;

        public UnitProjectManagementTaskToEmployee(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ProjectManagementTaskToEmployeeModule ProjectManagementTaskToEmployeeMod = new ProjectManagementTaskToEmployeeModule();
            Employee employee = await ProjectManagementTaskToEmployeeMod.Employee.Query().GetEntityById(3);
            AddressBook addressBook = await ProjectManagementTaskToEmployeeMod.AddressBook.Query().GetEntityById(employee.AddressId);
            ProjectManagementTask task = await ProjectManagementTaskToEmployeeMod.Task.Query().GetEntityById(5);
            ProjectManagementMilestone milestone = await ProjectManagementTaskToEmployeeMod.Milestone.Query().GetEntityById(task.MileStoneId);
            ProjectManagementProject project = await ProjectManagementTaskToEmployeeMod.Project.Query().GetEntityById(milestone.ProjectId);
            ProjectManagementTaskToEmployeeView view = new ProjectManagementTaskToEmployeeView()
            {
                TaskId = task.TaskId,
                EmployeeId = employee.EmployeeId,
                EmployeeName = addressBook.Name,
                TaskName = task.TaskName,
                TaskDescription = task.Description,
                MilestoneName = milestone.MilestoneName,
                ProjectName = project.ProjectName

            };
            NextNumber nnNextNumber = await ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.Query().GetNextNumber();

            view.TaskToEmployeeNumber = nnNextNumber.NextNumberValue;

            ProjectManagementTaskToEmployee projectManagementTaskToEmployee = await ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.Query().MapToEntity(view);

            ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.AddProjectManagementTaskToEmployee(projectManagementTaskToEmployee).Apply();

            ProjectManagementTaskToEmployee newProjectManagementTaskToEmployee = await ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.Query().GetEntityByNumber(view.TaskToEmployeeNumber);

            Assert.NotNull(newProjectManagementTaskToEmployee);

            newProjectManagementTaskToEmployee.EmployeeId=6;

            ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.UpdateProjectManagementTaskToEmployee(newProjectManagementTaskToEmployee).Apply();

            ProjectManagementTaskToEmployeeView updateView = await ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.Query().GetViewById(newProjectManagementTaskToEmployee.TaskToEmployeeId);

            if (updateView.EmployeeId != 6) Assert.True(false);

            ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.DeleteProjectManagementTaskToEmployee(newProjectManagementTaskToEmployee).Apply();
            ProjectManagementTaskToEmployee lookupProjectManagementTaskToEmployee = await ProjectManagementTaskToEmployeeMod.ProjectManagementTaskToEmployee.Query().GetEntityById(view.TaskToEmployeeId);

            Assert.Null(lookupProjectManagementTaskToEmployee);
        }



    }
}
