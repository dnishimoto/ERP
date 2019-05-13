using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentAssetsQuery
    {
        Task<NextNumber> GetAssetsNextNumber();
        Task<Assets> MapToAssetsEntity(AssetsView view);
        Task<Assets> GetAssetsByNumber(long assetNumber);
        Task<Assets> GetAssetsById(long assetId);
        Task<AssetsView> GetAssetsViewById(long assetId);
        Task<AssetsView> GetAssetViewByNumber(long assetNumber);
        Task<Udc> GetUdc(string productCode, string keyCode);
    }
}
