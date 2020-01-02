using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IFluentInventory
    {
        IFluentInventoryQuery Query();
        IFluentInventory AddInventory(Inventory inventory);
        IFluentInventory UpdateInventory(Inventory inventory);
        IFluentInventory DeleteInventory(Inventory inventory);
        IFluentInventory Apply();
    }
}
