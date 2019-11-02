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
    public class PayRollCurrentPaySequenceController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollCurrentPaySequenceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollCurrentPaySequence([FromBody]PayRollCurrentPaySequenceView view)
        {
            PayRollCurrentPaySequenceModule invMod = new PayRollCurrentPaySequenceModule();

            NextNumber nnPayRollCurrentPaySequence = await invMod.PayRollCurrentPaySequence.Query().GetNextNumber();

            view.PayRollCurrentPaySequenceNumber = nnPayRollCurrentPaySequence.NextNumberValue;

            PayRollCurrentPaySequence payRollCurrentPaySequence = await invMod.PayRollCurrentPaySequence.Query().MapToEntity(view);

            invMod.PayRollCurrentPaySequence.AddPayRollCurrentPaySequence(payRollCurrentPaySequence).Apply();

            PayRollCurrentPaySequenceView newView = await invMod.PayRollCurrentPaySequence.Query().GetViewByNumber(view.PayRollCurrentPaySequenceNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollCurrentPaySequenceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollCurrentPaySequence([FromBody]PayRollCurrentPaySequenceView view)
        {
            PayRollCurrentPaySequenceModule invMod = new PayRollCurrentPaySequenceModule();
            PayRollCurrentPaySequence payRollCurrentPaySequence = await invMod.PayRollCurrentPaySequence.Query().MapToEntity(view);
            invMod.PayRollCurrentPaySequence.DeletePayRollCurrentPaySequence(payRollCurrentPaySequence).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollCurrentPaySequenceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollCurrentPaySequence([FromBody]PayRollCurrentPaySequenceView view)
        {
            PayRollCurrentPaySequenceModule invMod = new PayRollCurrentPaySequenceModule();

            PayRollCurrentPaySequence payRollCurrentPaySequence = await invMod.PayRollCurrentPaySequence.Query().MapToEntity(view);


            invMod.PayRollCurrentPaySequence.UpdatePayRollCurrentPaySequence(payRollCurrentPaySequence).Apply();

            PayRollCurrentPaySequenceView retView = await invMod.PayRollCurrentPaySequence.Query().GetViewById(payRollCurrentPaySequence.PayRollCurrentPaySequenceId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollCurrentPaySequenceId}")]
        [ProducesResponseType(typeof(PayRollCurrentPaySequenceView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollCurrentPaySequenceView(long payRollCurrentPaySequenceId)
        {
            PayRollCurrentPaySequenceModule invMod = new PayRollCurrentPaySequenceModule();

            PayRollCurrentPaySequenceView view = await invMod.PayRollCurrentPaySequence.Query().GetViewById(payRollCurrentPaySequenceId);
            return Ok(view);
        }
        }
	}
        