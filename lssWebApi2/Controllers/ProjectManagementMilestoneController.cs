using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ProjectManagementMilestoneDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ProjectManagementMilestoneController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementMilestoneView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectManagementMilestone([FromBody]ProjectManagementMilestoneView view)
        {
            ProjectManagementMilestoneModule invMod = new ProjectManagementMilestoneModule();

            NextNumber nnProjectManagementMilestone = await invMod.Milestone.Query().GetNextNumber();

            view.MileStoneNumber = nnProjectManagementMilestone.NextNumberValue;

            ProjectManagementMilestone projectManagementMilestone = await invMod.Milestone.Query().MapToEntity(view);

            invMod.Milestone.AddProjectManagementMilestone(projectManagementMilestone).Apply();

            ProjectManagementMilestoneView newView = await invMod.Milestone.Query().GetViewByNumber(view.MileStoneNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementMilestoneView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectManagementMilestone([FromBody]ProjectManagementMilestoneView view)
        {
            ProjectManagementMilestoneModule invMod = new ProjectManagementMilestoneModule();
            ProjectManagementMilestone projectManagementMilestone = await invMod.Milestone.Query().MapToEntity(view);
            invMod.Milestone.DeleteProjectManagementMilestone(projectManagementMilestone).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementMilestoneView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectManagementMilestone([FromBody]ProjectManagementMilestoneView view)
        {
            ProjectManagementMilestoneModule invMod = new ProjectManagementMilestoneModule();

            ProjectManagementMilestone projectManagementMilestone = await invMod.Milestone.Query().MapToEntity(view);


            invMod.Milestone.UpdateProjectManagementMilestone(projectManagementMilestone).Apply();

            ProjectManagementMilestoneView retView = await invMod.Milestone.Query().GetViewById(projectManagementMilestone.MilestoneId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ProjectManagementMilestoneId}")]
        [ProducesResponseType(typeof(ProjectManagementMilestoneView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProjectManagementMilestoneView(long projectManagementMilestoneId)
        {
            ProjectManagementMilestoneModule invMod = new ProjectManagementMilestoneModule();

            ProjectManagementMilestoneView view = await invMod.Milestone.Query().GetViewById(projectManagementMilestoneId);
            return Ok(view);
        }
        }
	}
        