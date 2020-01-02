
using lssWebApi2.InventoryDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.AssetsDomain
{

    public class UnitTestAssets
    {

        private readonly ITestOutputHelper output;

        public UnitTestAssets(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDeleteAssets()
        {
           AssetModule assetsMod = new AssetModule();


            Udc equipmentStatus = await assetsMod.Asset.Query().GetUdc("ASSETS_STATUS","InUse");


            AssetView view = new AssetView()
            {

                AssetCode = "12345",
                TagCode = "Amplifier",
                ClassCode = "SOUND",
                Description = "PA SYSTEM",
                Manufacturer = "LIGHTSPEED",
                Model = "CAT 855",
                SerialNumber = "12X34X56",
                AcquiredDate = DateTime.Parse("5/11/2019"),
                OriginalCost = 855.57M,
                ReplacementCost = 855.57M,
                Depreciation = 0,
                Location = "Build 1",
                SubLocation = "Room 1",
                Quantity = 1,
                EquipmentStatusXrefId = equipmentStatus.XrefId,
                GenericLocationLevel1 ="",
                GenericLocationLevel2 ="",
                GenericLocationLevel3 =""
    };
            NextNumber nnAssets = await assetsMod.Asset.Query().GetAssetNextNumber();

            view.AssetNumber = nnAssets.NextNumberValue;

            Asset Asset = await assetsMod.Asset.Query().MapToEntity(view);

            assetsMod.Asset.AddAsset(Asset).Apply();

            Asset newAssets = await assetsMod.Asset.Query().GetAssetByNumber(view.AssetNumber);

            Assert.NotNull(newAssets);

            newAssets.Description = "Testing Assets update";

            assetsMod.Asset.UpdateAsset(newAssets).Apply();

            AssetView updateView = await assetsMod.Asset.Query().GetViewById(newAssets.AssetId);

            Assert.Same(updateView.Description, "Testing Assets update");

            assetsMod.Asset.DeleteAsset(newAssets).Apply();
            Asset lookupAssets = await assetsMod.Asset.Query().GetEntityById(view.AssetId);

            Assert.Null(lookupAssets);
        }
            

    }
}
