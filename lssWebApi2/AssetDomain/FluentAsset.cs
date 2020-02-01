using lssWebApi2.AccountPayableDomain;
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
    public class FluentAsset :IFluentAsset
    {
        private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentAsset(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentAssetQuery Query()
        {
            return new FluentAssetQuery(unitOfWork) as IFluentAssetQuery;
        }
        public IFluentAsset Apply()
        {

            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAsset;
        }
        public IFluentAsset AddAsset(Asset asset) {
            unitOfWork.assetRepository.AddObject(asset);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAsset;
        }
        public IFluentAsset UpdateAsset(Asset asset) {
            unitOfWork.assetRepository.UpdateObject(asset);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAsset;

        }
        public IFluentAsset DeleteAsset(Asset asset) {
            unitOfWork.assetRepository.DeleteObject(asset);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAsset;
        }
    }
}
