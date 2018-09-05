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
using MillenniumERP.PackingSlipDomain;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;
using ERP_Core2.AccountPayableDomain;

namespace MillenniumERP.InventoryDomain
{
    public class InventoryView
    {
        public InventoryView() { }
        public InventoryView(Inventory inventory)
        {
            this.InventoryId = inventory.InventoryId;
            this.ItemId = inventory.ItemId;
            this.Description = inventory.Description;
            this.Remarks = inventory.Remarks;
            this.UnitOfMeasure = inventory.UnitOfMeasure;
            this.Quantity = inventory.Quantity;
            this.ExtendedPrice = inventory.ExtendedPrice;
            this.DistributionAccountId = inventory.DistributionAccountId;
    }
        public long InventoryId { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string UnitOfMeasure { get; set; }
        public int? Quantity { get; set; }
        public decimal? ExtendedPrice { get; set; }
        public long? DistributionAccountId { get; set; }
    }
    public class InventoryRepository: Repository<Inventory>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public InventoryRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateInventoryByPackingSlipView(PackingSlipView view)
        {
            int count = 0;
            try
            {
                foreach (var item in view.PackingSlipDetailViews)
                {
                    var query = await (from e in _dbContext.Inventories
                                       where e.ItemId == item.ItemId
                                       && e.PackingSlipDetailId==item.PackingSlipDetailId
                                       select e).FirstOrDefaultAsync<Inventory>();
                    if (query == null)
                    {
                        Inventory inventory = new Inventory();

                        applicationViewFactory.MapPackingSlipIntoInventoryEntity(ref inventory, item);

                        AddObject(inventory);
                        count++;
                   
                    }
                }
                if (count == 0) { return CreateProcessStatus.AlreadyExists; } else { return CreateProcessStatus.Insert; }

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            try
            {
                var query = await GetObjectAsync(inventory.InventoryId);

                Inventory inventoryBase = query;
              
                UpdateObject(inventoryBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
     
            }
        public bool DeleteItemMaster(Inventory inventory)
        {
            try
            {
                DeleteObject(inventory);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
     
        }
    }
}
