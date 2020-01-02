   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.CustomerDomain
{
    public class CustomerView
    {
        public long CustomerId { get; set; }
        public long AddressId { get; set; }
        public long? PrimaryShippedToLocationAddressId { get; set; }
        public long? PrimaryEmailId { get; set; }
        public long? PrimaryPhoneId { get; set; }
        public long? MailingLocationAddressId { get; set; }
        public long? PrimaryBillingLocationAddressId { get; set; }
        public string TaxIdentification { get; set; }
        public long CustomerNumber { get; set; }

        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountEmailPassword { get; set; }
        public bool AccountEmailLogin { get; set; }

        public string ShipToAddressLine1 { get; set; }
        public string ShipToAddressLine2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToZipcode { get; set; }

        public string MailingAddressLine1 { get; set; }
        public string MailingAddressLine2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingZipcode { get; set; }

        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingZipcode { get; set; }

        public string PhoneNumber { get; set; }


    }
    public class CustomerRepository: Repository<Customer>, ICustomerRepository
    {
        ListensoftwaredbContext _dbContext;
        public CustomerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<Customer> GetEntityByAddressId(long ? addressId)
        {
            try
            {
                var query = await (from e in _dbContext.Customer
                                   where e.AddressId == addressId
                                   select e).FirstOrDefaultAsync<Customer>();
                return query;

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Customer>GetEntityById(long ? customerId)
        {
			try{
            return await _dbContext.FindAsync<Customer>(customerId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Customer> GetEntityByNumber(long customerNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Customer
                               where detail.CustomerNumber == customerNumber
                               select detail).FirstOrDefaultAsync<Customer>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		
  }
}
