using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.TimeAndAttendanceDomain;
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
        [HttpGet]
        [Route("TAViews")]
        //http://localhost:61612/api/TimeAndAttendance/TAViews?StartDate=10/1/2018&EndDate=10/14/2018

        public List<TimeAndAttendanceView> Get([FromQuery] FilterTimeAndAttendance filter)
        {

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            List<TimeAndAttendanceView> views = taMod.TimeAndAttendance.Query().GetTimeAndAttendanceViewsByDate(filter.StartDate, filter.EndDate);

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
