   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.CarrierDomain
{
    public class CarrierView
    {
        public long? CarrierId { get; set; }
        public long AddressId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierType { get; set; }
        public long CarrierNumber { get; set; }
        public long CarrierTypeXrefId { get; set; }



    }
    public class CarrierRepository: Repository<Carrier>, ICarrierRepository
    {
        ListensoftwaredbContext _dbContext;
        public CarrierRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<Carrier>GetEntityById(long ? carrierId)
        {
			try{
            return await _dbContext.FindAsync<Carrier>(carrierId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Carrier> GetEntityByNumber(long carrierNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Carrier
                               where detail.CarrierNumber == carrierNumber
                               select detail).FirstOrDefaultAsync<Carrier>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
	

		
  }
}
