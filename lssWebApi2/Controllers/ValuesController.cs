using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            /*
            ListensoftwaredbContext db = new ListensoftwaredbContext();

            var query = (from e in db.Budget
                         where e.BudgetId == 2
                         select e).FirstOrDefault<Budget>();

            */

            //UnitOfWork unitOfWork = new UnitOfWork();
            //Task<NextNumber> nextNumberTask = Task.Run(async()=>await unitOfWork.udcRepository.GetNextNumber("PackingSlipNumber"));
            //Task.WaitAll(nextNumberTask);

            //return new string[] { nextNumberTask.Result.NextNumberName };

            return new string[] { "value1" ,"value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
