using lssWebApi2.InventoryDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IFluentInventoryQuery
    {
        Task<Inventory> GetEntityById(long ? inventoryId);
        Task<InventoryView> GetViewById(long? inventoryId);
        Task<NextNumber> GetInventoryNextNumber();
        Task<Inventory> MapToEntity(InventoryView inputObject);
        Task<InventoryView> MapToView(Inventory inputObject);
        Task<Inventory> GetInventoryByNumber(long inventoryNumber);
        Task<InventoryView> GetInventoryViewByNumber(long inventoryNumber);
        Task<IList<Inventory>> MapToEntity(IList<InventoryView> inputObjects);
    }
}
