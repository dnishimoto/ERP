using ERP_Core2.AutoMapper;
using ERP_Core2.ChartOfAccountsDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.ItemMasterDomain;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentInventoryQuery : IFluentInventoryQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentInventoryQuery() { }
        public FluentInventoryQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<InventoryView> GetInventoryViewByNumber(long inventoryNumber)
        {
            Inventory inventory=await _unitOfWork.inventoryRepository.GetInventoryByNumber(inventoryNumber);
            InventoryView view = await MapToInventoryView(inventory);

            if (inventory != null)
            {
                view=await SetViewDependencies(view);
            }

            return view;
        }

        public async Task<NextNumber> GetInventoryNextNumber()
        {
            return await _unitOfWork.inventoryRepository.GetNextNumber(TypeOfNextNumberEnum.InventoryNumber.ToString());
        }
        public async Task<Inventory> MapToInventoryEntity(InventoryView inputObject)
        {
            Mapper mapper = new Mapper();
            Inventory inventory = mapper.Map<Inventory>(inputObject);
            await Task.Yield();
            return inventory;
        }
        public async Task<Inventory> GetInventoryByNumber(long inventoryNumber)
        {
            return await _unitOfWork.inventoryRepository.GetInventoryByNumber(inventoryNumber);
        }
        public async Task<InventoryView> GetInventoryViewbyId(long inventoryId)
        {
            Inventory inventory = await GetInventoryById(inventoryId);
            InventoryView view = await MapToInventoryView(inventory);

            if (inventory != null)
            {
                view=await SetViewDependencies(view);
            }
               
            return view;

        }
        public async Task<InventoryView> SetViewDependencies(InventoryView view)
        {
            Task<ItemMaster> itemMasterTask = GetItemMasterById(view.ItemId);
            Task<ChartOfAccts> accountTask = GetDistributionAccountById(view.DistributionAccountId);
            Task<PackingSlipDetail> packingSlipDetailTask = GetPackingSlipDetailById(view.PackingSlipDetailId);

            Task.WaitAll(itemMasterTask, accountTask, packingSlipDetailTask);

            ItemMaster itemMaster = await itemMasterTask;
            ChartOfAccts distributionAccount = await accountTask;
            PackingSlipDetail packingSlipDetail = await packingSlipDetailTask;

            if (itemMaster != null) view.ItemMasterView = await MapToItemMasterView(itemMaster);
            if (distributionAccount != null) view.DistributionAccountView = await MapToChartAccountView(await accountTask);
            if (packingSlipDetail != null) view.PackingSlipDetailView = await MapToPackingSlipDetailView(await packingSlipDetailTask);
            return view;
        }

        public async Task<ChartOfAccountView> MapToChartAccountView(ChartOfAccts inputObject)
        {
            Mapper mapper = new Mapper();
            ChartOfAccountView view = mapper.Map<ChartOfAccountView>(inputObject);
            await Task.Yield();
            return view;
        }
        public async Task<PackingSlipDetailView> MapToPackingSlipDetailView(PackingSlipDetail inputObject)
        {
            Mapper mapper = new Mapper();
            PackingSlipDetailView view = mapper.Map<PackingSlipDetailView>(inputObject);
            await Task.Yield();
            return view;
        }

        public async Task<ItemMasterView> MapToItemMasterView(ItemMaster inputObject)
        {
            Mapper mapper = new Mapper();
            ItemMasterView view = mapper.Map<ItemMasterView>(inputObject);
            await Task.Yield();
            return view;
        }
        public async Task<InventoryView> MapToInventoryView(Inventory inputObject)
        {
            
            Mapper mapper = new Mapper();
            InventoryView view = mapper.Map<InventoryView>(inputObject);
            await Task.Yield();
            return view;
        }

        public async Task<ItemMaster> GetItemMasterById(long itemId) {
            try
            {
                return await _unitOfWork.itemMasterRepository.GetItemMasterById(itemId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Inventory> GetInventoryById(long inventoryId) {
            return await _unitOfWork.inventoryRepository.GetInventoryById(inventoryId);
        }
        public async Task<PackingSlipDetail> GetPackingSlipDetailById(long? packingSlipId){
            return await _unitOfWork.packingSlipRepository.GetPackingSlipDetailById(packingSlipId);
        }
        public async Task<ChartOfAccts> GetDistributionAccountById(long? accountId) {
            return await _unitOfWork.chartOfAccountRepository.GetChartOfAccountById(accountId);
        }
    }
}
