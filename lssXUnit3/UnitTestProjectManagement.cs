
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.ProjectManagementTaskDomain;

namespace lssWebApi2.ProjectManagementDomain
{

    public class UnitTestProjectManagement
    {

        private readonly ITestOutputHelper output;

        public UnitTestProjectManagement(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestWoToEmployee()
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            ProjectManagementWorkOrderToEmployee woToEmployee = new ProjectManagementWorkOrderToEmployee()
            {
                EmployeeId = 1,
                WorkOrderId = 2
            };
            List<ProjectManagementWorkOrderToEmployee> list = new List<ProjectManagementWorkOrderToEmployee>();

            list.Add(woToEmployee);
            pmMod.WorkOrderToEmployee.AddProjectManagementWorkOrderToEmployees(list).Apply();

            long? workOrderId = woToEmployee.WorkOrderId ;

            IEnumerable<EmployeeView> employeeList =
                await pmMod.Employee.Query().GetEntitiesByWorkOrderId(workOrderId??0);
            foreach (var item in employeeList)
            {
                output.WriteLine($"{item.EmployeeName}");
            }
            pmMod.WorkOrderToEmployee.DeleteProjectManagementWorkOrderToEmployees(list).Apply();


        }
        [Fact]
        public async Task TestCreateProjecttoWorkOrder()
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();

            NextNumber nnProject = await pmMod.Project.Query().GetNextNumber();



            ProjectManagementProject newProject = new ProjectManagementProject()
            {
                ProjectName = "Test Project",
                Version = "1",
                Description = "Test Project Description",
                EstimatedStartDate = DateTime.Parse("4/1/2019"),
                EstimatedHours = 10,
                EstimatedEndDate = DateTime.Parse("4/1/2019"),
                EstimatedDays = 1,
                ProjectNumber = nnProject.NextNumberValue
            };

            pmMod.Project.AddProject(newProject).Apply();

            ProjectManagementProject project = await pmMod.Project.Query().GetEntityByNumber(nnProject.NextNumberValue);

            long projectId = project.ProjectId;

            project.Description = "Test Project Description Update";

            pmMod.Project.UpdateProject(project).Apply();

            project = await pmMod.Project.Query().GetEntityById(projectId);

            Assert.Contains(project.Description, "Test Project Description Update");

            NextNumber nnWorkOrder = await pmMod.WorkOrder.Query().GetNextNumber();

            ProjectManagementWorkOrder newWorkOrder = new ProjectManagementWorkOrder()
            {
                WorkOrderNumber = nnWorkOrder.NextNumberValue,
                Description = "Test Work Order",
                StartDate = DateTime.Parse("4/1/2019"),
                EndDate = DateTime.Parse("4/1/2019"),
                EstimatedAmount = 100,
                EstimatedHours = 10,
                AccountNumber = "Test Account",
                Instructions = "Test Instructions",
                ProjectId = projectId,
                Status = "Open",
                Location = "Test Location"
            };

            pmMod.WorkOrder.AddProjectManagementWorkOrder(newWorkOrder).Apply();

            ProjectManagementWorkOrder workOrder = await pmMod.WorkOrder.Query().GetEntityByNumber(nnWorkOrder.NextNumberValue);

            long workOrderId = workOrder.WorkOrderId;

            workOrder.Description = "Test Work Order Description Update";

            pmMod.WorkOrder.UpdateProjectManagementWorkOrder(workOrder).Apply();
            
            workOrder = await pmMod.WorkOrder.Query().GetEntityById(workOrderId);

            Assert.Contains(workOrder.Description, "Test Work Order Description Update");

            NextNumber nnMileStone = await pmMod.Milestone.Query().GetNextNumber();

            ProjectManagementMilestone mileStone = new ProjectManagementMilestone() {

                MileStoneNumber = nnMileStone.NextNumberValue,
                MilestoneName = "Test Milestone",
                ProjectId = projectId,
                EstimatedHours = 1.1M,
                ActualDays = 1,
                EstimatedDays = 1,
                ActualHours = 1.2M,
                ActualStartDate = DateTime.Now,
                ActualEndDate = DateTime.Now,
                EstimatedStartDate = DateTime.Now.AddDays(-7), 
                EstimatedEndDate = DateTime.Now.AddDays(-1),
                Cost =100.1M,
                Wbs ="1.1"
            };

            pmMod.Milestone.AddProjectManagementMilestone(mileStone).Apply();

            ProjectManagementMilestone milestoneLookup = await pmMod.Milestone.Query().GetEntityByNumber(nnMileStone.NextNumberValue);

            ProjectManagementWorkOrderToEmployee woToEmployee = new ProjectManagementWorkOrderToEmployee()
            {
                EmployeeId = 1,
                WorkOrderId = workOrderId
            };
            List<ProjectManagementWorkOrderToEmployee> list = new List<ProjectManagementWorkOrderToEmployee>();

