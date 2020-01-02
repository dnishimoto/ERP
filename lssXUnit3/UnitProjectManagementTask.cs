using lssWebApi2.EmployeeDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ProjectManagementTaskDomain
{

    public class UnitProjectManagementTask
    {

        private readonly ITestOutputHelper output;

        public UnitProjectManagementTask(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ProjectManagementTaskModule ProjectManagementTaskMod = new ProjectManagementTaskModule();
            ProjectManagementMilestone milestone = await ProjectManagementTaskMod.Milestone.Query().GetEntityById(1);
            Udc udc = await ProjectManagementTaskMod.Udc.Query().GetEntityById(21);
            ProjectManagementProject project = await ProjectManagementTaskMod.Project.Query().GetEntityById(1);
            ProjectManagementWorkOrder workOrder = await ProjectManagementTaskMod.WorkOrder.Query().GetEntityById(7);
            ChartOfAccount account = await ProjectManagementTaskMod.ChartOfAccount.Query().GetEntityById(4);
            IList<ProjectManagementWorkOrderToEmployeeView> views = await ProjectManagementTaskMod.WorkToEmployee.Query().GetViewsByWorkOrderId(workOrder.WorkOrderId);


            ProjectManagementTaskView view = new ProjectManagementTaskView()
            {
                Wbs = "1.3",
                TaskName = "refactor code",
                Description = "refactor code to solid principles",
                EstimatedStartDate = DateTime.Parse("12/1/2019"),
                EstimatedHours = 100,
                EstimatedEndDate = DateTime.Parse("12/31/2019"),
                ActualStartDate = DateTime.Parse("12/1/2019"),
                ActualHours = 101,
                ActualEndDate = DateTime.Parse("12/20/2020"),
                Cost = 3100,
                MileStoneId = milestone.MilestoneId,
                MilestoneName=milestone.MilestoneName,
                StatusXrefId = udc.XrefId,
                EstimatedCost = 2900,
                ActualDays = 101 / 8,
                EstimatedDays = 100 / 8,
                ProjectId = project.ProjectId,
                WorkOrderId = workOrder.WorkOrderId,
                Instructions=workOrder.Instructions,
                AccountId = account.AccountId,
                Account = account.Account,
                ProjectName = project.ProjectName,

                WorkOrderToEmployeeViews = views
            };
            NextNumber nnNextNumber = await ProjectManagementTaskMod.ProjectManagementTask.Query().GetNextNumber();

            view.TaskNumber = nnNextNumber.NextNumberValue;

            ProjectManagementTask projectManagementTask = await ProjectManagementTaskMod.ProjectManagementTask.Query().MapToEntity(view);

            ProjectManagementTaskMod.ProjectManagementTask.AddProjectManagementTask(projectManagementTask).Apply();

            ProjectManagementTask newProjectManagementTask = await ProjectManagementTaskMod.ProjectManagementTask.Query().GetEntityByNumber(view.TaskNumber);

            Assert.NotNull(newProjectManagementTask);

            newProjectManagementTask.Description = "ProjectManagementTask Test Update";

            ProjectManagementTaskMod.ProjectManagementTask.UpdateProjectManagementTask(newProjectManagementTask).Apply();

            ProjectManagementTaskView updateView = await ProjectManagementTaskMod.ProjectManagementTask.Query().GetViewById(newProjectManagementTask.TaskId);

            Assert.Same(updateView.Description, "ProjectManagementTask Test Update");
            ProjectManagementTaskMod.ProjectManagementTask.DeleteProjectManagementTask(newProjectManagementTask).Apply();
            ProjectManagementTask lookupProjectManagementTask = await ProjectManagementTaskMod.ProjectManagementTask.Query().GetEntityById(view.TaskId);

            Assert.Null(lookupProjectManagementTask);
        }



    }
}
