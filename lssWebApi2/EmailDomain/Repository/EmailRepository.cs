   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AddressBookDomain
{
    public class EmailEntityView
    {
        public long EmailId { get; set; }
        public string Password { get; set; }
        public bool? LoginEmail { get; set; }
        public string Email { get; set; }
        public long EmailEntityNumber { get; set; }
        public long AddressId { get; set; }

        public string Name { get; set; }
    }

    public class EmailRepository: Repository<EmailEntity>, IEmailRepository
    {
        ListensoftwaredbContext _dbContext;
        public EmailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IList<EmailEntity>> GetEmailsByAddressId(long addressId)
        {
            try
            {
           
                IList<EmailEntity> list = await (from addressBook in _dbContext.AddressBook
                                           join emails in _dbContext.EmailEntity
                                           on addressBook.AddressId equals emails.AddressId
                                           select emails).ToListAsync<EmailEntity>() ;

                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<EmailEntity> FindAccountEmailbyAddressId(long ? addressId, string email)
        {

            EmailEntity query = await (from e in _dbContext.EmailEntity
                                  where e.Email == email
                                  && e.LoginEmail == true
                                  && e.AddressId == addressId
                                  select e).FirstOrDefaultAsync<EmailEntity>();
            return query;
        }

        public async Task<IList<EmailEntity>> GetEmailByCustomerId(long ? customerId)
        {
            try
            {

                Customer customer = await _dbContext.FindAsync<Customer>(customerId);

                long addressId = customer.AddressId;
                List<EmailEntity> list = await

                (from e in _dbContext.EmailEntity
                 where e.AddressId == addressId
                 select e).ToListAsync<EmailEntity>();
              
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
     
        public async Task<EmailEntity>GetEntityById(long ? emailId)
        {
			try{
            return await _dbContext.FindAsync<EmailEntity>(emailId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<EmailEntity> GetEntityByNumber(long emailEntityNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.EmailEntity
                               where detail.EmailEntityNumber == emailEntityNumber
                               select detail).FirstOrDefaultAsync<EmailEntity>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
   
      

		
  }
}
