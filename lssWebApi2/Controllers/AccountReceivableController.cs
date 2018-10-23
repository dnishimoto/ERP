using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AccountsReceivableDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AccountReceivableController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        [Route("OpenReceivables")]
        //http://localhost:61612/api/AccountReceivable/OpenReceivables
        public IEnumerable<AccountReceivableFlatView> Get()
        {
            AccountsReceivableModule acctRecMod = new AccountsReceivableModule();
            List<AccountReceivableFlatView> list = acctRecMod.AccountsReceivable.Query().GetOpenAccountReceivables();
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
