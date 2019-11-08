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
    public class PayRollDeductionLiabilitiesController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollDeductionLiabilitiesView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollDeductionLiabilities([FromBody]PayRollDeductionLiabilitiesView view)
        {
            PayRollDeductionLiabilitiesModule invMod = new PayRollDeductionLiabilitiesModule();

            NextNumber nnPayRollDeductionLiabilities = await invMod.PayRollDeductionLiabilities.Query().GetNextNumber();

            view.PayRollDeductionLiabilitiesNumber = nnPayRollDeductionLiabilities.NextNumberValue;

            PayRollDeductionLiabilities payRollDeductionLiabilities = await invMod.PayRollDeductionLiabilities.Query().MapToEntity(view);

            invMod.PayRollDeductionLiabilities.AddPayRollDeductionLiabilities(payRollDeductionLiabilities).Apply();

            PayRollDeductionLiabilitiesView newView = await invMod.PayRollDeductionLiabilities.Query().GetViewByNumber(view.PayRollDeductionLiabilitiesNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollDeductionLiabilitiesView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollDeductionLiabilities([FromBody]PayRollDeductionLiabilitiesView view)
        {
            PayRollDeductionLiabilitiesModule invMod = new PayRollDeductionLiabilitiesModule();
            PayRollDeductionLiabilities payRollDeductionLiabilities = await invMod.PayRollDeductionLiabilities.Query().MapToEntity(view);
            invMod.PayRollDeductionLiabilities.DeletePayRollDeductionLiabilities(payRollDeductionLiabilities).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollDeductionLiabilitiesView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollDeductionLiabilities([FromBody]PayRollDeductionLiabilitiesView view)
        {
            PayRollDeductionLiabilitiesModule invMod = new PayRollDeductionLiabilitiesModule();

            PayRollDeductionLiabilities payRollDeductionLiabilities = await invMod.PayRollDeductionLiabilities.Query().MapToEntity(view);


            invMod.PayRollDeductionLiabilities.UpdatePayRollDeductionLiabilities(payRollDeductionLiabilities).Apply();

            PayRollDeductionLiabilitiesView retView = await invMod.PayRollDeductionLiabilities.Query().GetViewById(payRollDeductionLiabilities.PayRollDeductionLiabilitiesId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollDeductionLiabilitiesId}")]
        [ProducesResponseType(typeof(PayRollDeductionLiabilitiesView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollDeductionLiabilitiesView(long payRollDeductionLiabilitiesId)
        {
            PayRollDeductionLiabilitiesModule invMod = new PayRollDeductionLiabilitiesModule();

            PayRollDeductionLiabilitiesView view = await invMod.PayRollDeductionLiabilities.Query().GetViewById(payRollDeductionLiabilitiesId);
            return Ok(view);
        }
        }
	}
        