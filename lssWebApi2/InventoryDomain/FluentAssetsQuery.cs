using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;
using lssWebApi2.InventoryDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAssetsQuery : IFluentAssetsQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAssetsQuery() { }
        public FluentAssetsQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<NextNumber> GetAssetsNextNumber()
        {
            return await _unitOfWork.assetsRepository.GetNextNumber(TypeOfNextNumberEnum.AssetsNumber.ToString());
        }
        public async Task<Assets> MapToAssetsEntity(AssetsView inputObject)
        {
            Mapper mapper = new Mapper();
            Assets assets = mapper.Map<Assets>(inputObject);
            await Task.Yield();
            return assets;
        }
        public async Task<Assets> GetAssetsByNumber(long assetNumber) {
            return await _unitOfWork.assetsRepository.GetAssetsByNumber(assetNumber);
        }
        public async Task<Assets> GetAssetsById(long assetId)
        {
            return await _unitOfWork.assetsRepository.GetAssetsById(assetId);
        }
        public async Task<AssetsView> GetAssetsViewById(long assetId)
        {
            Assets asset=await _unitOfWork.assetsRepository.GetAssetsById(assetId);

            Mapper mapper = new Mapper();
            AssetsView view = mapper.Map<AssetsView>(asset);
            await Task.Yield();
            return view;
        }
        public async Task<AssetsView> GetAssetViewByNumber(long assetNumber)
        {
            Assets assets= await _unitOfWork.assetsRepository.GetAssetsByNumber(assetNumber);
            Mapper mapper = new Mapper();
            AssetsView view = mapper.Map<AssetsView>(assets);
            await Task.Yield();
            return view;
        }
        public async Task<Udc> GetUdc(string productCode, string keyCode)
        {
            return await _unitOfWork.assetsRepository.GetUdc(productCode, keyCode);
        }
    }
}
