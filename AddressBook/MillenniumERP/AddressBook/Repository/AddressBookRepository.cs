using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.EntityFramework;

namespace MillenniumERP.Services
{
    public class AddressBookRepository: Repository<AddressBook>
    {
        Entities _dbContext;
        public AddressBookRepository(DbContext db):base(db)
        {
            _dbContext = (Entities) db;
        }
        public List<Phone> GetPhonesByAddressId(int addressId)
        {
            var resultList = base.GetObjectsAsync(e => e.AddressId == addressId, "phones").FirstOrDefault();

            List<Phone> phoneList = new List<Phone>();
            foreach (var item in resultList.Phones)
            {
                phoneList.Add(item);
            }
            return phoneList;
        }
        public List<Email> GetEmailsByAddressId(int addressId)
        {
            var resultList = base.GetObjectsAsync(e => e.AddressId == addressId, "emails").FirstOrDefault();

            List<Email> emailList = new List<Email>();
            foreach (var item in resultList.Emails)
            {
                emailList.Add(item);
            }
            return emailList;
        }


        public async Task<List<AddressBook>> GetAddressBooks(string keyCode)
        {


                Task<List<AddressBook>> resultList = (from a in _dbContext.AddressBooks
                                                                      join b in _dbContext.UDCs on a.PeopleXrefId equals b.XRefId
                                                                      where b.KeyCode == keyCode
                                                                      orderby a.Name
                                                                      select a

                    ).ToListAsync<AddressBook>();

                    return await resultList;


        }
    }
}
