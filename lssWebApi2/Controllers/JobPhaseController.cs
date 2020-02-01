using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.JobPhaseDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class JobPhaseController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(JobPhaseView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddJobPhase([FromBody]JobPhaseView view)
        {
            JobPhaseModule invMod = new JobPhaseModule();

            NextNumber nnJobPhase = await invMod.JobPhase.Query().GetNextNumber();

            view.JobPhaseNumber = nnJobPhase.NextNumberValue;

            JobPhase jobPhase = await invMod.JobPhase.Query().MapToEntity(view);

            invMod.JobPhase.AddJobPhase(jobPhase).Apply();

            JobPhaseView newView = await invMod.JobPhase.Query().GetViewByNumber(view.JobPhaseNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(JobPhaseView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteJobPhase([FromBody]JobPhaseView view)
        {
            JobPhaseModule invMod = new JobPhaseModule();
            JobPhase jobPhase = await invMod.JobPhase.Query().MapToEntity(view);
            invMod.JobPhase.DeleteJobPhase(jobPhase).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(JobPhaseView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateJobPhase([FromBody]JobPhaseView view)
        {
            JobPhaseModule invMod = new JobPhaseModule();

            JobPhase jobPhase = await invMod.JobPhase.Query().MapToEntity(view);


            invMod.JobPhase.UpdateJobPhase(jobPhase).Apply();

            JobPhaseView retView = await invMod.JobPhase.Query().GetViewById(jobPhase.JobPhaseId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{JobPhaseId}")]
        [ProducesResponseType(typeof(JobPhaseView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetJobPhaseView(long jobPhaseId)
        {
            JobPhaseModule invMod = new JobPhaseModule();

            JobPhaseView view = await invMod.JobPhase.Query().GetViewById(jobPhaseId);
            return Ok(view);
        }
        }
	}
        