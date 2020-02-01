using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.AbstractFactory;
using lssWebApi2.CustomerDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lssWebApi2.AddressBookDomain
{
    public class AddressBookView
    {
          
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


        public IQueryable<AddressBook> GetEntitiesByExpression(Expression<Func<AddressBook, bool>> predicate)
        {
            //IQueryable<SalesOrder> result = _dbContext.Set<SalesOrder>().Where(predicate).AsQueryable<SalesOrder>();
            var result = _dbContext.Set<AddressBook>().Where(predicate);
            return result;
        }

        public async Task<AddressBook> GetEntityByCustomerId(long? customerId)
        {
            var query = await (from detail in _dbContext.AddressBook
                               join customer in _dbContext.Customer
                                on detail.AddressId equals customer.AddressId
                               where customer.CustomerId == customerId
                               select detail
                               ).FirstOrDefaultAsync<AddressBook>();
            return query;
        }
        public async Task<AddressBook> GetEntityByAccountEmail(string email)
        {
            AddressBook query = await (from e in _dbContext.AddressBook

                                       join f in _dbContext.EmailEntity

                                           on e.AddressId equals f.AddressId

                                       where f.Email == email

                                       && f.LoginEmail == true

                                       select e).FirstOrDefaultAsync<AddressBook>();
            return query;

        }

        public async Task<AddressBook> GetAddressBookByCustomerView(CustomerView customerView)
        {
            try
            {
           
                var query = await (from e in _dbContext.AddressBook
                                   join f in _dbContext.EmailEntity on e.AddressId equals f.AddressId
                                   where e.Name == customerView.CustomerName &&
                                   f.Email == customerView.AccountEmail
                                   && f.LoginEmail == true
                                   select e).FirstOrDefaultAsync<AddressBook>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<long> GetAddressIdByCustomerId(long? customerId)
        {
            try
            {
              

                Customer customer = await (from e in _dbContext.Customer
                                           where e.CustomerId == customerId
                                           select e).FirstOrDefaultAsync<Customer>();

                return customer.AddressId;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }

        public List<AddressBook> GetEntityByName(string name)
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

                          
                List<AddressBook> list = new List<AddressBook>();

                foreach (var item in query)
                {
                    if (item != null)
                    {
                        // AddressBookView abv = applicationViewFactory.MapAddressBookView((AddressBook)item);
                        //views.Add(abv);
                        list.Add(item);
                    }
               }
            
             
                return list ;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<AddressBook> GetEntityById(long ? employeeId)
        {
            return await _dbContext.FindAsync<AddressBook>(employeeId);
        }

        public async Task<AddressBook> FindEntityByAddressIdAndEmail(long addressId, string email)
        {
            AddressBook query = await (from e in _dbContext.AddressBook
                                       join f in _dbContext.EmailEntity
                                           on e.AddressId equals f.AddressId
                                       where f.Email == email
                                       && f.LoginEmail == true
                                       && e.AddressId==addressId
                                       select e).FirstOrDefaultAsync<AddressBook>();
            return query;
        }

      

    }
}
