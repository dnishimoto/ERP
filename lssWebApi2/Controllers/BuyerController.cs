using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.BuyerDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class BuyerController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(BuyerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBuyer([FromBody]BuyerView view)
        {
            BuyerModule invMod = new BuyerModule();

            NextNumber nnBuyer = await invMod.Buyer.Query().GetNextNumber();

            view.BuyerNumber = nnBuyer.NextNumberValue;

            Buyer buyer = await invMod.Buyer.Query().MapToEntity(view);

            invMod.Buyer.AddBuyer(buyer).Apply();

            BuyerView newView = await invMod.Buyer.Query().GetViewByNumber(view.BuyerNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(BuyerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBuyer([FromBody]BuyerView view)
        {
            BuyerModule invMod = new BuyerModule();
            Buyer buyer = await invMod.Buyer.Query().MapToEntity(view);
            invMod.Buyer.DeleteBuyer(buyer).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(BuyerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBuyer([FromBody]BuyerView view)
        {
            BuyerModule invMod = new BuyerModule();

            Buyer buyer = await invMod.Buyer.Query().MapToEntity(view);


            invMod.Buyer.UpdateBuyer(buyer).Apply();

            BuyerView retView = await invMod.Buyer.Query().GetViewById(buyer.BuyerId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{BuyerId}")]
        [ProducesResponseType(typeof(BuyerView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetBuyerView(long buyerId)
        {
            BuyerModule invMod = new BuyerModule();

            BuyerView view = await invMod.Buyer.Query().GetViewById(buyerId);
            return Ok(view);
        }
        }
	}
        