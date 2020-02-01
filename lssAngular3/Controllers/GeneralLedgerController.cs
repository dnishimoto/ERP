using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.GeneralLedgerDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular2.Controllers
{
    [Route("api/[controller]")]
    public class GeneralLedgerController : Controller
    {
        string _baseUrl;
        public GeneralLedgerController(IOptions<AppSettings> settings)
        {
            AppSettings _settings = settings.Value;
            this._baseUrl = _settings.BaseUrl;
        }
        [Route("IncomeStatementAccounts/{fiscalYear}")]
        [HttpGet]
        public async Task<String[]> GetIncomeStatementAccounts(int fiscalYear)
        {
            DataService ds = new DataService(this._baseUrl);
            string[] array = await ds.GetAsync<String[]>("api/GeneralLedger/IncomeStatementAccounts/" + fiscalYear.ToString());
            return array;

        }
        [Route("IncomeStatementViews/{fiscalYear}")]
        [HttpGet]

        public async Task<IList<IncomeStatementView>> GetIncomeStatementViews(int fiscalYear)
        {
            DataService ds = new DataService(this._baseUrl);
            IList <IncomeStatementView> views = await ds.GetAsync<List<IncomeStatementView>>("api/GeneralLedger/IncomeStatementViews/"+fiscalYear.ToString());
            return views;
        }

        [Route("IncomeShortView")]
        [HttpPost]
        public async Task PostIncomeView([FromBody]IncomeShortView income)
        {
            DataService ds = new DataService(this._baseUrl);
            bool result=await ds.PostAsync<IncomeShortView,bool>("api/GeneralLedger/IncomeShortView", income);
        }
        [Route("IncomeViews")]
        [HttpGet]
        public async Task<IEnumerable<IncomeView>> GetIncomeViews()
        {
            DataService ds = new DataService(this._baseUrl);
            IEnumerable<IncomeView> views = await ds.GetAsync<List<IncomeView>>("api/GeneralLedger/IncomeViews");
            return views;
        }
        
        [Route("BySummary/{fiscalYear}")]
        [HttpGet]
        public async Task<IEnumerable<AccountSummaryView>> GetSummary(int fiscalYear)
        {
            DataService ds = new DataService(this._baseUrl);
            IEnumerable<AccountSummaryView> views = await ds.GetAsync<List<AccountSummaryView>>("api/GeneralLedger/BySummary/" + fiscalYear.ToString());
            return views;
        }
        
        
        [Route("ById/{generalLedgerId}")]
        [HttpGet]
        public async Task<GeneralLedgerView> Get(long generalLedgerId)
        {
            DataService ds = new DataService(this._baseUrl);
            GeneralLedgerView view = await ds.GetAsync<GeneralLedgerView>("api/GeneralLedger/ById/" + generalLedgerId.ToString());
            
            return view;
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
