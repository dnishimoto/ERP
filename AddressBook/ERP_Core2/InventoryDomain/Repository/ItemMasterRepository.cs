using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.Services;
using ERP_Core2.AbstractFactory;
using System.Collections;

namespace MillenniumERP.ItemMasterDomain
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
            this.ItemPriceGroup = itemMaster.ItemPriceGroup;
            this.Description2 = itemMaster.Description2;
            this.ItemNumber = itemMaster.ItemNumber;
    }

        public long ItemId { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CommodityCode { get; set; }
        public string ItemPriceGroup { get; set; }
        public string Description2 { get; set; }
        public string ItemNumber { get; set; }

    }
    public class ItemMasterRepository: Repository<ItemMaster>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public ItemMasterRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
   
        public async Task<bool> UpdateItemMaster(ItemMaster itemMaster)
        {
            try
            {
                var query = GetObjectAsync((int)itemMaster.ItemId);

                ItemMaster itemMasterBase = query.Result;

                
                
                UpdateObject(itemMasterBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
     
            }
        public async Task<bool> DeleteItemMaster(ItemMaster itemMaster)
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
