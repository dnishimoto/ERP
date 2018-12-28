using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AddressBookDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular2.Controllers
{
    [Route("api/[controller]")]
    public class AddressBookController : Controller
    {
        [HttpGet("{id}")]
        public async Task<AddressBookView> Get(int id)
        {
            DataService ds = new DataService();
            AddressBookView view = await ds.GetAsync<AddressBookView>("api/AddressBook/"+id.ToString());
            return view;
        }
        // GET: api/<controller>
        [Route("People")]
        public async Task<List<AddressBookView>> GetPeople()
        {
            DataService ds = new DataService();
            List<AddressBookView> views = await ds.GetAsync<List<AddressBookView>>("api/AddressBook/People");

            return views;
        }

        [Route("People/{searchName}")]
        //[HttpGet]
       
        public async Task<List<AddressBookView>> GetPeople( string searchName)
        {
            DataService ds = new DataService();
            List<AddressBookView> views = await ds.GetAsync<List<AddressBookView>>("api/AddressBook/People/"+searchName??"");

            return views;
        }

        // GET api/<controller>/5
       

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
