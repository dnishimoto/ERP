using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using ERP_Core2.ItemMasterDomain;
using ERP_Core2.ChartOfAccountsDomain;
using lssWebApi2.InventoryDomain.Repository;

namespace ERP_Core2.InventoryDomain
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
            this.PackingSlipDetailId = inventory.PackingSlipDetailId;
    }
        public long InventoryId { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string UnitOfMeasure { get; set; }
        public int? Quantity { get; set; }
        public decimal? ExtendedPrice { get; set; }
        public long? DistributionAccountId { get; set; }
        public string Branch { get; set; }
        public long InventoryNumber { get; set; }
        public long? PackingSlipDetailId { get; set; }

        public ItemMasterView ItemMasterView { get; set; }
        public ChartOfAccountView DistributionAccountView { get; set; }
        public PackingSlipDetailView PackingSlipDetailView { get; set; }
       

    }
    public class InventoryRepository: Repository<Inventory>, IInventoryRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public InventoryRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
         public async Task<Inventory> GetInventoryByNumber(long inventoryNumber)
        {
            try
            {
                Inventory inventory = await _dbContext.Inventory.Where(m => m.InventoryNumber == inventoryNumber).FirstOrDefaultAsync<Inventory>();
                return inventory;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Inventory> GetInventoryById(long inventoryId) {
            try
            {
                Inventory item = await _dbContext.Inventory.FindAsync(inventoryId);
                return item;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreateInventoryByPackingSlipView(PackingSlipView view)
        {
            int count = 0;
            try
            {
                foreach (var item in view.PackingSlipDetailViews)
                {
                    var query = await (from e in _dbContext.Inventory
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
