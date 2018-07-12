using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using MillenniumERP.Services;

namespace MillenniumERP.AddressBookDomain
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
            try
            {
                var resultList = base.GetObjectsAsync(e => e.AddressId == addressId, "phones").FirstOrDefault();

                List<Phone> phoneList = new List<Phone>();
                foreach (var item in resultList.Phones)
                {
                    phoneList.Add(item);
                }
                return phoneList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public List<Email> GetEmailsByAddressId(int addressId)
        {
            try
            {
                var resultList = base.GetObjectsAsync(e => e.AddressId == addressId, "emails").FirstOrDefault();

                List<Email> emailList = new List<Email>();
                foreach (var item in resultList.Emails)
                {
                    emailList.Add(item);
                }
                return emailList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
     

        public async Task<List<AddressBook>> GetAddressBookByAddressId(int addressId)
        {
            try
            {

                Task<List<AddressBook>> resultList = (from a in _dbContext.AddressBooks
                                                      join b in _dbContext.Supervisors on
                                                      a.AddressId equals b.AddressId
                                                      where b.AddressId == addressId
                                                      orderby a.Name
                                                      select a

                    ).ToListAsync<AddressBook>();

                return await resultList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
