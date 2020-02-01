using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.JobCostTypeDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class JobCostTypeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(JobCostTypeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddJobCostType([FromBody]JobCostTypeView view)
        {
            JobCostTypeModule invMod = new JobCostTypeModule();

            NextNumber nnJobCostType = await invMod.JobCostType.Query().GetNextNumber();

            view.JobCostTypeNumber = nnJobCostType.NextNumberValue;

            JobCostType jobCostType = await invMod.JobCostType.Query().MapToEntity(view);

            invMod.JobCostType.AddJobCostType(jobCostType).Apply();

            JobCostTypeView newView = await invMod.JobCostType.Query().GetViewByNumber(view.JobCostTypeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(JobCostTypeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteJobCostType([FromBody]JobCostTypeView view)
        {
            JobCostTypeModule invMod = new JobCostTypeModule();
            JobCostType jobCostType = await invMod.JobCostType.Query().MapToEntity(view);
            invMod.JobCostType.DeleteJobCostType(jobCostType).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(JobCostTypeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateJobCostType([FromBody]JobCostTypeView view)
        {
            JobCostTypeModule invMod = new JobCostTypeModule();

            JobCostType jobCostType = await invMod.JobCostType.Query().MapToEntity(view);


            invMod.JobCostType.UpdateJobCostType(jobCostType).Apply();

            JobCostTypeView retView = await invMod.JobCostType.Query().GetViewById(jobCostType.JobCostTypeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{JobCostTypeId}")]
        [ProducesResponseType(typeof(JobCostTypeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetJobCostTypeView(long jobCostTypeId)
        {
            JobCostTypeModule invMod = new JobCostTypeModule();

            JobCostTypeView view = await invMod.JobCostType.Query().GetViewById(jobCostTypeId);
            return Ok(view);
        }
        }
	}
        