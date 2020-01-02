

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.AddressBookDomain
{
    public class PhoneEntityView
    {
        public long PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string Extension { get; set; }
        public long AddressId { get; set; }
        public long PhoneEntityNumber { get; set; }

        public string Name { get; set; }

    }
    public class PhoneRepository : Repository<PhoneEntity>, IPhoneRepository
    {
        ListensoftwaredbContext _dbContext;
        public PhoneRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<PhoneEntity> GetEntityById(long ? phonesId)
        {
            try
            {
                return await _dbContext.FindAsync<PhoneEntity>(phonesId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<PhoneEntity>> GetEntitiesByAddressId(long? addressId)
        {
            try
            {
                IList<PhoneEntity> list = await (from detail in _dbContext.PhoneEntity
                                           where detail.AddressId == addressId
                                           select detail).ToListAsync<PhoneEntity>();

                return list;                  

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<IList<PhoneEntity>> GetPhonesByCustomerId(long? customerId)
        {
            try
            {
                Customer customer = await (from detail in _dbContext.Customer
                                           where detail.CustomerId == customerId
                                           select detail).FirstOrDefaultAsync<Customer>();

                long addressId = customer.AddressId;

                List<PhoneEntity> list = await

                 (from e in _dbContext.PhoneEntity
                  where e.AddressId == addressId
                  select e).ToListAsync<PhoneEntity>();

                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<PhoneEntity> GetEntityByNumber(long phoneEntityNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.PhoneEntity
                                   where detail.PhoneEntityNumber == phoneEntityNumber
                                   select detail).FirstOrDefaultAsync<PhoneEntity>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    

    }
}
