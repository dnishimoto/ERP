using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.TimeAndAttendanceSetupDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class TimeAndAttendanceSetupController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceSetupView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTimeAndAttendanceSetup([FromBody]TimeAndAttendanceSetupView view)
        {
            TimeAndAttendanceSetupModule invMod = new TimeAndAttendanceSetupModule();

            NextNumber nnTimeAndAttendanceSetup = await invMod.TimeAndAttendanceSetup.Query().GetNextNumber();

            view.TimeAndAttendanceSetupNumber = nnTimeAndAttendanceSetup.NextNumberValue;

            TimeAndAttendanceSetup timeAndAttendanceSetup = await invMod.TimeAndAttendanceSetup.Query().MapToEntity(view);

            invMod.TimeAndAttendanceSetup.AddTimeAndAttendanceSetup(timeAndAttendanceSetup).Apply();

            TimeAndAttendanceSetupView newView = await invMod.TimeAndAttendanceSetup.Query().GetViewByNumber(view.TimeAndAttendanceSetupNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceSetupView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTimeAndAttendanceSetup([FromBody]TimeAndAttendanceSetupView view)
        {
            TimeAndAttendanceSetupModule invMod = new TimeAndAttendanceSetupModule();
            TimeAndAttendanceSetup timeAndAttendanceSetup = await invMod.TimeAndAttendanceSetup.Query().MapToEntity(view);
            invMod.TimeAndAttendanceSetup.DeleteTimeAndAttendanceSetup(timeAndAttendanceSetup).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceSetupView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTimeAndAttendanceSetup([FromBody]TimeAndAttendanceSetupView view)
        {
            TimeAndAttendanceSetupModule invMod = new TimeAndAttendanceSetupModule();

            TimeAndAttendanceSetup timeAndAttendanceSetup = await invMod.TimeAndAttendanceSetup.Query().MapToEntity(view);


            invMod.TimeAndAttendanceSetup.UpdateTimeAndAttendanceSetup(timeAndAttendanceSetup).Apply();

            TimeAndAttendanceSetupView retView = await invMod.TimeAndAttendanceSetup.Query().GetViewById(timeAndAttendanceSetup.TimeAndAttendanceSetupId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{TimeAndAttendanceSetupId}")]
        [ProducesResponseType(typeof(TimeAndAttendanceSetupView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTimeAndAttendanceSetupView(long timeAndAttendanceSetupId)
        {
            TimeAndAttendanceSetupModule invMod = new TimeAndAttendanceSetupModule();

            TimeAndAttendanceSetupView view = await invMod.TimeAndAttendanceSetup.Query().GetViewById(timeAndAttendanceSetupId);
            return Ok(view);
        }
        }
	}
        