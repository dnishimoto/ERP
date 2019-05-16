using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAssets :IFluentAssets
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentAssets() { }
        public IFluentAssetsQuery Query()
        {
            return new FluentAssetsQuery(unitOfWork) as IFluentAssetsQuery;
        }
        public IFluentAssets Apply()
        {

            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAssets;
        }
        public IFluentAssets AddAssets(Assets assets) {
            unitOfWork.assetsRepository.AddObject(assets);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAssets;
        }
        public IFluentAssets UpdateAssets(Assets assets) {
            unitOfWork.assetsRepository.UpdateObject(assets);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAssets;

        }
        public IFluentAssets DeleteAssets(Assets assets) {
            unitOfWork.assetsRepository.DeleteObject(assets);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAssets;
        }
    }
}
