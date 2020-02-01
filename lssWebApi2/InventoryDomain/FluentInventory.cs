using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class FluentInventory : IFluentInventory
    {
        

        private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
            
        public FluentInventory(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }

        public IFluentInventory CreateInventoryByPackingSlipView(PackingSlipView view)
        {
            int count = 0;
            try
            {
                foreach (var item in view.PackingSlipDetailViews)
                {


                    Task<Inventory> inventoryTask = Task.Run(async () => await unitOfWork.inventoryRepository.FindEntityByExpression(e => e.ItemId == item.ItemId && e.PackingSlipDetailId == item.PackingSlipDetailId));
                    Task.WaitAll(inventoryTask);
                    if (inventoryTask.Result == null)
                    {
                        Inventory inventory = new Inventory();

                        applicationViewFactory.MapPackingSlipIntoInventoryEntity(ref inventory, item);

                        AddInventory(inventory);
                        count++;

                    }
                }
                if (count == 0) { processStatus=CreateProcessStatus.AlreadyExists; return this as IFluentInventory; } else { return this as IFluentInventory; }

            }
            catch (Exception ex) { throw new Exception("CreateInventoryByPackingSlipView", ex); }
        }

        public IFluentInventoryQuery Query()
        {
            return new FluentInventoryQuery(unitOfWork) as IFluentInventoryQuery;
        }
        public IFluentInventory Apply()
        {

            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentInventory;
        }
        public IFluentInventory UpdateInventory(Inventory inventory)
        {
            unitOfWork.inventoryRepository.UpdateObject(inventory);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentInventory;
        }
        public IFluentInventory DeleteInventory(Inventory inventory)
        {
            unitOfWork.inventoryRepository.DeleteObject(inventory);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentInventory;

        }
        public IFluentInventory AddInventory(Inventory inventory)
        {
            unitOfWork.inventoryRepository.AddObject(inventory);

            this.processStatus = CreateProcessStatus.Insert;

            return this as IFluentInventory;
        }
    }
}
