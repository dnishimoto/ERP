using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.CustomerClaimDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class CustomerClaimController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(CustomerClaimView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCustomerClaim([FromBody]CustomerClaimView view)
        {
            CustomerClaimModule invMod = new CustomerClaimModule();

            NextNumber nnCustomerClaim = await invMod.CustomerClaim.Query().GetNextNumber();

            view.CustomerClaimNumber = nnCustomerClaim.NextNumberValue;

            CustomerClaim customerClaim = await invMod.CustomerClaim.Query().MapToEntity(view);

            invMod.CustomerClaim.AddCustomerClaim(customerClaim).Apply();

            CustomerClaimView newView = await invMod.CustomerClaim.Query().GetViewByNumber(view.CustomerClaimNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(CustomerClaimView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCustomerClaim([FromBody]CustomerClaimView view)
        {
            CustomerClaimModule invMod = new CustomerClaimModule();
            CustomerClaim customerClaim = await invMod.CustomerClaim.Query().MapToEntity(view);
            invMod.CustomerClaim.DeleteCustomerClaim(customerClaim).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(CustomerClaimView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomerClaim([FromBody]CustomerClaimView view)
        {
            CustomerClaimModule invMod = new CustomerClaimModule();

            CustomerClaim customerClaim = await invMod.CustomerClaim.Query().MapToEntity(view);


            invMod.CustomerClaim.UpdateCustomerClaim(customerClaim).Apply();

            CustomerClaimView retView = await invMod.CustomerClaim.Query().GetViewById(customerClaim.ClaimId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{CustomerClaimId}")]
        [ProducesResponseType(typeof(CustomerClaimView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetCustomerClaimView(long customerClaimId)
        {
            CustomerClaimModule invMod = new CustomerClaimModule();

            CustomerClaimView view = await invMod.CustomerClaim.Query().GetViewById(customerClaimId);
            return Ok(view);
        }
        }
	}
        