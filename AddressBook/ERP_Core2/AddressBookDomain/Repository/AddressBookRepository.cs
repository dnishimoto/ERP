using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using MillenniumERP.CustomerDomain;
using MillenniumERP.Services;

namespace MillenniumERP.AddressBookDomain
{

    public class AddressBookRepository: Repository<AddressBook>
    {
        Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AddressBookRepository(DbContext db):base(db)
        {
            _dbContext = (Entities) db;
            applicationViewFactory = new ApplicationViewFactory();
        }
   
        public List<Phone> GetPhonesByAddressId(long addressId)
        {
            try
            {
                var resultList = base.GetObjectsQueryable(e => e.AddressId == addressId, "phones").FirstOrDefault();

                List<Phone> phoneList = new List<Phone>();
                foreach (var item in resultList.Phones)
                {
                    phoneList.Add(item);
                }
                return phoneList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public List<Email> GetEmailsByAddressId(long addressId)
        {
            try
            {
                var resultList = base.GetObjectsQueryable(e => e.AddressId == addressId, "emails").FirstOrDefault();

                List<Email> emailList = new List<Email>();
                foreach (var item in resultList.Emails)
                {
                    emailList.Add(item);
                }
                return emailList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<CreateProcessStatus> CreateAddressBook(CustomerView customerView)
        {
            long addressId = 0;
            try
            {
                AddressBook lookupAddressBook = await base.GetAddressBookByCustomerView(customerView);

                if (lookupAddressBook == null)
                {
                    AddressBook addressBook = new AddressBook();

                    applicationViewFactory.MapAddressBookEntity(ref addressBook, customerView);
                    AddObject(addressBook);
                    return CreateProcessStatus.Inserted;
                }

                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<List<AddressBook>> GetAddressBookByName(string name)
        {
            try
            {

                Task<List<AddressBook>> resultList = (from a in _dbContext.AddressBooks
                                                      join b in _dbContext.Supervisors on
                                                      a.AddressId equals b.AddressId
                                                      where a.Name.Contains(name)
                                                      orderby a.Name
                                                      select a

                    ).ToListAsync<AddressBook>();

                return await resultList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<AddressBook> GetAddressBookByAddressId(long addressId)
        {
            try
            {

                AddressBook ab = await GetObjectAsync(addressId);

                return ab;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
       
    }
}
