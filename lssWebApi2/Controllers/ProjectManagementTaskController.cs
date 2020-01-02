using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ProjectManagementTaskDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ProjectManagementTaskController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementTaskView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectManagementTask([FromBody]ProjectManagementTaskView view)
        {
            ProjectManagementTaskModule invMod = new ProjectManagementTaskModule();

            NextNumber nnProjectManagementTask = await invMod.ProjectManagementTask.Query().GetNextNumber();

            view.TaskNumber = nnProjectManagementTask.NextNumberValue;

            ProjectManagementTask projectManagementTask = await invMod.ProjectManagementTask.Query().MapToEntity(view);

            invMod.ProjectManagementTask.AddProjectManagementTask(projectManagementTask).Apply();

            ProjectManagementTaskView newView = await invMod.ProjectManagementTask.Query().GetViewByNumber(view.TaskNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementTaskView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectManagementTask([FromBody]ProjectManagementTaskView view)
        {
            ProjectManagementTaskModule invMod = new ProjectManagementTaskModule();
            ProjectManagementTask projectManagementTask = await invMod.ProjectManagementTask.Query().MapToEntity(view);
            invMod.ProjectManagementTask.DeleteProjectManagementTask(projectManagementTask).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementTaskView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectManagementTask([FromBody]ProjectManagementTaskView view)
        {
            ProjectManagementTaskModule invMod = new ProjectManagementTaskModule();

            ProjectManagementTask projectManagementTask = await invMod.ProjectManagementTask.Query().MapToEntity(view);


            invMod.ProjectManagementTask.UpdateProjectManagementTask(projectManagementTask).Apply();

            ProjectManagementTaskView retView = await invMod.ProjectManagementTask.Query().GetViewById(projectManagementTask.TaskId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ProjectManagementTaskId}")]
        [ProducesResponseType(typeof(ProjectManagementTaskView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProjectManagementTaskView(long projectManagementTaskId)
        {
            ProjectManagementTaskModule invMod = new ProjectManagementTaskModule();

            ProjectManagementTaskView view = await invMod.ProjectManagementTask.Query().GetViewById(projectManagementTaskId);
            return Ok(view);
        }
        }
	}
        