using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.BudgetDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular2.Controllers
{

    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        string _baseUrl;
        public BudgetController(IOptions<AppSettings> settings)
        {
            AppSettings _settings = settings.Value;
            this._baseUrl = _settings.BaseUrl;
        }

        // GET: api/<controller>
        [HttpGet("{budgetId}")]
        public async Task<BudgetView> Get(long budgetId)
        {
            DataService ds = new DataService(this._baseUrl);
            BudgetView view =  await ds.GetAsync<BudgetView>("api/Budget/" + budgetId.ToString());
          
            return view;

          
        }
        [HttpGet()]
        
        [Route("PersonalBudgetViews")]
        public async Task<List<PersonalBudgetView>> GetPersonalBudgetViews()
        {
            DataService ds = new DataService(this._baseUrl);
            List<PersonalBudgetView> views = await ds.GetAsync<List<PersonalBudgetView>>("api/Budget/PersonalBudgetViews");
            return views;
         
        }
        public async Task<IEnumerable<BudgetView>> Get()
        {
            DataService ds = new DataService(this._baseUrl);
            List<BudgetView> views = await ds.GetAsync<List<BudgetView>>("api/Budget");
           
            return views;
           
        }



        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        // }

        // POST api/<controller>
        [HttpPost]
        [Route("Payment")]
        public async Task Post([FromBody] PersonalBudgetView budget)
        {
           //string info=string.Format("{0}", budget.Account);
            DataService ds = new DataService(this._baseUrl);
            await ds.PostAsync<PersonalBudgetView>("api/budget/Payment",budget);


        }
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
