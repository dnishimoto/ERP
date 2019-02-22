using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AddressBookDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssAngular2.Controllers
{
    [Route("api/[controller]")]
    public class AddressBookController : Controller
    {
        string _baseUrl;
        public AddressBookController(IOptions<AppSettings> settings)
        {
            AppSettings _settings = settings.Value;
            this._baseUrl = _settings.BaseUrl;
        }
        [HttpGet("{id}")]
        public async Task<AddressBookView> Get(int id)
        {
            DataService ds = new DataService(this._baseUrl);
            AddressBookView view = await ds.GetAsync<AddressBookView>("api/AddressBook/"+id.ToString());
            return view;
        }
        // GET: api/<controller>
        [Route("People")]
        public async Task<List<AddressBookView>> GetPeople()
        {
            DataService ds = new DataService(this._baseUrl);
            List<AddressBookView> views = await ds.GetAsync<List<AddressBookView>>("api/AddressBook/People");

            return views;
        }

        [Route("People/{searchName}")]
        //[HttpGet]
       
        public async Task<List<AddressBookView>> GetPeople( string searchName)
        {
            DataService ds = new DataService(this._baseUrl);
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
        [HttpPut]
        public async Task Put([FromBody]AddressBookView addressBookView)
        {
            DataService ds = new DataService();
            await ds.PutAsync<AddressBookView>("api/AddressBook/", addressBookView);
      
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
