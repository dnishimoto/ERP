using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain.Repository
{
    public interface IItemMasterRepository
    {
        Task<ItemMaster> GetItemMasterById(long itemId);
        Task<bool> CreateItemMaster(ItemMaster itemMaster);
        Task<bool> UpdateItemMaster(ItemMaster itemMaster);
        bool DeleteItemMaster(ItemMaster itemMaster);
    }
}
