using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.GeneralLedgerDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class GeneralLedgerController : Controller
    {
  
       
        [Route("ById/{generalLedgerId}")]
        [HttpGet]
        public async Task<GeneralLedgerView> GetByAccountId(long generalLedgerId)
        {
            GeneralLedgerModule glMod = new GeneralLedgerModule();
            return glMod.GeneralLedger.Query().GetLedgerViewById(generalLedgerId);
        }

       
        // GET: api/<controller>
        [Route("BySummary/{fiscalYear}")]
        [HttpGet]
        public IEnumerable<AccountSummaryView> GetSummary(int fiscalYear)
        {
  
            GeneralLedgerModule glMod = new GeneralLedgerModule();

            IEnumerable<AccountSummaryView> list = glMod.GeneralLedger.Query().GetAccountSummaryByFiscalYearViews(fiscalYear);

            return list;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
