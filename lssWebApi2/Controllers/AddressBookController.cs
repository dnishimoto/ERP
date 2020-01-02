using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AddressBookController : Controller
    {
        [HttpGet("{id}")]
        public async Task<AddressBookView> Get(long id)
        {
            AddressBookModule abMod = new AddressBookModule();
            AddressBookView addressBookView = await  abMod.AddressBook.Query().GetViewById(id);
            return (addressBookView);
        }
        [Route("People")]
        public async Task<IList<AddressBookView>> GetPeople()
        {
            AddressBookModule abMod = new AddressBookModule();
            IList<AddressBookView> list = await abMod.AddressBook.Query().GetAddressBookByName("");

            return list;
        }
        // GET: api/<controller>
        [Route("People/{searchName}")]
        //[HttpGet("{searchName}")]
       
        public async Task <IList<AddressBookView>> GetPeople(string searchName)
        {
             AddressBookModule abMod = new AddressBookModule();
            IList<AddressBookView> list = await abMod.AddressBook.Query().GetAddressBookByName(searchName);

            return list;
        }

    
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task Put([FromBody]AddressBookView addressBookView)
        {
            AddressBookModule abMod = new AddressBookModule();

            AddressBook addressBook = await abMod.AddressBook.Query().MapToEntity(addressBookView);

            abMod.AddressBook.UpdateAddressBook(addressBook).Apply();

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
