using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.InventoryDomain
{
    public interface IFluentAssets
    {
        IFluentAssetsQuery Query();
        IFluentAssets Apply();
        IFluentAssets AddAssets(Assets assets);
        IFluentAssets UpdateAssets(Assets assets);
        IFluentAssets DeleteAssets(Assets assets);
    }
}
