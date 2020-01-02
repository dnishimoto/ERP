using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IFluentAssetQuery
    {
        Task<NextNumber> GetAssetNextNumber();
        Task<Asset> MapToEntity(AssetView view);
        Task<List<Asset>> MapToEntity(List<AssetView> inputObjects);
        Task<AssetView> MapToView(Asset inputObject);
        Task<Asset> GetAssetByNumber(long assetNumber);
        Task<Asset> GetEntityById(long ? assetId);
        Task<AssetView> GetViewById(long ? assetId);
        Task<AssetView> GetAssetViewByNumber(long assetNumber);
        Task<Udc> GetUdc(string productCode, string keyCode);
    }
}
