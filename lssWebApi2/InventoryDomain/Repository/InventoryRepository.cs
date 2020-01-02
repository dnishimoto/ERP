using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.PackingSlipDetailDomain;
using System.Linq.Expressions;

namespace lssWebApi2.InventoryDomain
{
    public class InventoryView
    {
       
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
        public async Task<Inventory> GetEntityById(long ? inventoryId) {
            try
            {
                Inventory item = await _dbContext.Inventory.FindAsync(inventoryId);
                return item;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
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
        public async Task<Inventory> FindEntityByExpression(Expression<Func<Inventory, bool>> predicate)
        {
            IQueryable<Inventory> result = _dbContext.Set<Inventory>().Where(predicate);

            return await result.FirstOrDefaultAsync<Inventory>();
        }
    }
}
