using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using lssWebApi2.InventoryDomain.Repository;
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
        [ProducesResponseType(typeof(AssetsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAssets(AssetsView view)
        {
            AssetsModule invMod = new AssetsModule();

            NextNumber nnAssets = await invMod.Assets.Query().GetAssetsNextNumber();

            view.AssetNumber = nnAssets.NextNumberValue;

            Assets Assets = await invMod.Assets.Query().MapToAssetsEntity(view);

            invMod.Assets.AddAssets(Assets).Apply();

            AssetsView newView = await invMod.Assets.Query().GetAssetViewByNumber(view.AssetNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AssetsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAssets(AssetsView view)
        {
            AssetsModule invMod = new AssetsModule();
            Assets Assets = await invMod.Assets.Query().MapToAssetsEntity(view);
            invMod.Assets.DeleteAssets(Assets).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AssetsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAssets(AssetsView view)
        {
            AssetsModule invMod = new AssetsModule();

            Assets Assets = await invMod.Assets.Query().MapToAssetsEntity(view);


            invMod.Assets.UpdateAssets(Assets).Apply();

            AssetsView updateView = await invMod.Assets.Query().GetAssetsViewById(Assets.AssetId);

            AssetsView retView = await invMod.Assets.Query().GetAssetsViewById(Assets.AssetId);

            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AssetsId}")]
        [ProducesResponseType(typeof(AssetsView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAssetsView(long AssetsId)
        {
            AssetsModule invMod = new AssetsModule();

            AssetsView view = await invMod.Assets.Query().GetAssetsViewById(AssetsId);
            return Ok(view);
        }
    }
}