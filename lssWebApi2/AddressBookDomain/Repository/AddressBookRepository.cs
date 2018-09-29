using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AddressBookDomain
{

    public class AddressBookRepository: Repository<AddressBook>
    {
        ListensoftwareDBContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AddressBookRepository(DbContext db):base(db)
        {
            _dbContext = (ListensoftwareDBContext) db;
            applicationViewFactory = new ApplicationViewFactory();
        }
   
        public List<Phones> GetPhonesByAddressId(long addressId)
        {
            try
            {
                var resultList = base.GetObjectsQueryable(e => e.AddressId == addressId, "phones").FirstOrDefault();

                List<Phones> phoneList = new List<Phones>();
                foreach (var item in resultList.Phones)
                {
                    phoneList.Add(item);
                }
                return phoneList;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public List<Emails> GetEmailsByAddressId(long addressId)
        {
            try
            {
                var resultList = base.GetObjectsQueryable(e => e.AddressId == addressId, "emails").FirstOrDefault();

                List<Emails> emailList = new List<Emails>();
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
            try
            {
                AddressBook lookupAddressBook = await base.GetAddressBookByCustomerView(customerView);

                if (lookupAddressBook == null)
                {
                    AddressBook addressBook = new AddressBook();

                    applicationViewFactory.MapAddressBookEntity(ref addressBook, customerView);
                    AddObject(addressBook);
                    return CreateProcessStatus.Insert;
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

                Task<List<AddressBook>> resultList = (from a in _dbContext.AddressBook
                                                      join b in _dbContext.Supervisor on
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

        public class Phone
        {
        }
    }
}
