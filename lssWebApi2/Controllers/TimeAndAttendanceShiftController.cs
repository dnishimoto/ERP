using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.TimeAndAttendanceShiftDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class TimeAndAttendanceShiftController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceShiftView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTimeAndAttendanceShift([FromBody]TimeAndAttendanceShiftView view)
        {
            TimeAndAttendanceShiftModule invMod = new TimeAndAttendanceShiftModule();

            NextNumber nnTimeAndAttendanceShift = await invMod.TimeAndAttendanceShift.Query().GetNextNumber();

            view.TimeAndAttendanceShiftNumber = nnTimeAndAttendanceShift.NextNumberValue;

            TimeAndAttendanceShift timeAndAttendanceShift = await invMod.TimeAndAttendanceShift.Query().MapToEntity(view);

            invMod.TimeAndAttendanceShift.AddTimeAndAttendanceShift(timeAndAttendanceShift).Apply();

            TimeAndAttendanceShiftView newView = await invMod.TimeAndAttendanceShift.Query().GetViewByNumber(view.TimeAndAttendanceShiftNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceShiftView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTimeAndAttendanceShift([FromBody]TimeAndAttendanceShiftView view)
        {
            TimeAndAttendanceShiftModule invMod = new TimeAndAttendanceShiftModule();
            TimeAndAttendanceShift timeAndAttendanceShift = await invMod.TimeAndAttendanceShift.Query().MapToEntity(view);
            invMod.TimeAndAttendanceShift.DeleteTimeAndAttendanceShift(timeAndAttendanceShift).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(TimeAndAttendanceShiftView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTimeAndAttendanceShift([FromBody]TimeAndAttendanceShiftView view)
        {
            TimeAndAttendanceShiftModule invMod = new TimeAndAttendanceShiftModule();

            TimeAndAttendanceShift timeAndAttendanceShift = await invMod.TimeAndAttendanceShift.Query().MapToEntity(view);


            invMod.TimeAndAttendanceShift.UpdateTimeAndAttendanceShift(timeAndAttendanceShift).Apply();

            TimeAndAttendanceShiftView retView = await invMod.TimeAndAttendanceShift.Query().GetViewById(timeAndAttendanceShift.ShiftId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{TimeAndAttendanceShiftId}")]
        [ProducesResponseType(typeof(TimeAndAttendanceShiftView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTimeAndAttendanceShiftView(long shiftId)
        {
            TimeAndAttendanceShiftModule invMod = new TimeAndAttendanceShiftModule();

            TimeAndAttendanceShiftView view = await invMod.TimeAndAttendanceShift.Query().GetViewById(shiftId);
            return Ok(view);
        }
        }
	}
        