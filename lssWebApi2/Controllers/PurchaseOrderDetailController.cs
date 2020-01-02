using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PurchaseOrderDetailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseOrderDetailController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PurchaseOrderDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPurchaseOrderDetail([FromBody]PurchaseOrderDetailView view)
        {
            PurchaseOrderDetailModule invMod = new PurchaseOrderDetailModule();

            NextNumber nnPurchaseOrderDetail = await invMod.PurchaseOrderDetail.Query().GetNextNumber();

            view.PurchaseOrderDetailNumber = nnPurchaseOrderDetail.NextNumberValue;

            PurchaseOrderDetail purchaseOrderDetail = await invMod.PurchaseOrderDetail.Query().MapToEntity(view);

            invMod.PurchaseOrderDetail.AddPurchaseOrderDetail(purchaseOrderDetail).Apply();

            PurchaseOrderDetailView newView = await invMod.PurchaseOrderDetail.Query().GetViewByNumber(view.PurchaseOrderDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PurchaseOrderDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePurchaseOrderDetail([FromBody]PurchaseOrderDetailView view)
        {
            PurchaseOrderDetailModule invMod = new PurchaseOrderDetailModule();
            PurchaseOrderDetail purchaseOrderDetail = await invMod.PurchaseOrderDetail.Query().MapToEntity(view);
            invMod.PurchaseOrderDetail.DeletePurchaseOrderDetail(purchaseOrderDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PurchaseOrderDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePurchaseOrderDetail([FromBody]PurchaseOrderDetailView view)
        {
            PurchaseOrderDetailModule invMod = new PurchaseOrderDetailModule();

            PurchaseOrderDetail purchaseOrderDetail = await invMod.PurchaseOrderDetail.Query().MapToEntity(view);


            invMod.PurchaseOrderDetail.UpdatePurchaseOrderDetail(purchaseOrderDetail).Apply();

            PurchaseOrderDetailView retView = await invMod.PurchaseOrderDetail.Query().GetViewById(purchaseOrderDetail.PurchaseOrderDetailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PurchaseOrderDetailId}")]
        [ProducesResponseType(typeof(PurchaseOrderDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPurchaseOrderDetailView(long purchaseOrderDetailId)
        {
            PurchaseOrderDetailModule invMod = new PurchaseOrderDetailModule();

            PurchaseOrderDetailView view = await invMod.PurchaseOrderDetail.Query().GetViewById(purchaseOrderDetailId);
            return Ok(view);
        }
        }
	}
        