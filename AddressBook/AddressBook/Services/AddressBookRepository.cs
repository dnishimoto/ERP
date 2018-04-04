using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Millennium.EntityFramework;


namespace Millennium.Services
{
    public class AddressBookRepository
    {
        public void UpdateAddressBook(Millennium.EntityFramework.AddressBook addressBook)
        {
            using (var db = new Entities())
            {
                db.Entry(addressBook).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }


        public void DeleteAddressBook(long paramAddressId)
        {
            using (var db = new Entities())
            {
                var addressBookDelete = db.AddressBooks.Single(e => e.AddressId == paramAddressId);

                db.AddressBooks.Remove(addressBookDelete);
                db.SaveChanges();
            }
        }
        
        public void AddAddressBook(AddressBook addressBook)
        {
            using (var db = new Entities())
            {
                db.AddressBooks.Add(addressBook);
                db.SaveChanges();
            }
        }
        public async Task<AddressBook> GetAddressBook(int addressId)
        {
            using (var db = new Entities())
            {
                Task<Millennium.EntityFramework.AddressBook> result = db.AddressBooks.FindAsync(addressId);
                return await result;
            }
            
        }
        public async Task<List<AddressBook>> GetAddressBooks(string keyCode)
        {
            using (var db = new Entities())
            {

                Task<List<AddressBook>> resultList = (from a in db.AddressBooks
                                                                      join b in db.UDCs on a.PeopleXrefId equals b.XRefId
                                                                      where b.KeyCode == keyCode
                                                                      orderby a.Name
                                                                      select a

                    ).ToListAsync<AddressBook>();

                    return await resultList;

            }
        }
    }
}
