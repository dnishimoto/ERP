using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.BudgetDomain;
using ERP_Core2.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[Controller]")]
    public class BudgetController : Controller
    {
        // GET: api/<controller>
        [HttpGet("{budgetId}")]
        public BudgetView Get(long budgetId)
        {

            BudgetModule budgetMod = new BudgetModule();

            BudgetView budgetView = budgetMod.Budget.Query().GetBudgetView(budgetId);

            return budgetView;
        }
        [HttpGet]
        public IEnumerable<BudgetView> Get()
        {
     

            BudgetModule budgetMod = new BudgetModule();

            IEnumerable<BudgetView> budgetViews = budgetMod.Budget.Query().GetBudgetViews();

            return budgetViews;

        }
        [HttpGet]
        [Route("PersonalBudgetViews")]
        public List<PersonalBudgetView> GetPersonalBudgetViews()
        {
            BudgetModule budgetMod = new BudgetModule();
            List<PersonalBudgetView> list = budgetMod.Budget.Query().GetPersonalBudgetViews();
            return list;

        }

        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //   return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        [Route("Payment")]
        public void PostPayment([FromBody] PersonalBudgetView budget)
        {
            BudgetModule budgetMod = new BudgetModule();

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
