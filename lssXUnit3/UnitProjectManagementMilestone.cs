using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementMilestoneDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{

    public class UnitProjectManagementMilestone
    {

        private readonly ITestOutputHelper output;

        public UnitProjectManagementMilestone(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ProjectManagementMilestoneModule ProjectManagementMilestoneMod = new ProjectManagementMilestoneModule();
            ProjectManagementMilestone milestone = await ProjectManagementMilestoneMod.Milestone.Query().GetEntityById(1);
            ProjectManagementProject project = await ProjectManagementMilestoneMod.Project.Query().GetEntityById(1);
            RollupTaskToMilestoneView rollup = await ProjectManagementMilestoneMod.Milestone.Query().GetTaskToMilestoneRollupViewById(milestone.MilestoneId);
            ProjectManagementMilestoneView view = new ProjectManagementMilestoneView()
            {
                MilestoneName = "fluent code refactor",
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                EstimatedHours = rollup.EstimatedHours,
                EstimatedDays = rollup.EstimatedDays,
                ActualDays=rollup.ActualDays,
                ActualHours=rollup.ActualHours,
                ActualStartDate=rollup.ActualStartDate,
                ActualEndDate=rollup.ActualEndDate,
                EstimatedStartDate=rollup.EstimatedStartDate,
                EstimatedEndDate=rollup.EstimatedEndDate,
                Cost=rollup.Cost,
                Wbs = "2.1"


            };
            NextNumber nnNextNumber = await ProjectManagementMilestoneMod.Milestone.Query().GetNextNumber();

            view.MileStoneNumber = nnNextNumber.NextNumberValue;

            ProjectManagementMilestone projectManagementMilestone = await ProjectManagementMilestoneMod.Milestone.Query().MapToEntity(view);

            ProjectManagementMilestoneMod.Milestone.AddProjectManagementMilestone(projectManagementMilestone).Apply();

            ProjectManagementMilestone newProjectManagementMilestone = await ProjectManagementMilestoneMod.Milestone.Query().GetEntityByNumber(view.MileStoneNumber);

            Assert.NotNull(newProjectManagementMilestone);

            newProjectManagementMilestone.MilestoneName = "MS Name Update";

            ProjectManagementMilestoneMod.Milestone.UpdateProjectManagementMilestone(newProjectManagementMilestone).Apply();

            ProjectManagementMilestoneView updateView = await ProjectManagementMilestoneMod.Milestone.Query().GetViewById(newProjectManagementMilestone.MilestoneId);

            Assert.Same(updateView.MilestoneName, "MS Name Update");
            ProjectManagementMilestoneMod.Milestone.DeleteProjectManagementMilestone(newProjectManagementMilestone).Apply();
            ProjectManagementMilestone lookupProjectManagementMilestone = await ProjectManagementMilestoneMod.Milestone.Query().GetEntityById(view.MilestoneId);

            Assert.Null(lookupProjectManagementMilestone);
        }



    }
}
