using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.TimeAndAttendanceDomain;
using lssAngular2;
using lssAngular2.Controllers;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular3.Controllers
{
   

    [Route("api/[controller]")]
    public class TimeAndAttendanceController : Controller
    {
        string _baseUrl;
        public TimeAndAttendanceController(IOptions<AppSettings> settings)
        {
            AppSettings _settings = settings.Value;
            this._baseUrl = _settings.BaseUrl;
        }


        [HttpGet]
        [Route("TAPunchPage/{employeeId}/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(TimeAndAttendanceViewContainer), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTimeAndAttendanceByPage(long employeeId, int pageNumber, int pageSize)
        {
            DataService ds = new DataService(this._baseUrl);
            TimeAndAttendanceViewContainer container = await ds.GetAsync<TimeAndAttendanceViewContainer>("api/TimeAndAttendance/TAPunchPage/" + employeeId.ToString()+"/"+pageNumber.ToString()+"/"+pageSize.ToString());
            return Ok(container);

        }
        [HttpGet]
        [Route("TAOpenPunch/{employeeId}")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOpenPunch(long employeeId)
        {

            DataService ds = new DataService(this._baseUrl);
            TimeAndAttendancePunchInView view = await ds.GetAsync<TimeAndAttendancePunchInView>("api/TimeAndAttendance/TAOpenPunch/"+employeeId.ToString());
            return Ok(view);
        }
        [HttpPost]
        [Route("TAPunchout")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePunch([FromBody]TimeAndAttendanceParam param)
        {
            DataService ds = new DataService(this._baseUrl);
            TimeAndAttendancePunchInView view = await ds.PostAsync<TimeAndAttendanceParam,TimeAndAttendancePunchInView>("api/TimeAndAttendance/TAPunchout",param);
            return Ok(view);
        }

        
        [HttpPost]
        [Route("TAPunchin")]
        [ProducesResponseType(typeof(TimeAndAttendancePunchInView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePunchin([FromBody] TimeAndAttendanceParam param)
        {
            
            DataService ds = new DataService(this._baseUrl);
            TimeAndAttendancePunchInView view = await ds.PostAsync<TimeAndAttendanceParam, TimeAndAttendancePunchInView>("api/TimeAndAttendance/TAPunchin",param);
            return Ok(view);
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
