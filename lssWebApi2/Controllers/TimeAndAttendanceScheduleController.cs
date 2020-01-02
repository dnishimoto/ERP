using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.TimeAndAttendanceScheduleDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class TimeAndAttendanceScheduleController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduleView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTimeAndAttendanceSchedule([FromBody]TimeAndAttendanceScheduleView view)
        {
            TimeAndAttendanceScheduleModule invMod = new TimeAndAttendanceScheduleModule();

            NextNumber nnTimeAndAttendanceSchedule = await invMod.Schedule.Query().GetNextNumber();

            view.TimeAndAttendanceScheduleNumber = nnTimeAndAttendanceSchedule.NextNumberValue;

            TimeAndAttendanceSchedule timeAndAttendanceSchedule = await invMod.Schedule.Query().MapToEntity(view);

            invMod.Schedule.AddTimeAndAttendanceSchedule(timeAndAttendanceSchedule).Apply();

            TimeAndAttendanceScheduleView newView = await invMod.Schedule.Query().GetViewByNumber(view.TimeAndAttendanceScheduleNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduleView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTimeAndAttendanceSchedule([FromBody]TimeAndAttendanceScheduleView view)
        {
            TimeAndAttendanceScheduleModule invMod = new TimeAndAttendanceScheduleModule();
            TimeAndAttendanceSchedule timeAndAttendanceSchedule = await invMod.Schedule.Query().MapToEntity(view);
            invMod.Schedule.DeleteTimeAndAttendanceSchedule(timeAndAttendanceSchedule).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduleView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTimeAndAttendanceSchedule([FromBody]TimeAndAttendanceScheduleView view)
        {
            TimeAndAttendanceScheduleModule invMod = new TimeAndAttendanceScheduleModule();

            TimeAndAttendanceSchedule timeAndAttendanceSchedule = await invMod.Schedule.Query().MapToEntity(view);


            invMod.Schedule.UpdateTimeAndAttendanceSchedule(timeAndAttendanceSchedule).Apply();

            TimeAndAttendanceScheduleView retView = await invMod.Schedule.Query().GetViewById(timeAndAttendanceSchedule.ScheduleId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{TimeAndAttendanceScheduleId}")]
        [ProducesResponseType(typeof(TimeAndAttendanceScheduleView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTimeAndAttendanceScheduleView(long timeAndAttendanceScheduleId)
        {
            TimeAndAttendanceScheduleModule invMod = new TimeAndAttendanceScheduleModule();

            TimeAndAttendanceScheduleView view = await invMod.Schedule.Query().GetViewById(timeAndAttendanceScheduleId);
            return Ok(view);
        }
        }
	}
        