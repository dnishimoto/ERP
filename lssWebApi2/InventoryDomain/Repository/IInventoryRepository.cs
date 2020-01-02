using lssWebApi2.AccountPayableDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetInventoryByNumber(long inventoryNumber);
        Task<Inventory> GetEntityById(long ? inventoryId);
        Task<bool> UpdateInventory(Inventory inventory);
        bool DeleteItemMaster(Inventory inventory);
        Task<Inventory> FindEntityByExpression(Expression<Func<Inventory, bool>> predicate);
    }
}
