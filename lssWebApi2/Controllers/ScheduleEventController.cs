using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ScheduleEventDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleEventController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ScheduleEventView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddScheduleEvent([FromBody]ScheduleEventView view)
        {
            ScheduleEventModule invMod = new ScheduleEventModule();

            NextNumber nnScheduleEvent = await invMod.ScheduleEvent.Query().GetNextNumber();

            view.ScheduleEventNumber = nnScheduleEvent.NextNumberValue;

            ScheduleEvent scheduleEvent = await invMod.ScheduleEvent.Query().MapToEntity(view);

            invMod.ScheduleEvent.AddScheduleEvent(scheduleEvent).Apply();

            ScheduleEventView newView = await invMod.ScheduleEvent.Query().GetViewByNumber(view.ScheduleEventNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ScheduleEventView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteScheduleEvent([FromBody]ScheduleEventView view)
        {
            ScheduleEventModule invMod = new ScheduleEventModule();
            ScheduleEvent scheduleEvent = await invMod.ScheduleEvent.Query().MapToEntity(view);
            invMod.ScheduleEvent.DeleteScheduleEvent(scheduleEvent).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ScheduleEventView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateScheduleEvent([FromBody]ScheduleEventView view)
        {
            ScheduleEventModule invMod = new ScheduleEventModule();

            ScheduleEvent scheduleEvent = await invMod.ScheduleEvent.Query().MapToEntity(view);


            invMod.ScheduleEvent.UpdateScheduleEvent(scheduleEvent).Apply();

            ScheduleEventView retView = await invMod.ScheduleEvent.Query().GetViewById(scheduleEvent.ScheduleEventId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ScheduleEventId}")]
        [ProducesResponseType(typeof(ScheduleEventView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetScheduleEventView(long scheduleEventId)
        {
            ScheduleEventModule invMod = new ScheduleEventModule();

            ScheduleEventView view = await invMod.ScheduleEvent.Query().GetViewById(scheduleEventId);
            return Ok(view);
        }
        }
	}
        