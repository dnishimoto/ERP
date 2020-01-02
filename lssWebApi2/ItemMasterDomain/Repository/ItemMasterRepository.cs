   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ItemMasterDomain
{
    public class ItemMasterView
    {
        public long ItemId { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CommodityCode { get; set; }
        public string Description2 { get; set; }
        public string ItemCode { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Branch { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnitOfMeasure { get; set; }
        public decimal? Volume { get; set; }
        public string VolumeUnitOfMeasure { get; set; }
        public long ItemMasterNumber { get; set; }
        public long? AccountId { get; set; }

        public string Account { get; set; }


    }
    public class ItemMasterRepository: Repository<ItemMaster>, IItemMasterRepository
    {
        ListensoftwaredbContext _dbContext;
        public ItemMasterRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<ItemMaster> GetEntityByItemCode(string branch,string itemCode)
        {
            var query = await (from e in _dbContext.ItemMaster
                               where e.ItemCode == itemCode
                               && e.Branch==branch
                               select e).FirstOrDefaultAsync<ItemMaster>();
            return query;
        }

        public async Task<ItemMaster>GetEntityById(long ? itemMasterId)
        {
			try{
            return await _dbContext.FindAsync<ItemMaster>(itemMasterId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<ItemMaster> GetEntityByNumber(long itemMasterNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.ItemMaster
                               where detail.ItemMasterNumber == itemMasterNumber
                               select detail).FirstOrDefaultAsync<ItemMaster>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }



		
  }
}
