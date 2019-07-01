using ERP_Core2.AccountPayableDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.InventoryDomain
{
    public class FluentInventory : IFluentInventory
    {
        

        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentInventory()
        {

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
