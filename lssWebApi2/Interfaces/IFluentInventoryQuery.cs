using ERP_Core2.InventoryDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentInventoryQuery
    {
        Task<ItemMaster> GetItemMasterById(long itemId);
        Task<Inventory> GetInventoryById(long inventoryId);
        Task<PackingSlipDetail> GetPackingSlipDetailById(long? packingSlipId);
        Task<ChartOfAccts> GetDistributionAccountById(long? accountId);
        Task<InventoryView> GetInventoryViewbyId(long inventoryId);
    }
}
