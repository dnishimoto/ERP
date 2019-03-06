using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    public class FilterTimeAndAttendance
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    [Route("api/[controller]")]
    public class TimeAndAttendanceController : Controller
    {
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "reached";
        }
        //[HttpGet("{FilterTimeAndAttendance}")]
        [HttpPost]
        [Route("TAPunchout")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePunch([FromBody]TimeAndAttendanceParam param)
        {

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchIn taPunchin = null;

            TimeAndAttendanceTimeView currentTime = await taMod.TimeAndAttendance.Query().GetUTCAdjustedTime();
            DateTime asOfDate = currentTime.PunchDate;

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchOpen(param.employeeId);

            taMod.TimeAndAttendance.UpdatePunchIn(taPunchin, param.mealDeduction).Apply();

            TimeAndAttendancePunchInView view = null;
            //view = await taMod.TimeAndAttendance.Query().GetPunchOpenView(param.employeeId);
            view = await taMod.TimeAndAttendance.Query().GetPunchInByIdView(taPunchin.TimePunchinId);
                
            return Ok(view);

        }
        [HttpPost]
        [Route("TAPunchin")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePunchin([FromBody]TimeAndAttendanceParam param)
        {
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchIn taPunchin = null; 

            TimeAndAttendanceTimeView currentTime = await taMod.TimeAndAttendance.Query().GetUTCAdjustedTime();
            DateTime asOfDate = currentTime.PunchDate;

            taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(param.employeeId, param.account);
            taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();
                       
            TimeAndAttendancePunchInView view = null;
            view = await taMod.TimeAndAttendance.Query().GetPunchOpenView(param.employeeId);

            return Ok(view);

        }
        [Route("TAOpenPunch/{employeeId}")]
        [HttpGet]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOpenPunch(long employeeId)
        {
       
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchInView view = null;
            view = await taMod.TimeAndAttendance.Query().GetPunchOpenView(employeeId);
            return Ok(view);
        }
        [HttpGet]
        [Route("TAViews")]
        //http://localhost:61612/api/TimeAndAttendance/TAViews?StartDate=10/1/2018&EndDate=10/14/2018

        public async Task<List<TimeAndAttendanceView>> Get([FromQuery] FilterTimeAndAttendance filter)
        {

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            List<TimeAndAttendanceView> views = await taMod.TimeAndAttendance.Query().GetTimeAndAttendanceViewsByDate(filter.StartDate, filter.EndDate);

            return views;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
