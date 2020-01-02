using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseOrderController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PurchaseOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPurchaseOrder([FromBody]PurchaseOrderView view)
        {
            PurchaseOrderModule invMod = new PurchaseOrderModule();

            NextNumber nnPurchaseOrder = await invMod.PurchaseOrder.Query().GetNextNumber();

            view.PurchaseOrderNumber = nnPurchaseOrder.NextNumberValue;

            PurchaseOrder purchaseOrder = await invMod.PurchaseOrder.Query().MapToEntity(view);

            invMod.PurchaseOrder.AddPurchaseOrder(purchaseOrder).Apply();

            PurchaseOrderView newView = await invMod.PurchaseOrder.Query().GetViewByNumber(view.PurchaseOrderNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PurchaseOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePurchaseOrder([FromBody]PurchaseOrderView view)
        {
            PurchaseOrderModule invMod = new PurchaseOrderModule();
            PurchaseOrder purchaseOrder = await invMod.PurchaseOrder.Query().MapToEntity(view);
            invMod.PurchaseOrder.DeletePurchaseOrder(purchaseOrder).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PurchaseOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePurchaseOrder([FromBody]PurchaseOrderView view)
        {
            PurchaseOrderModule invMod = new PurchaseOrderModule();

            PurchaseOrder purchaseOrder = await invMod.PurchaseOrder.Query().MapToEntity(view);


            invMod.PurchaseOrder.UpdatePurchaseOrder(purchaseOrder).Apply();

            PurchaseOrderView retView = await invMod.PurchaseOrder.Query().GetViewById(purchaseOrder.PurchaseOrderId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PurchaseOrderId}")]
        [ProducesResponseType(typeof(PurchaseOrderView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPurchaseOrderView(long purchaseOrderId)
        {
            PurchaseOrderModule invMod = new PurchaseOrderModule();

            PurchaseOrderView view = await invMod.PurchaseOrder.Query().GetViewById(purchaseOrderId);
            return Ok(view);
        }
        }
	}
        