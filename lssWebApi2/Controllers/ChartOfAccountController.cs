using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.ChartOfAccountsDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    public class FilterChartOfAccount
    {
        public long[] AccountIds { get; set; }
   
    }
    [Route("api/[controller]")]
    public class ChartOfAccountController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("CoaViews")]
        //http://localhost:61612/api/ChartOfAccount/CoaViews?AccountIds=3&AccountIds=4&AccountIds=5
        public List<ChartOfAccountView> Get([FromQuery] FilterChartOfAccount filter)
        {

            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            List<ChartOfAccountView> list = coaMod.ChartOfAccount.Query().GetChartOfAccountViewsByIds(filter.AccountIds);

            return list;
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
