using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular2.Controllers
{
   
    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        
        // GET: api/<controller>
        [HttpGet("{budgetId}")]
        public async Task<BudgetView> Get(long budgetId)
        {
            DataService ds = new DataService();
            BudgetView view =  await ds.GetAsync<BudgetView>("api/Budget/" + budgetId.ToString());
            /*
            BudgetView view = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Budget/" + budgetId.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var BudgetResponse = Res.Content.ReadAsStringAsync().Result;
                    view = JsonConvert.DeserializeObject<BudgetView>(BudgetResponse);

                }

            }
            */
            return view;
        }

        [HttpGet()]
        public async Task<List<BudgetView>> Get()
        {
            DataService ds = new DataService();
            List<BudgetView> views = await ds.GetAsync<List<BudgetView>>("api/Budget");
            /*

            List <BudgetView> views = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Budget");

                if (Res.IsSuccessStatusCode)
                {
                    var BudgetResponse = Res.Content.ReadAsStringAsync().Result;
                    views = JsonConvert.DeserializeObject<List<BudgetView>>(BudgetResponse);

                }

            }
            */
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
