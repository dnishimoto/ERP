using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AddressBookController : Controller
    {
        [Route("People")]
        public List<AddressBookView> GetPeople()
        {
            AddressBookModule abMod = new AddressBookModule();
            List<AddressBookView> list = abMod.AddressBook.Query().GetAddressBookByName("");

            return list;
        }
        // GET: api/<controller>
        [Route("People/{searchName}")]
        //[HttpGet("{searchName}")]
       
        public List<AddressBookView> GetPeople(string searchName)
        {
             AddressBookModule abMod = new AddressBookModule();
            List<AddressBookView> list = abMod.AddressBook.Query().GetAddressBookByName(searchName);

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
