using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(AssetView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAssets([FromBody]AssetView view)
        {
            AssetModule invMod = new AssetModule();

            NextNumber nnAssets = await invMod.Asset.Query().GetAssetNextNumber();

            view.AssetNumber = nnAssets.NextNumberValue;

            Asset Assets = await invMod.Asset.Query().MapToEntity(view);

            invMod.Asset.AddAsset(Assets).Apply();

            AssetView newView = await invMod.Asset.Query().GetAssetViewByNumber(view.AssetNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AssetView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAssets([FromBody]AssetView view)
        {
            AssetModule invMod = new AssetModule();
            Asset Assets = await invMod.Asset.Query().MapToEntity(view);
            invMod.Asset.DeleteAsset(Assets).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AssetView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAssets([FromBody]AssetView view)
        {
            AssetModule invMod = new AssetModule();

            Asset Assets = await invMod.Asset.Query().MapToEntity(view);


            invMod.Asset.UpdateAsset(Assets).Apply();

            AssetView updateView = await invMod.Asset.Query().GetViewById(Assets.AssetId);

            AssetView retView = await invMod.Asset.Query().GetViewById(Assets.AssetId);

            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AssetsId}")]
        [ProducesResponseType(typeof(AssetView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAssetsView(long AssetsId)
        {
            AssetModule invMod = new AssetModule();

            AssetView view = await invMod.Asset.Query().GetViewById(AssetsId);
            return Ok(view);
        }
    }
}