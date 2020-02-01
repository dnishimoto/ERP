using lssWebApi2.InventoryDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;

namespace lssWebApi2.InventoryDomain
{
    public class AssetModule
    {
        private UnitOfWork unitOfWork;
        public FluentAsset Asset;
        public AssetModule()
        {
            unitOfWork = new UnitOfWork();
            Asset = new FluentAsset(unitOfWork);
        }
    }
}
