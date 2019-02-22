using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AccountsReceivableDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular2.Controllers
{
    [Route("api/[controller]")]
    public class AccountReceivableController : Controller
    {
        string _baseUrl;
        public AccountReceivableController(IOptions<AppSettings> settings)
        {
            AppSettings _settings = settings.Value;
            this._baseUrl = _settings.BaseUrl;
        }
        // GET: api/<controller>
        // GET api/<controller>/5
        [HttpGet()]
        [Route("OpenReceivables")]
        public async Task<IEnumerable<AccountReceivableFlatView>> GetOpenReceivables()
        {
            DataService ds = new DataService(this._baseUrl);
            IEnumerable<AccountReceivableFlatView> views = await ds.GetAsync<List<AccountReceivableFlatView>>("api/AccountReceivable/OpenReceivables");

            return views;
          
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
