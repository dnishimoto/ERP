using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.Services;
using lssWebApi2.AddressBookDomain.Repository;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AddressBookDomain
{
    public class AddressBookView
    {
        public AddressBookView() { }
        public AddressBookView(AddressBook item)
        {
            this.AddressId = item.AddressId;
            this.Name = item.Name;
            this.FirstName = item.FirstName;
            this.LastName = item.LastName;
            this.CompanyName = item.CompanyName;

            this.CategoryCodeChar1 = item.CategoryCodeChar1;
            this.CategoryCodeChar2 = item.CategoryCodeChar2;
            this.CategoryCodeChar3 = item.CategoryCodeChar3;
            this.CategoryCodeInt1 = item.CategoryCodeInt1;
            this.CategoryCodeInt2 = item.CategoryCodeInt2;
            this.CategoryCodeInt3 = item.CategoryCodeInt3;
            this.CategoryCodeDate1 = item.CategoryCodeDate1;
            this.CategoryCodeDate2 = item.CategoryCodeDate2;
            this.CategoryCodeDate3 = item.CategoryCodeDate3;

        }
       
        public long AddressId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CategoryCodeChar1 { get; set; }
        public string CategoryCodeChar2 { get; set; }
        public string CategoryCodeChar3 { get; set; }
        public int? CategoryCodeInt1 { get; set; }
        public int? CategoryCodeInt2 { get; set; }
        public int? CategoryCodeInt3 { get; set; }
        public DateTime? CategoryCodeDate1 { get; set; }
        public DateTime? CategoryCodeDate2 { get; set; }
        public DateTime? CategoryCodeDate3 { get; set; }

    }

    public class AddressBookRepository: Repository<AddressBook>, IAddressBookRepository
    {
        ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AddressBookRepository(DbContext db):base(db)
        {
            _dbContext = (ListensoftwaredbContext) db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public void MapAddressBookEntity(ref AddressBook addressBook, AddressBookView addressBookView)
        {
            applicationViewFactory.MapAddressBookEntity(ref addressBook, addressBookView);
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
        public List<AddressBookView> GetAddressBookByName(string name)
        {
            try
            {
                IQueryable<AddressBook> query;
                query = (from e in _dbContext.AddressBook
                         select e);
                if (string.IsNullOrWhiteSpace(name)==false)
                {
                    query = query.Where(e => e.Name.StartsWith(name));
                    //query = base.GetObjectsQueryable(e => e.Name !=null);
                }

                          
                List<AddressBookView> views = new List<AddressBookView>();

                foreach (var item in query)
                {
                    if (item != null)
                    {
                        AddressBookView abv = applicationViewFactory.MapAddressBookView((AddressBook)item);
                        views.Add(abv);
                    }
               }
            
             
                return views ;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }

        public async Task<AddressBookView> GetAddressBookViewByAddressId(long addressId)
        {
            try
            {

                AddressBook ab = await GetObjectAsync(addressId);

                AddressBookView view = applicationViewFactory.MapAddressBookView(ab);
                
                return view;
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
