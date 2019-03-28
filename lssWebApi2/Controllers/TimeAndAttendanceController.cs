using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
        [HttpGet]
        [Route("TAPunchPage/{employeeId}/{pageNumber}/{pageSize}")]
        //[ProducesResponseType(typeof(TimeAndAttendanceViewContainer), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetTimeAndAttendanceByPage(long employeeId,int pageNumber,int pageSize)
        public async Task<TimeAndAttendanceViewContainer> GetTimeAndAttendanceByPage(long employeeId, int pageNumber, int pageSize)
        {
            //int pageSize = 1;
            //int pageNumber = 1;
            //int employeeId = 3;

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            Func<TimeAndAttendancePunchIn, bool> predicate = e => e.EmployeeId == employeeId;
            Func<TimeAndAttendancePunchIn, object> order = e => e.PunchinDateTime;

            TimeAndAttendanceViewContainer container = await taMod.TimeAndAttendance.Query().GetTimeAndAttendanceViewsByPage(predicate, order, pageSize, pageNumber);

            return (container);

            //return Ok(container);
        }
        [HttpPost]
        [Route("TAManualPunchOut")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> ManualPunchOut(TimeAndAttendanceParam param)
        {
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchIn taPunchin = null;
            TimeAndAttendancePunchInView view = null;
            //long employeeId=1;
            //string account = "1200.215";
            //int mealDeduction = 30;
            //int manual_elapsedHours = 12;
            //int manual_elapsedMinutes = 30;

            TimeAndAttendancePunchIn openTA = await taMod.TimeAndAttendance.Query().IsPunchOpen(param.EmployeeId, param.AsOfDate);

            if (openTA != null)
            {
               taMod.TimeAndAttendance.UpdatePunchIn(openTA, param.MealDeduction, param.Manual_ElapsedHours, param.Manual_ElapsedMinutes).Apply();

                taPunchin = await taMod.TimeAndAttendance.Query().GetPunchInById(openTA.TimePunchinId);

                view = taMod.TimeAndAttendance.Query().MapToView(taPunchin);
            }
            else
            {
                taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(param.EmployeeId, param.Account, param.AsOfDate);

                taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();

                taPunchin = await taMod.TimeAndAttendance.Query().GetPunchOpen(param.EmployeeId);

                taMod.TimeAndAttendance.UpdatePunchIn(taPunchin, param.MealDeduction, param.Manual_ElapsedHours, param.Manual_ElapsedMinutes).Apply();

                taPunchin = await taMod.TimeAndAttendance.Query().GetPunchInById(taPunchin.TimePunchinId);

                view = taMod.TimeAndAttendance.Query().MapToView(taPunchin);
            }
     

            return Ok(view);
            //Assert.NotNull(taPunchin.PunchinDate);
        }
        [HttpPost]
        [Route("TAPunchout")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePunch([FromBody]TimeAndAttendanceParam param)
        {

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchIn taPunchin = null;

            TimeAndAttendanceTimeView currentTime = await taMod.TimeAndAttendance.Query().GetUTCAdjustedTime();
            DateTime asOfDate = currentTime.PunchDate;

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchOpen(param.EmployeeId);

            taMod.TimeAndAttendance.UpdatePunchIn(taPunchin, param.MealDeduction).Apply();

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

            taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(param.EmployeeId, param.Account,asOfDate);
            taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();

            TimeAndAttendancePunchInView view = null;
            view = await taMod.TimeAndAttendance.Query().GetPunchOpenView(param.EmployeeId);

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
