

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ItemMasterDomain;

namespace lssWebApi2.ItemMasterDomain
{ 

public interface IFluentItemMaster
    {
        IFluentItemMasterQuery Query();
        IFluentItemMaster Apply();
        IFluentItemMaster AddItemMaster(ItemMaster itemMaster);
        IFluentItemMaster UpdateItemMaster(ItemMaster itemMaster);
        IFluentItemMaster DeleteItemMaster(ItemMaster itemMaster);
     	IFluentItemMaster UpdateItemMasters(IList<ItemMaster> newObjects);
        IFluentItemMaster AddItemMasters(List<ItemMaster> newObjects);
        IFluentItemMaster DeleteItemMasters(List<ItemMaster> deleteObjects);
        IFluentItemMaster CreateItemMaster(ItemMaster itemMaster);
    }
}
