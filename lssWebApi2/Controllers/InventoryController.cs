using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.InventoryDomain;
using lssWebApi2.InventoryDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
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