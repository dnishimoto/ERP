using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PayRollDomain;
using ERP_Core2.PayRollDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PayRollEarningsController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollEarningsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollEarnings([FromBody]PayRollEarningsView view)
        {
            PayRollEarningsModule invMod = new PayRollEarningsModule();

            NextNumber nnPayRollEarnings = await invMod.PayRollEarnings.Query().GetNextNumber();

            view.PayRollEarningsNumber = nnPayRollEarnings.NextNumberValue;

            PayRollEarnings payRollEarnings = await invMod.PayRollEarnings.Query().MapToEntity(view);

            invMod.PayRollEarnings.AddPayRollEarnings(payRollEarnings).Apply();

            PayRollEarningsView newView = await invMod.PayRollEarnings.Query().GetViewByNumber(view.PayRollEarningsNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollEarningsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollEarnings([FromBody]PayRollEarningsView view)
        {
            PayRollEarningsModule invMod = new PayRollEarningsModule();
            PayRollEarnings payRollEarnings = await invMod.PayRollEarnings.Query().MapToEntity(view);
            invMod.PayRollEarnings.DeletePayRollEarnings(payRollEarnings).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollEarningsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollEarnings([FromBody]PayRollEarningsView view)
        {
            PayRollEarningsModule invMod = new PayRollEarningsModule();

            PayRollEarnings payRollEarnings = await invMod.PayRollEarnings.Query().MapToEntity(view);


            invMod.PayRollEarnings.UpdatePayRollEarnings(payRollEarnings).Apply();

            PayRollEarningsView retView = await invMod.PayRollEarnings.Query().GetViewById(payRollEarnings.PayRollEarningsId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollEarningsId}")]
        [ProducesResponseType(typeof(PayRollEarningsView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollEarningsView(long payRollEarningsId)
        {
            PayRollEarningsModule invMod = new PayRollEarningsModule();

            PayRollEarningsView view = await invMod.PayRollEarnings.Query().GetViewById(payRollEarningsId);
            return Ok(view);
        }
        }
	}
        