            list.Add(woToEmployee);
            pmMod.WorkOrderToEmployee.AddProjectManagementWorkOrderToEmployees(list).Apply();

            long  workOrderId2 = woToEmployee.WorkOrderId ;

            IEnumerable<EmployeeView> employeeList =
                await pmMod.Employee.Query().GetEntitiesByWorkOrderId(workOrderId2);
            foreach (var item in employeeList)
            {
                output.WriteLine($"{item.EmployeeName}");
            }
            Assert.True(employeeList.Count() > 0);

           pmMod.WorkOrderToEmployee.DeleteProjectManagementWorkOrderToEmployees(list).Apply();

            pmMod.Milestone.DeleteProjectManagementMilestone(mileStone).Apply();

            pmMod.WorkOrder.DeleteProjectManagementWorkOrder(workOrder).Apply();

            pmMod.Project.DeleteProject(project).Apply();

            //ProjectManagementMilestones mileStone = new ProjectManagementMilestones();


        }
        [Fact]
        public async Task TestTaskToProjectRollup()
        {
            ProjectManagementModule pmMod = new ProjectManagementModule();
            ProjectManagementProject project = await pmMod.Project.Query().GetEntityById(1);
            ProjectManagementMilestone milestone = await pmMod.Milestone.Query().GetEntityById(1);
            Udc udc = await pmMod.Udc.Query().GetEntityById(21);
            ChartOfAccount account = await pmMod.ChartOfAccount.Query().GetEntityById(4);


            ProjectManagementTaskView view = new ProjectManagementTaskView()
            {
                Wbs = "1.3",
                TaskName = "test rollup",
                Description = "task to project rollup",
                EstimatedStartDate = DateTime.Parse("12/1/2019"),
                EstimatedHours = 1,
                EstimatedEndDate = DateTime.Parse("12/31/2019"),
                ActualStartDate = DateTime.Parse("12/23/2019"),
                ActualHours = 1,
                ActualEndDate = DateTime.Parse("12/23/2020"),
                Cost = 31,
                MileStoneId = milestone.MilestoneId,
                MilestoneName = milestone.MilestoneName,
                StatusXrefId = udc.XrefId,
                EstimatedCost = 29,
                ActualDays = 1,
                EstimatedDays = 1,
                ProjectId = project.ProjectId,
                AccountId = account.AccountId,
                Account = account.Account,
                ProjectName = project.ProjectName,


            };

            NextNumber nnNextNumber = await pmMod.ProjectManagementTask.Query().GetNextNumber();

            view.TaskNumber = nnNextNumber.NextNumberValue;

            ProjectManagementTask projectManagementTask = await pmMod.ProjectManagementTask.Query().MapToEntity(view);

            pmMod.ProjectManagementTask.AddProjectManagementTask(projectManagementTask).Apply();


            RollupTaskToProjectView rollup = await pmMod.Project.Query().GetTaskToProjectRollupViewById(milestone.MilestoneId);


            ProjectManagementTask newProjectManagementTask = await pmMod.ProjectManagementTask.Query().GetEntityByNumber(view.TaskNumber);

            if (rollup.Cost<3000) Assert.True(false);

            pmMod.ProjectManagementTask.DeleteProjectManagementTask(newProjectManagementTask).Apply();

        }
        [Fact]
        public async Task TestGetTasksByMilestoneId()
        {
            long milestoneId = 1;
            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementTask> query = await pmMod.Task.Query().GetEntitiesByMilestoneId(milestoneId);

            int count = 0;
            foreach (var item in query)
            {
                
                    output.WriteLine($"{item.TaskName}");
                    count++;
             
            }
            Assert.True(count > 0);
        }

        [Fact]
        public async Task TestGetWorkOrdersByProjectId()
        {
            int projectId = 1;

            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementWorkOrder> query = await pmMod.WorkOrder.Query().GetEntitiesByProjectId(projectId);

            foreach (var item in query)
            {
                output.WriteLine($"{item.Description}");
            }
            Assert.True(query.Count() > 0);
        }
        [Fact]
        public async Task TestGetMileStonesByProjectId()
        {
            int projectId = 1;

            ProjectManagementModule pmMod = new ProjectManagementModule();
            IQueryable<ProjectManagementMilestone> query = await pmMod.Milestone.Query().GetEntitiesByProjectId(projectId);
            int count = 0;
            foreach (var item in query)
            {
                   output.WriteLine($"{item.MilestoneName}");
                    count++;
       
            }
            Assert.True(count > 0);
        }

        [Fact]
        public async Task TestGetTasksByProjectId()
        {
            long projectId = 1;

            ProjectManagementModule pmMod = new ProjectManagementModule();

            IQueryable<ProjectManagementTask> query = await pmMod.Task.Query().GetEntitiesByProjectId(projectId);

            int count = 0;
            foreach (var item in query)
            {

                output.WriteLine($"Task Name: {item.Wbs} {item.TaskName}");
                count++;

            }
            Assert.True(count > 0);
        }//end function

    }
}
