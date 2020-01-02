using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PayRollDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PayRollLedgerController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollLedger([FromBody]PayRollLedgerView view)
        {
            PayRollLedgerModule invMod = new PayRollLedgerModule();

            NextNumber nnPayRollLedger = await invMod.PayRollLedger.Query().GetNextNumber();

            view.PayRollLedgerNumber = nnPayRollLedger.NextNumberValue;

            PayRollLedger payRollLedger = await invMod.PayRollLedger.Query().MapToEntity(view);

            invMod.PayRollLedger.AddPayRollLedger(payRollLedger).Apply();

            PayRollLedgerView newView = await invMod.PayRollLedger.Query().GetViewByNumber(view.PayRollLedgerNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollLedger([FromBody]PayRollLedgerView view)
        {
            PayRollLedgerModule invMod = new PayRollLedgerModule();
            PayRollLedger payRollLedger = await invMod.PayRollLedger.Query().MapToEntity(view);
            invMod.PayRollLedger.DeletePayRollLedger(payRollLedger).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollLedger([FromBody]PayRollLedgerView view)
        {
            PayRollLedgerModule invMod = new PayRollLedgerModule();

            PayRollLedger payRollLedger = await invMod.PayRollLedger.Query().MapToEntity(view);


            invMod.PayRollLedger.UpdatePayRollLedger(payRollLedger).Apply();

            PayRollLedgerView retView = await invMod.PayRollLedger.Query().GetViewById(payRollLedger.PayRollLedgerId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollLedgerId}")]
        [ProducesResponseType(typeof(PayRollLedgerView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollLedgerView(long payRollLedgerId)
        {
            PayRollLedgerModule invMod = new PayRollLedgerModule();

            PayRollLedgerView view = await invMod.PayRollLedger.Query().GetViewById(payRollLedgerId);
            return Ok(view);
        }
        }
	}
        