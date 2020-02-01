using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ItemMasterDomain
{
public class FluentItemMasterQuery:MapperAbstract<ItemMaster,ItemMasterView>,IFluentItemMasterQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentItemMasterQuery() { }
        public FluentItemMasterQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<ItemMaster> MapToEntity(ItemMasterView inputObject)
        {

            ItemMaster outObject = mapper.Map<ItemMaster>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<ItemMaster>> MapToEntity(IList<ItemMasterView> inputObjects)
        {
            IList<ItemMaster> list = new List<ItemMaster>();

            foreach (var item in inputObjects)
            {
                ItemMaster outObject = mapper.Map<ItemMaster>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<ItemMasterView> MapToView(ItemMaster inputObject)
        {
     
            ItemMasterView outObject = mapper.Map<ItemMasterView>(inputObject);
            ChartOfAccount chartOfAccount = await _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);

            outObject.Account = chartOfAccount.Account;


            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfItemMaster.ItemMasterNumber.ToString());
        }
 public override async Task<ItemMasterView> GetViewById(long ? itemMasterId)
        {
            ItemMaster detailItem = await _unitOfWork.itemMasterRepository.GetEntityById(itemMasterId);

            return await MapToView(detailItem);
        }
 public async Task<ItemMasterView> GetViewByNumber(long itemMasterNumber)
        {
            ItemMaster detailItem = await _unitOfWork.itemMasterRepository.GetEntityByNumber(itemMasterNumber);

            return await MapToView(detailItem);
        }

public override async Task<ItemMaster> GetEntityById(long ? itemMasterId)
        {
            return await _unitOfWork.itemMasterRepository.GetEntityById(itemMasterId);

        }
 public async Task<ItemMaster> GetEntityByNumber(long itemMasterNumber)
        {
            return await _unitOfWork.itemMasterRepository.GetEntityByNumber(itemMasterNumber);
        }
        public async Task<ItemMaster> GetEntityByItemCode(string branch, string itemCode)
        {
            return await _unitOfWork.itemMasterRepository.GetEntityByItemCode(branch, itemCode);
        }
}
}
