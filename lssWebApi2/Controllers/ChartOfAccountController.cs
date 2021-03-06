﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ChartOfAccountsDomain;
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
        [Route("PersonalExpense")]
        public async Task<IList<ChartOfAccountView>> GetPersonalExpenseCoa()
        {
            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            string company = "1000";
            string busUnit = "1200";
            string objectNumber = "502";
            IList<ChartOfAccountView> list = await coaMod.ChartOfAccount.Query().GetViewsByAccount(company, busUnit, objectNumber, "");
            return list;

        }
        [HttpGet]
        [Route("CoaViews")]
        //http://localhost:61612/api/ChartOfAccount/CoaViews?AccountIds=3&AccountIds=4&AccountIds=5
        public async Task<IList<ChartOfAccountView>> Get([FromQuery] FilterChartOfAccount filter)
        {

            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            IList<ChartOfAccountView> list = await coaMod.ChartOfAccount.Query().GetViewsByIds(filter.AccountIds);

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
