
using ERP_Core2.AddressBookDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using lssWebApi2.InventoryDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.AssetsDomain
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
           AssetsModule assetsMod = new AssetsModule();


            Udc equipmentStatus = await assetsMod.Assets.Query().GetUdc("ASSETS_STATUS","InUse");


            AssetsView view = new AssetsView()
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
            NextNumber nnAssets = await assetsMod.Assets.Query().GetAssetsNextNumber();

            view.AssetNumber = nnAssets.NextNumberValue;

            Assets Assets = await assetsMod.Assets.Query().MapToAssetsEntity(view);

            assetsMod.Assets.AddAssets(Assets).Apply();

            Assets newAssets = await assetsMod.Assets.Query().GetAssetsByNumber(view.AssetNumber);

            Assert.NotNull(newAssets);

            newAssets.Description = "Testing Assets update";

            assetsMod.Assets.UpdateAssets(newAssets).Apply();

            AssetsView updateView = await assetsMod.Assets.Query().GetAssetsViewById(newAssets.AssetId);

            Assert.Same(updateView.Description, "Testing Assets update");

            assetsMod.Assets.DeleteAssets(newAssets).Apply();
            Assets lookupAssets = await assetsMod.Assets.Query().GetAssetsById(view.AssetId);

            Assert.Null(lookupAssets);
        }
        [Fact]
        public async Task TestAssetsView()
        {
            AssetsModule invMod = new AssetsModule();

            long AssetsId = 21;
            AssetsView view = await invMod.Assets.Query().GetAssetsViewById(AssetsId);

            Assert.NotNull(view);

        }
      

    }
}
