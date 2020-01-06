using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.Enumerations;
using System.Threading.Tasks;

namespace lssWebApi2.ItemMasterDomain
{

public class FluentItemMaster :IFluentItemMaster
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentItemMaster() { }
        public IFluentItemMasterQuery Query()
        {
            return new FluentItemMasterQuery(unitOfWork) as IFluentItemMasterQuery;
        }
        public IFluentItemMaster Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentItemMaster;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentItemMaster CreateItemMaster(ItemMaster itemMaster)
        {
            try
            {
                Task<ItemMaster> resultTask =  Query().GetEntityByItemCode(itemMaster.Branch,itemMaster.ItemCode);
                Task.WaitAll(resultTask);
             
                if (resultTask.Result == null)
                {
                    AddItemMaster(itemMaster);
                    return this as IFluentItemMaster;
                }
                processStatus = CreateProcessStatus.AlreadyExists;
                return this as IFluentItemMaster;
            }
            catch (Exception ex) { throw new Exception("CreateItemMaster", ex); }
        }
        public IFluentItemMaster AddItemMasters(List<ItemMaster> newObjects)
        {
            unitOfWork.itemMasterRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentItemMaster;
        }
        public IFluentItemMaster UpdateItemMasters(IList<ItemMaster> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.itemMasterRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentItemMaster;
        }
        public IFluentItemMaster AddItemMaster(ItemMaster newObject) {
            unitOfWork.itemMasterRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentItemMaster;
        }
        public IFluentItemMaster UpdateItemMaster(ItemMaster updateObject) {
            unitOfWork.itemMasterRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentItemMaster;

        }
        public IFluentItemMaster DeleteItemMaster(ItemMaster deleteObject) {
            unitOfWork.itemMasterRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentItemMaster;
        }
   	public IFluentItemMaster DeleteItemMasters(List<ItemMaster> deleteObjects)
        {
            unitOfWork.itemMasterRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentItemMaster;
        }
    }
}
