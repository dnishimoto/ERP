using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ProjectManagementTaskToEmployeeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementTaskToEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectManagementTaskToEmployee([FromBody]ProjectManagementTaskToEmployeeView view)
        {
            ProjectManagementTaskToEmployeeModule invMod = new ProjectManagementTaskToEmployeeModule();

            NextNumber nnProjectManagementTaskToEmployee = await invMod.ProjectManagementTaskToEmployee.Query().GetNextNumber();

            view.TaskToEmployeeNumber = nnProjectManagementTaskToEmployee.NextNumberValue;

            ProjectManagementTaskToEmployee projectManagementTaskToEmployee = await invMod.ProjectManagementTaskToEmployee.Query().MapToEntity(view);

            invMod.ProjectManagementTaskToEmployee.AddProjectManagementTaskToEmployee(projectManagementTaskToEmployee).Apply();

            ProjectManagementTaskToEmployeeView newView = await invMod.ProjectManagementTaskToEmployee.Query().GetViewByNumber(view.TaskToEmployeeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementTaskToEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectManagementTaskToEmployee([FromBody]ProjectManagementTaskToEmployeeView view)
        {
            ProjectManagementTaskToEmployeeModule invMod = new ProjectManagementTaskToEmployeeModule();
            ProjectManagementTaskToEmployee projectManagementTaskToEmployee = await invMod.ProjectManagementTaskToEmployee.Query().MapToEntity(view);
            invMod.ProjectManagementTaskToEmployee.DeleteProjectManagementTaskToEmployee(projectManagementTaskToEmployee).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementTaskToEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectManagementTaskToEmployee([FromBody]ProjectManagementTaskToEmployeeView view)
        {
            ProjectManagementTaskToEmployeeModule invMod = new ProjectManagementTaskToEmployeeModule();

            ProjectManagementTaskToEmployee projectManagementTaskToEmployee = await invMod.ProjectManagementTaskToEmployee.Query().MapToEntity(view);


            invMod.ProjectManagementTaskToEmployee.UpdateProjectManagementTaskToEmployee(projectManagementTaskToEmployee).Apply();

            ProjectManagementTaskToEmployeeView retView = await invMod.ProjectManagementTaskToEmployee.Query().GetViewById(projectManagementTaskToEmployee.TaskToEmployeeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ProjectManagementTaskToEmployeeId}")]
        [ProducesResponseType(typeof(ProjectManagementTaskToEmployeeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProjectManagementTaskToEmployeeView(long projectManagementTaskToEmployeeId)
        {
            ProjectManagementTaskToEmployeeModule invMod = new ProjectManagementTaskToEmployeeModule();

            ProjectManagementTaskToEmployeeView view = await invMod.ProjectManagementTaskToEmployee.Query().GetViewById(projectManagementTaskToEmployeeId);
            return Ok(view);
        }
        }
	}
        