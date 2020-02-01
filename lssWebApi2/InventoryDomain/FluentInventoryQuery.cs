using lssWebApi2.AutoMapper;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class FluentInventoryQuery : MapperAbstract<Inventory,InventoryView>, IFluentInventoryQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentInventoryQuery() { }
        public FluentInventoryQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<InventoryView> GetInventoryViewByNumber(long inventoryNumber)
        {
            Inventory inventory=await _unitOfWork.inventoryRepository.GetInventoryByNumber(inventoryNumber);
            InventoryView view = await MapToView(inventory);

            if (inventory != null)
            {
                view=await SetViewDependencies(view);
            }

            return view;
        }

        public async Task<NextNumber> GetInventoryNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfInventory.InventoryNumber.ToString());
        }
        public override async Task<Inventory> MapToEntity(InventoryView inputObject)
        {
       
            Inventory inventory = mapper.Map<Inventory>(inputObject);
            await Task.Yield();
            return inventory;
        }
        public override async Task<IList<Inventory>> MapToEntity(IList<InventoryView> inputObjects)
        {
            IList<Inventory> list = new List<Inventory>();

            foreach (var item in inputObjects)
            {
                Inventory outObject = mapper.Map<Inventory>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public async Task<Inventory> GetInventoryByNumber(long inventoryNumber)
        {
            return await _unitOfWork.inventoryRepository.GetInventoryByNumber(inventoryNumber);
        }
        public override async Task<InventoryView> GetViewById(long ? inventoryId)
        {
            Inventory inventory = await GetEntityById(inventoryId);
            InventoryView view = await MapToView(inventory);

            if (inventory != null)
            {
                view=await SetViewDependencies(view);
            }
               
            return view;

        }
        public async Task<InventoryView> SetViewDependencies(InventoryView view)
        {
            Task<ItemMaster> itemMasterTask = _unitOfWork.itemMasterRepository.GetEntityById(view.ItemId);
            Task<ChartOfAccount> accountTask = _unitOfWork.chartOfAccountRepository.GetEntityById(view.DistributionAccountId);
        
            Task.WaitAll(itemMasterTask, accountTask);

            ItemMaster itemMaster = await itemMasterTask;
            ChartOfAccount distributionAccount = await accountTask;

            FluentItemMaster fluentItemMaster = new FluentItemMaster(_unitOfWork);
            FluentChartOfAccount fluentChartOfAccount = new FluentChartOfAccount(_unitOfWork);


            if (itemMaster != null) view.ItemMasterView = await fluentItemMaster.Query().MapToView(itemMaster);
            if (distributionAccount != null) view.DistributionAccountView = await fluentChartOfAccount.Query().MapToView(await accountTask);
              return view;
        }

    
      

        
        public override async Task<InventoryView> MapToView(Inventory inputObject)
        {
            InventoryView view = mapper.Map<InventoryView>(inputObject);
            await Task.Yield();
            return view;
        }

       
        public override async Task<Inventory> GetEntityById(long ? inventoryId) {
            return await _unitOfWork.inventoryRepository.GetEntityById(inventoryId);
        }
      
       
    }
}
