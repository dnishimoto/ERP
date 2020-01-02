

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.ItemMasterDomain
{
public interface IItemMasterRepository
    {
        Task<ItemMaster> GetEntityById(long ? itemMasterId);
	
        Task<ItemMaster> GetEntityByItemCode(string branch, string itemNumber);
    }
}
