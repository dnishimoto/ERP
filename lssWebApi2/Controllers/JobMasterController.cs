using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.JobMasterDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class JobMasterController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(JobMasterView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddJobMaster([FromBody]JobMasterView view)
        {
            JobMasterModule invMod = new JobMasterModule();

            NextNumber nnJobMaster = await invMod.JobMaster.Query().GetNextNumber();

            view.JobMasterNumber = nnJobMaster.NextNumberValue;

            JobMaster jobMaster = await invMod.JobMaster.Query().MapToEntity(view);

            invMod.JobMaster.AddJobMaster(jobMaster).Apply();

            JobMasterView newView = await invMod.JobMaster.Query().GetViewByNumber(view.JobMasterNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(JobMasterView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteJobMaster([FromBody]JobMasterView view)
        {
            JobMasterModule invMod = new JobMasterModule();
            JobMaster jobMaster = await invMod.JobMaster.Query().MapToEntity(view);
            invMod.JobMaster.DeleteJobMaster(jobMaster).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(JobMasterView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateJobMaster([FromBody]JobMasterView view)
        {
            JobMasterModule invMod = new JobMasterModule();

            JobMaster jobMaster = await invMod.JobMaster.Query().MapToEntity(view);


            invMod.JobMaster.UpdateJobMaster(jobMaster).Apply();

            JobMasterView retView = await invMod.JobMaster.Query().GetViewById(jobMaster.JobMasterId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{JobMasterId}")]
        [ProducesResponseType(typeof(JobMasterView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetJobMasterView(long jobMasterId)
        {
            JobMasterModule invMod = new JobMasterModule();

            JobMasterView view = await invMod.JobMaster.Query().GetViewById(jobMasterId);
            return Ok(view);
        }
        }
	}
        