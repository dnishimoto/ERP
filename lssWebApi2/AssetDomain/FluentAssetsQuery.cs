using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.InventoryDomain;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class FluentAssetQuery : MapperAbstract<Asset, AssetView> , IFluentAssetQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAssetQuery() { }
        public FluentAssetQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<AssetView> MapToView(Asset inputObject)
        {

            AssetView outObject = mapper.Map<AssetView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<NextNumber> GetAssetNextNumber()
        {
            return await _unitOfWork.assetRepository.GetNextNumber(TypeOfAsset.AssetNumber.ToString());
        }
        public override async Task<Asset> MapToEntity(AssetView inputObject)
        {
            Asset assets = mapper.Map<Asset>(inputObject);
            await Task.Yield();
            return assets;
        }
        public override async Task<IList<Asset>> MapToEntity(IList<AssetView> inputObjects)
        {
            IList<Asset> list = new List<Asset>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                Asset outObject = mapper.Map<Asset>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public override async Task<AssetView> GetViewById(long ? assetId)
        {
            Asset asset = await _unitOfWork.assetRepository.GetEntityById(assetId);

            AssetView view = mapper.Map<AssetView>(asset);
            await Task.Yield();
            return view;
        }
        public async Task<Asset> GetAssetByNumber(long assetNumber) {
            return await _unitOfWork.assetRepository.GetAssetByNumber(assetNumber);
        }
        public override async Task<Asset> GetEntityById(long ? assetId)
        {
            return await _unitOfWork.assetRepository.GetEntityById(assetId);
        }

        public async Task<AssetView> GetAssetViewByNumber(long assetNumber)
        {
            Asset assets= await _unitOfWork.assetRepository.GetAssetByNumber(assetNumber);
            Mapper mapper = new Mapper();
            AssetView view = mapper.Map<AssetView>(assets);
            await Task.Yield();
            return view;
        }
        public async Task<Udc> GetUdc(string productCode, string keyCode)
        {
            return await _unitOfWork.assetRepository.GetUdc(productCode, keyCode);
        }
    }
}
