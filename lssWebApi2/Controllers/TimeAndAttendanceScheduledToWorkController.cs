using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class TimeAndAttendanceScheduledToWorkController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduledToWorkView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTimeAndAttendanceScheduledToWork([FromBody]TimeAndAttendanceScheduledToWorkView view)
        {
            TimeAndAttendanceScheduledToWorkModule invMod = new TimeAndAttendanceScheduledToWorkModule();

            NextNumber nnTimeAndAttendanceScheduledToWork = await invMod.ScheduledToWork.Query().GetNextNumber();

            view.ScheduledToWorkNumber = nnTimeAndAttendanceScheduledToWork.NextNumberValue;

            TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork = await invMod.ScheduledToWork.Query().MapToEntity(view);

            invMod.ScheduledToWork.AddTimeAndAttendanceScheduledToWork(timeAndAttendanceScheduledToWork).Apply();

            TimeAndAttendanceScheduledToWorkView newView = await invMod.ScheduledToWork.Query().GetViewByNumber(view.ScheduledToWorkNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduledToWorkView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTimeAndAttendanceScheduledToWork([FromBody]TimeAndAttendanceScheduledToWorkView view)
        {
            TimeAndAttendanceScheduledToWorkModule invMod = new TimeAndAttendanceScheduledToWorkModule();
            TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork = await invMod.ScheduledToWork.Query().MapToEntity(view);
            invMod.ScheduledToWork.DeleteTimeAndAttendanceScheduledToWork(timeAndAttendanceScheduledToWork).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduledToWorkView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTimeAndAttendanceScheduledToWork([FromBody]TimeAndAttendanceScheduledToWorkView view)
        {
            TimeAndAttendanceScheduledToWorkModule invMod = new TimeAndAttendanceScheduledToWorkModule();

            TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork = await invMod.ScheduledToWork.Query().MapToEntity(view);


            invMod.ScheduledToWork.UpdateTimeAndAttendanceScheduledToWork(timeAndAttendanceScheduledToWork).Apply();

            TimeAndAttendanceScheduledToWorkView retView = await invMod.ScheduledToWork.Query().GetViewById(timeAndAttendanceScheduledToWork.ScheduledToWorkId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{TimeAndAttendanceScheduledToWorkId}")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduledToWorkView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTimeAndAttendanceScheduledToWorkView(long timeAndAttendanceScheduledToWorkId)
        {
            TimeAndAttendanceScheduledToWorkModule invMod = new TimeAndAttendanceScheduledToWorkModule();

            TimeAndAttendanceScheduledToWorkView view = await invMod.ScheduledToWork.Query().GetViewById(timeAndAttendanceScheduledToWorkId);
            return Ok(view);
        }
        }
	}
        