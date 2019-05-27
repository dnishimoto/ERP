using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain.Repository
{
    public interface IAssetRepository
    {
        Task<Assets> GetAssetsByNumber(long assetNumber);
        Task<Assets> GetAssetsById(long assetId);
    }
}
