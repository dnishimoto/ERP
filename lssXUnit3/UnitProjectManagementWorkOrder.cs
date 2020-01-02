using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{

    public class UnitProjectManagementWorkOrder
    {

        private readonly ITestOutputHelper output;

        public UnitProjectManagementWorkOrder(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ProjectManagementWorkOrderModule ProjectManagementWorkOrderMod = new ProjectManagementWorkOrderModule();
            ProjectManagementProject project = await ProjectManagementWorkOrderMod.Project.Query().GetEntityById(1);
            ChartOfAccount account = await ProjectManagementWorkOrderMod.ChartOfAccount.Query().GetEntityById(4);
           ProjectManagementWorkOrderView view = new ProjectManagementWorkOrderView()
            {
                    Description = "ProjectManagementWorkOrder Test",
                    StartDate=DateTime.Parse("12/19/2019"),
                    EndDate=DateTime.Parse("12/19/2019"),
                    ActualAmount=30,
                    ActualHours=1,
                    EstimatedAmount=30,
                    EstimatedHours=1,
                    AccountNumber=account.Account,
                    AccountDescription=account.Description,
                    SupCode=account.SupCode,
                    ThirdAccount=account.ThirdAccount,
                    Instructions="TestInstructions",
                    ProjectId=project.ProjectId,
                    ProjectName=project.ProjectName,
                    Status="Open",
                    Location="Test Location"

            };
            NextNumber nnNextNumber = await ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.Query().GetNextNumber();

            view.WorkOrderNumber = nnNextNumber.NextNumberValue;

            ProjectManagementWorkOrder projectManagementWorkOrder = await ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.Query().MapToEntity(view);

            ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.AddProjectManagementWorkOrder(projectManagementWorkOrder).Apply();

            ProjectManagementWorkOrder newProjectManagementWorkOrder = await ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.Query().GetEntityByNumber(view.WorkOrderNumber??0);

            Assert.NotNull(newProjectManagementWorkOrder);

            newProjectManagementWorkOrder.Description = "ProjectManagementWorkOrder Test Update";

            ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.UpdateProjectManagementWorkOrder(newProjectManagementWorkOrder).Apply();

            ProjectManagementWorkOrderView updateView = await ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.Query().GetViewById(newProjectManagementWorkOrder.WorkOrderId);

            Assert.Same(updateView.Description, "ProjectManagementWorkOrder Test Update");
              ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.DeleteProjectManagementWorkOrder(newProjectManagementWorkOrder).Apply();
            ProjectManagementWorkOrder lookupProjectManagementWorkOrder= await ProjectManagementWorkOrderMod.ProjectManagementWorkOrder.Query().GetEntityById(view.WorkOrderId);

            Assert.Null(lookupProjectManagementWorkOrder);
        }
       
      

    }
}
