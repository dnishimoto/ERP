using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{

    public class AssetView
    {
        public long AssetId { get; set; }
        public long AssetNumber { get; set; }
        public string AssetCode { get; set; }
        public string TagCode { get; set; }
        public string ClassCode { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? AcquiredDate { get; set; }
        public decimal? OriginalCost { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Depreciation { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public int? Quantity { get; set; }
        public long EquipmentStatusXrefId { get; set; }
        public string GenericLocationLevel1 { get; set; }
        public string GenericLocationLevel2 { get; set; }
        public string GenericLocationLevel3 { get; set; }

    }
    public class AssetRepository: Repository<Asset>, IAssetRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public AssetRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<Asset> GetAssetByNumber(long assetNumber)
        {
            return await _dbContext.Asset.Where(m => m.AssetNumber == assetNumber).FirstOrDefaultAsync<Asset>();
        }
        public async Task<Asset> GetEntityById(long ? assetId)
        {
            return await _dbContext.Asset.FindAsync(assetId);
        }
    }
}
