using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.SupervisorDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SupervisorController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SupervisorView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSupervisor([FromBody]SupervisorView view)
        {
            SupervisorModule invMod = new SupervisorModule();

            NextNumber nnSupervisor = await invMod.Supervisor.Query().GetNextNumber();

            view.SupervisorNumber = nnSupervisor.NextNumberValue;

            Supervisor supervisor = await invMod.Supervisor.Query().MapToEntity(view);

            invMod.Supervisor.AddSupervisor(supervisor).Apply();

            SupervisorView newView = await invMod.Supervisor.Query().GetViewByNumber(view.SupervisorNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(SupervisorView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSupervisor([FromBody]SupervisorView view)
        {
            SupervisorModule invMod = new SupervisorModule();
            Supervisor supervisor = await invMod.Supervisor.Query().MapToEntity(view);
            invMod.Supervisor.DeleteSupervisor(supervisor).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SupervisorView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSupervisor([FromBody]SupervisorView view)
        {
            SupervisorModule invMod = new SupervisorModule();

            Supervisor supervisor = await invMod.Supervisor.Query().MapToEntity(view);


            invMod.Supervisor.UpdateSupervisor(supervisor).Apply();

            SupervisorView retView = await invMod.Supervisor.Query().GetViewById(supervisor.SupervisorId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{SupervisorId}")]
        [ProducesResponseType(typeof(SupervisorView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSupervisorView(long supervisorId)
        {
            SupervisorModule invMod = new SupervisorModule();

            SupervisorView view = await invMod.Supervisor.Query().GetViewById(supervisorId);
            return Ok(view);
        }
        }
	}
        