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
    public class PayRollTotalsController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTotalsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollTotals([FromBody]PayRollTotalsView view)
        {
            PayRollTotalsModule invMod = new PayRollTotalsModule();

            NextNumber nnPayRollTotals = await invMod.PayRollTotals.Query().GetNextNumber();

            view.PayRollTotalsNumber = nnPayRollTotals.NextNumberValue;

            PayRollTotals payRollTotals = await invMod.PayRollTotals.Query().MapToEntity(view);

            invMod.PayRollTotals.AddPayRollTotals(payRollTotals).Apply();

            PayRollTotalsView newView = await invMod.PayRollTotals.Query().GetViewByNumber(view.PayRollTotalsNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTotalsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollTotals([FromBody]PayRollTotalsView view)
        {
            PayRollTotalsModule invMod = new PayRollTotalsModule();
            PayRollTotals payRollTotals = await invMod.PayRollTotals.Query().MapToEntity(view);
            invMod.PayRollTotals.DeletePayRollTotals(payRollTotals).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTotalsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollTotals([FromBody]PayRollTotalsView view)
        {
            PayRollTotalsModule invMod = new PayRollTotalsModule();

            PayRollTotals payRollTotals = await invMod.PayRollTotals.Query().MapToEntity(view);


            invMod.PayRollTotals.UpdatePayRollTotals(payRollTotals).Apply();

            PayRollTotalsView retView = await invMod.PayRollTotals.Query().GetViewById(payRollTotals.PayRollTotalsId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollTotalsId}")]
        [ProducesResponseType(typeof(PayRollTotalsView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollTotalsView(long payRollTotalsId)
        {
            PayRollTotalsModule invMod = new PayRollTotalsModule();

            PayRollTotalsView view = await invMod.PayRollTotals.Query().GetViewById(payRollTotalsId);
            return Ok(view);
        }
        }
	}
        