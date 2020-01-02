using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ProjectManagementWorkOrderToEmployeeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderToEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectManagementWorkOrderToEmployee([FromBody]ProjectManagementWorkOrderToEmployeeView view)
        {
            ProjectManagementWorkOrderToEmployeeModule invMod = new ProjectManagementWorkOrderToEmployeeModule();

            NextNumber nnProjectManagementWorkOrderToEmployee = await invMod.ProjectManagementWorkOrderToEmployee.Query().GetNextNumber();

            view.WorkOrderToEmployeeNumber = nnProjectManagementWorkOrderToEmployee.NextNumberValue;

            ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee = await invMod.ProjectManagementWorkOrderToEmployee.Query().MapToEntity(view);

            invMod.ProjectManagementWorkOrderToEmployee.AddProjectManagementWorkOrderToEmployee(projectManagementWorkOrderToEmployee).Apply();

            ProjectManagementWorkOrderToEmployeeView newView = await invMod.ProjectManagementWorkOrderToEmployee.Query().GetViewByNumber(view.WorkOrderToEmployeeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderToEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectManagementWorkOrderToEmployee([FromBody]ProjectManagementWorkOrderToEmployeeView view)
        {
            ProjectManagementWorkOrderToEmployeeModule invMod = new ProjectManagementWorkOrderToEmployeeModule();
            ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee = await invMod.ProjectManagementWorkOrderToEmployee.Query().MapToEntity(view);
            invMod.ProjectManagementWorkOrderToEmployee.DeleteProjectManagementWorkOrderToEmployee(projectManagementWorkOrderToEmployee).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderToEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectManagementWorkOrderToEmployee([FromBody]ProjectManagementWorkOrderToEmployeeView view)
        {
            ProjectManagementWorkOrderToEmployeeModule invMod = new ProjectManagementWorkOrderToEmployeeModule();

            ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee = await invMod.ProjectManagementWorkOrderToEmployee.Query().MapToEntity(view);


            invMod.ProjectManagementWorkOrderToEmployee.UpdateProjectManagementWorkOrderToEmployee(projectManagementWorkOrderToEmployee).Apply();

            ProjectManagementWorkOrderToEmployeeView retView = await invMod.ProjectManagementWorkOrderToEmployee.Query().GetViewById(projectManagementWorkOrderToEmployee.WorkOrderToEmployeeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ProjectManagementWorkOrderToEmployeeId}")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderToEmployeeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProjectManagementWorkOrderToEmployeeView(long projectManagementWorkOrderToEmployeeId)
        {
            ProjectManagementWorkOrderToEmployeeModule invMod = new ProjectManagementWorkOrderToEmployeeModule();

            ProjectManagementWorkOrderToEmployeeView view = await invMod.ProjectManagementWorkOrderToEmployee.Query().GetViewById(projectManagementWorkOrderToEmployeeId);
            return Ok(view);
        }
        }
	}
        