using lssWebApi2.AutoMapper;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentItemMasterQuery
{
    Task<ItemMaster> MapToEntity(ItemMasterView inputObject);
    Task<List<ItemMaster>> MapToEntity(List<ItemMasterView> inputObjects);
    Task<ItemMasterView> MapToView(ItemMaster inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ItemMaster> GetEntityById(long ? itemMasterId);
    Task<ItemMaster> GetEntityByNumber(long itemMasterNumber);
    Task<ItemMasterView> GetViewById(long ? itemMasterId);
    Task<ItemMasterView> GetViewByNumber(long itemMasterNumber);
    Task<ItemMaster> GetEntityByItemCode(string branch,string itemNumber);
}
