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
    public class PayRollTransactionTypesController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionTypesView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollTransactionTypes([FromBody]PayRollTransactionTypesView view)
        {
            PayRollTransactionTypesModule invMod = new PayRollTransactionTypesModule();

            NextNumber nnPayRollTransactionTypes = await invMod.PayRollTransactionTypes.Query().GetNextNumber();

            view.PayRollTransactionTypesNumber = nnPayRollTransactionTypes.NextNumberValue;

            PayRollTransactionTypes payRollTransactionTypes = await invMod.PayRollTransactionTypes.Query().MapToEntity(view);

            invMod.PayRollTransactionTypes.AddPayRollTransactionTypes(payRollTransactionTypes).Apply();

            PayRollTransactionTypesView newView = await invMod.PayRollTransactionTypes.Query().GetViewByNumber(view.PayRollTransactionTypesNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionTypesView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollTransactionTypes([FromBody]PayRollTransactionTypesView view)
        {
            PayRollTransactionTypesModule invMod = new PayRollTransactionTypesModule();
            PayRollTransactionTypes payRollTransactionTypes = await invMod.PayRollTransactionTypes.Query().MapToEntity(view);
            invMod.PayRollTransactionTypes.DeletePayRollTransactionTypes(payRollTransactionTypes).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionTypesView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollTransactionTypes([FromBody]PayRollTransactionTypesView view)
        {
            PayRollTransactionTypesModule invMod = new PayRollTransactionTypesModule();

            PayRollTransactionTypes payRollTransactionTypes = await invMod.PayRollTransactionTypes.Query().MapToEntity(view);


            invMod.PayRollTransactionTypes.UpdatePayRollTransactionTypes(payRollTransactionTypes).Apply();

            PayRollTransactionTypesView retView = await invMod.PayRollTransactionTypes.Query().GetViewById(payRollTransactionTypes.PayRollTransactionTypesId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollTransactionTypesId}")]
        [ProducesResponseType(typeof(PayRollTransactionTypesView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollTransactionTypesView(long payRollTransactionTypesId)
        {
            PayRollTransactionTypesModule invMod = new PayRollTransactionTypesModule();

            PayRollTransactionTypesView view = await invMod.PayRollTransactionTypes.Query().GetViewById(payRollTransactionTypesId);
            return Ok(view);
        }
        }
	}
        