using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.JobCostLedgerDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class JobCostLedgerController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(JobCostLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddJobCostLedger([FromBody]JobCostLedgerView view)
        {
            JobCostLedgerModule invMod = new JobCostLedgerModule();

            NextNumber nnJobCostLedger = await invMod.JobCostLedger.Query().GetNextNumber();

            view.JobCostLedgerNumber = nnJobCostLedger.NextNumberValue;

            JobCostLedger jobCostLedger = await invMod.JobCostLedger.Query().MapToEntity(view);

            invMod.JobCostLedger.AddJobCostLedger(jobCostLedger).Apply();

            JobCostLedgerView newView = await invMod.JobCostLedger.Query().GetViewByNumber(view.JobCostLedgerNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(JobCostLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteJobCostLedger([FromBody]JobCostLedgerView view)
        {
            JobCostLedgerModule invMod = new JobCostLedgerModule();
            JobCostLedger jobCostLedger = await invMod.JobCostLedger.Query().MapToEntity(view);
            invMod.JobCostLedger.DeleteJobCostLedger(jobCostLedger).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(JobCostLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateJobCostLedger([FromBody]JobCostLedgerView view)
        {
            JobCostLedgerModule invMod = new JobCostLedgerModule();

            JobCostLedger jobCostLedger = await invMod.JobCostLedger.Query().MapToEntity(view);


            invMod.JobCostLedger.UpdateJobCostLedger(jobCostLedger).Apply();

            JobCostLedgerView retView = await invMod.JobCostLedger.Query().GetViewById(jobCostLedger.JobCostLedgerId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{JobCostLedgerId}")]
        [ProducesResponseType(typeof(JobCostLedgerView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetJobCostLedgerView(long jobCostLedgerId)
        {
            JobCostLedgerModule invMod = new JobCostLedgerModule();

            JobCostLedgerView view = await invMod.JobCostLedger.Query().GetViewById(jobCostLedgerId);
            return Ok(view);
        }
        }
	}
        