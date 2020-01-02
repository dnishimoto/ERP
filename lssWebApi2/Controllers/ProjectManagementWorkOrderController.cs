using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ProjectManagementWorkOrderDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ProjectManagementWorkOrderController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectManagementWorkOrder([FromBody]ProjectManagementWorkOrderView view)
        {
            ProjectManagementWorkOrderModule invMod = new ProjectManagementWorkOrderModule();

            NextNumber nnProjectManagementWorkOrder = await invMod.ProjectManagementWorkOrder.Query().GetNextNumber();

            view.WorkOrderNumber = nnProjectManagementWorkOrder.NextNumberValue;

            ProjectManagementWorkOrder projectManagementWorkOrder = await invMod.ProjectManagementWorkOrder.Query().MapToEntity(view);

            invMod.ProjectManagementWorkOrder.AddProjectManagementWorkOrder(projectManagementWorkOrder).Apply();

            ProjectManagementWorkOrderView newView = await invMod.ProjectManagementWorkOrder.Query().GetViewByNumber(view.WorkOrderNumber??0);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectManagementWorkOrder([FromBody]ProjectManagementWorkOrderView view)
        {
            ProjectManagementWorkOrderModule invMod = new ProjectManagementWorkOrderModule();
            ProjectManagementWorkOrder projectManagementWorkOrder = await invMod.ProjectManagementWorkOrder.Query().MapToEntity(view);
            invMod.ProjectManagementWorkOrder.DeleteProjectManagementWorkOrder(projectManagementWorkOrder).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectManagementWorkOrder([FromBody]ProjectManagementWorkOrderView view)
        {
            ProjectManagementWorkOrderModule invMod = new ProjectManagementWorkOrderModule();

            ProjectManagementWorkOrder projectManagementWorkOrder = await invMod.ProjectManagementWorkOrder.Query().MapToEntity(view);


            invMod.ProjectManagementWorkOrder.UpdateProjectManagementWorkOrder(projectManagementWorkOrder).Apply();

            ProjectManagementWorkOrderView retView = await invMod.ProjectManagementWorkOrder.Query().GetViewById(projectManagementWorkOrder.WorkOrderId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ProjectManagementWorkOrderId}")]
        [ProducesResponseType(typeof(ProjectManagementWorkOrderView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetProjectManagementWorkOrderView(long projectManagementWorkOrderId)
        {
            ProjectManagementWorkOrderModule invMod = new ProjectManagementWorkOrderModule();

            ProjectManagementWorkOrderView view = await invMod.ProjectManagementWorkOrder.Query().GetViewById(projectManagementWorkOrderId);
            return Ok(view);
        }
        }
	}
        