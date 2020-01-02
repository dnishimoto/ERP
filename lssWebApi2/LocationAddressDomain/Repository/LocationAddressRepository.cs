   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.CustomerDomain;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.LocationAddressDomain
{
    public partial class LocationAddressView
    {
     
        public long LocationAddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public long TypeXrefId { get; set; }
        public long AddressId { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public long LocationAddressNumber { get; set; }

        public string Name { get; set; }

    }
    public class LocationAddressRepository: Repository<LocationAddress>, ILocationAddressRepository
    {
        ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;

        public LocationAddressRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<IList<LocationAddress>> GetLocationAddressByCustomerId(long ? customerId)
        {
            try
            {
           
                Customer customer = await (from e in _dbContext.Customer
                                           where e.CustomerId == customerId
                                           select e).FirstOrDefaultAsync<Customer>();


                 List < LocationAddress > list =

                  await  (from e in _dbContext.LocationAddress
                    where e.AddressId == customer.AddressId
                    select e).ToListAsync<LocationAddress>();

               
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<IList<LocationAddress>> GetEntitiesByAddressId(long? addressId)
        {
            IList<LocationAddress> query = await (from e in _dbContext.LocationAddress
                                           where e.AddressId == addressId
                                           select e).ToListAsync<LocationAddress>();
            return query;
        }
        public async Task<IList<LocationAddress>> GetEntitiesByCustomerId(long? customerId)
        {
            IList<LocationAddress> query = await (from locationAddress in _dbContext.LocationAddress
                                                  join customer in _dbContext.Customer
                                                  on locationAddress.AddressId equals customer.AddressId
                                                  where customer.CustomerId == customerId
                                                  select locationAddress).ToListAsync<LocationAddress>();
            return query;
        }

        public async Task<LocationAddress> GetEntityByCustomer(CustomerView customerView)
        {

            try

            {
                    LocationAddress locationAddress = await (from e in _dbContext.LocationAddress
                                       where e.LocationAddressId == customerView.PrimaryBillingLocationAddressId
                               
                                       select e
                                ).FirstOrDefaultAsync<LocationAddress>();
          
                return locationAddress;

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        

        public async Task<LocationAddress>GetEntityById(long ? locationAddressId)
        {
			try{
            return await _dbContext.FindAsync<LocationAddress>(locationAddressId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<LocationAddress> GetEntityByNumber(long locationAddressNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.LocationAddress
                               where detail.LocationAddressNumber == locationAddressNumber
                               select detail).FirstOrDefaultAsync<LocationAddress>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		

		
  }
}
