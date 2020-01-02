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
    public class PayRollPaySequenceController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollPaySequenceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollPaySequence([FromBody]PayRollPaySequenceView view)
        {
            PayRollPaySequenceModule invMod = new PayRollPaySequenceModule();

            NextNumber nnPayRollPaySequence = await invMod.PayRollPaySequence.Query().GetNextNumber();

            view.PayRollPaySequenceNumber = nnPayRollPaySequence.NextNumberValue;

            PayRollPaySequence payRollPaySequence = await invMod.PayRollPaySequence.Query().MapToEntity(view);

            invMod.PayRollPaySequence.AddPayRollPaySequence(payRollPaySequence).Apply();

            PayRollPaySequenceView newView = await invMod.PayRollPaySequence.Query().GetViewByNumber(view.PayRollPaySequenceNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollPaySequenceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollPaySequence([FromBody]PayRollPaySequenceView view)
        {
            PayRollPaySequenceModule invMod = new PayRollPaySequenceModule();
            PayRollPaySequence payRollPaySequence = await invMod.PayRollPaySequence.Query().MapToEntity(view);
            invMod.PayRollPaySequence.DeletePayRollPaySequence(payRollPaySequence).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollPaySequenceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollPaySequence([FromBody]PayRollPaySequenceView view)
        {
            PayRollPaySequenceModule invMod = new PayRollPaySequenceModule();

            PayRollPaySequence payRollPaySequence = await invMod.PayRollPaySequence.Query().MapToEntity(view);


            invMod.PayRollPaySequence.UpdatePayRollPaySequence(payRollPaySequence).Apply();

            PayRollPaySequenceView retView = await invMod.PayRollPaySequence.Query().GetViewById(payRollPaySequence.PayRollPaySequenceId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollPaySequenceId}")]
        [ProducesResponseType(typeof(PayRollPaySequenceView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollPaySequenceView(long payRollPaySequenceId)
        {
            PayRollPaySequenceModule invMod = new PayRollPaySequenceModule();

            PayRollPaySequenceView view = await invMod.PayRollPaySequence.Query().GetViewById(payRollPaySequenceId);
            return Ok(view);
        }
        }
	}
        