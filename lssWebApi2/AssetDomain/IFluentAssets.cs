using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IFluentAsset
    {
        IFluentAssetQuery Query();
        IFluentAsset Apply();
        IFluentAsset AddAsset(Asset asset);
        IFluentAsset UpdateAsset(Asset asset);
        IFluentAsset DeleteAsset(Asset asset);
    }
}
