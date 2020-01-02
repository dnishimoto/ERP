   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.BuyerDomain
{
    public class BuyerView
    {
        public long BuyerId { get; set; }
        public long AddressId { get; set; }
        public string Title { get; set; }
        public long BuyerNumber { get; set; }

        public string BuyerName { get; set; }
    }

    public class BuyerRepository: Repository<Buyer>, IBuyerRepository
    {
        ListensoftwaredbContext _dbContext;
        public BuyerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<Buyer>GetEntityById(long ? buyerId)
        {
			try{
            return await _dbContext.FindAsync<Buyer>(buyerId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Buyer> GetEntityByNumber(long buyerNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Buyer
                               where detail.BuyerNumber == buyerNumber
                               select detail).FirstOrDefaultAsync<Buyer>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		
  }
}
