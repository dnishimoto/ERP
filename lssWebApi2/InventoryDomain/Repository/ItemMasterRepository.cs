using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ItemMasterDomain
{
    public class ItemMasterView
    {
        public ItemMasterView() { }
        public ItemMasterView(ItemMaster itemMaster)
        {
            this.ItemId = itemMaster.ItemId;
            this.Description = itemMaster.Description;
            this.UnitOfMeasure = itemMaster.UnitOfMeasure;
            this.CommodityCode = itemMaster.CommodityCode;
            this.Description2 = itemMaster.Description2;
            this.ItemNumber = itemMaster.ItemNumber;
            this.UnitPrice = itemMaster.UnitPrice;
            this.Branch = itemMaster.Branch;
            this.Weight = itemMaster.Weight;
            this.WeightUnitOfMeasure = itemMaster.WeightUnitOfMeasure;
            this.Volume = itemMaster.Volume;
            this.VolumeUnitOfMeasure = itemMaster.VolumeUnitOfMeasure;
        }

        public long ItemId { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CommodityCode { get; set; }
        public string Description2 { get; set; }
        public string ItemNumber { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Branch { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnitOfMeasure { get; set; }
        public decimal? Volume { get; set; }
        public string VolumeUnitOfMeasure { get; set; }
    }
    public class ItemMasterRepository : Repository<ItemMaster>,IItemMasterRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public ItemMasterRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
       
        public async Task<ItemMaster> GetItemMasterById(long itemId)
        {
            try { 
            ItemMaster item = await _dbContext.ItemMaster.FindAsync(itemId);
            return item;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<bool> CreateItemMaster(ItemMaster itemMaster)
        {
            try
            {
                var query = await (from e in _dbContext.ItemMaster
                                   where e.ItemNumber == itemMaster.ItemNumber
                                   select e).FirstOrDefaultAsync<ItemMaster>();
                if (query == null)
                {
                    AddObject(itemMaster);
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateItemMaster(ItemMaster itemMaster)
        {
            try
            {
                var query = await GetObjectAsync(itemMaster.ItemId);

                ItemMaster itemMasterBase = query;

                
                
                UpdateObject(itemMasterBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
     
            }
        public bool DeleteItemMaster(ItemMaster itemMaster)
        {
            try
            {
                DeleteObject(itemMaster);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
     
        }
    }
}
