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
    public class PayRollTransactionControlController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionControlView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollTransactionControl([FromBody]PayRollTransactionControlView view)
        {
            PayRollTransactionControlModule invMod = new PayRollTransactionControlModule();

            NextNumber nnPayRollTransactionControl = await invMod.PayRollTransactionControl.Query().GetNextNumber();

            view.PayRollTransactionControlNumber = nnPayRollTransactionControl.NextNumberValue;

            PayRollTransactionControl payRollTransactionControl = await invMod.PayRollTransactionControl.Query().MapToEntity(view);

            invMod.PayRollTransactionControl.AddPayRollTransactionControl(payRollTransactionControl).Apply();

            PayRollTransactionControlView newView = await invMod.PayRollTransactionControl.Query().GetViewByNumber(view.PayRollTransactionControlNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionControlView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollTransactionControl([FromBody]PayRollTransactionControlView view)
        {
            PayRollTransactionControlModule invMod = new PayRollTransactionControlModule();
            PayRollTransactionControl payRollTransactionControl = await invMod.PayRollTransactionControl.Query().MapToEntity(view);
            invMod.PayRollTransactionControl.DeletePayRollTransactionControl(payRollTransactionControl).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionControlView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollTransactionControl([FromBody]PayRollTransactionControlView view)
        {
            PayRollTransactionControlModule invMod = new PayRollTransactionControlModule();

            PayRollTransactionControl payRollTransactionControl = await invMod.PayRollTransactionControl.Query().MapToEntity(view);


            invMod.PayRollTransactionControl.UpdatePayRollTransactionControl(payRollTransactionControl).Apply();

            PayRollTransactionControlView retView = await invMod.PayRollTransactionControl.Query().GetViewById(payRollTransactionControl.PayRollTransactionControlId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollTransactionControlId}")]
        [ProducesResponseType(typeof(PayRollTransactionControlView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollTransactionControlView(long payRollTransactionControlId)
        {
            PayRollTransactionControlModule invMod = new PayRollTransactionControlModule();

            PayRollTransactionControlView view = await invMod.PayRollTransactionControl.Query().GetViewById(payRollTransactionControlId);
            return Ok(view);
        }
        }
	}
        