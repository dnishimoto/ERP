using ERP_Core2.AccountPayableDomain;
using ERP_Core2.PackingSlipDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetInventoryByNumber(long inventoryNumber);
        Task<Inventory> GetInventoryById(long inventoryId);
        Task<CreateProcessStatus> CreateInventoryByPackingSlipView(PackingSlipView view);
        Task<bool> UpdateInventory(Inventory inventory);
        bool DeleteItemMaster(Inventory inventory);
    }
}
