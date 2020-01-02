using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ItemMasterDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ItemMasterController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ItemMasterView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddItemMaster([FromBody]ItemMasterView view)
        {
            ItemMasterModule invMod = new ItemMasterModule();

            NextNumber nnItemMaster = await invMod.ItemMaster.Query().GetNextNumber();

            view.ItemMasterNumber = nnItemMaster.NextNumberValue;

            ItemMaster itemMaster = await invMod.ItemMaster.Query().MapToEntity(view);

            invMod.ItemMaster.AddItemMaster(itemMaster).Apply();

            ItemMasterView newView = await invMod.ItemMaster.Query().GetViewByNumber(view.ItemMasterNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ItemMasterView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteItemMaster([FromBody]ItemMasterView view)
        {
            ItemMasterModule invMod = new ItemMasterModule();
            ItemMaster itemMaster = await invMod.ItemMaster.Query().MapToEntity(view);
            invMod.ItemMaster.DeleteItemMaster(itemMaster).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ItemMasterView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateItemMaster([FromBody]ItemMasterView view)
        {
            ItemMasterModule invMod = new ItemMasterModule();

            ItemMaster itemMaster = await invMod.ItemMaster.Query().MapToEntity(view);


            invMod.ItemMaster.UpdateItemMaster(itemMaster).Apply();

            ItemMasterView retView = await invMod.ItemMaster.Query().GetViewById(itemMaster.ItemId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ItemMasterId}")]
        [ProducesResponseType(typeof(ItemMasterView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetItemMasterView(long itemMasterId)
        {
            ItemMasterModule invMod = new ItemMasterModule();

            ItemMasterView view = await invMod.ItemMaster.Query().GetViewById(itemMasterId);
            return Ok(view);
        }
        }
	}
        