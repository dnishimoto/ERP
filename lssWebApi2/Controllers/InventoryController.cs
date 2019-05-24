using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.InventoryDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(InventoryView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddInventory([FromBody]InventoryView view)
        {
            InventoryModule invMod = new InventoryModule();
            //InventoryView view = new InventoryView()
            //{
            //    ItemId = 9,
            //    Description = "Highlighter - 3 Color",
            //    Remarks = "Testing inventory add",
            //    UnitOfMeasure = "Sets",
            //    Quantity = 10,
            //    ExtendedPrice = 100,
            //    DistributionAccountId = 5,
            //    Branch = "700"
            //};
            NextNumber nnInventory = await invMod.Inventory.Query().GetInventoryNextNumber();

            view.InventoryNumber = nnInventory.NextNumberValue;

            Inventory inventory = await invMod.Inventory.Query().MapToInventoryEntity(view);

            invMod.Inventory.AddInventory(inventory).Apply();

            InventoryView newView = await invMod.Inventory.Query().GetInventoryViewByNumber(view.InventoryNumber);

           
            return Ok(newView);

        }
        
        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(InventoryView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteInventory([FromBody]InventoryView view)
        {
            InventoryModule invMod = new InventoryModule();
            Inventory inventory = await invMod.Inventory.Query().MapToInventoryEntity(view);
            invMod.Inventory.DeleteInventory(inventory).Apply();
           
             return Ok(view);
        }
     
        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(InventoryView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInventory([FromBody]InventoryView view)
        {
            InventoryModule invMod = new InventoryModule();
            Inventory inventory = await invMod.Inventory.Query().MapToInventoryEntity(view);
            invMod.Inventory.UpdateInventory(inventory).Apply();
            InventoryView updateView = await invMod.Inventory.Query().GetInventoryViewbyId(inventory.InventoryId);
            return Ok(updateView);
        }
    
        [HttpGet]
        [Route("View/{inventoryId}")]
        [ProducesResponseType(typeof(InventoryView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetInventoryView(long inventoryId)
        {
            InventoryModule invMod = new InventoryModule();
            InventoryView view = await invMod.Inventory.Query().GetInventoryViewbyId(inventoryId);
            return Ok(view);
        }
    }
}