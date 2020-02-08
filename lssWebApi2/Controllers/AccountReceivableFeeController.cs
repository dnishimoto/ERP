using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AccountReceivableDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AccountReceivableFeeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableFeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccountReceivableFee([FromBody]AccountReceivableFeeView view)
        {
            AccountReceivableFeeModule invMod = new AccountReceivableFeeModule();

            NextNumber nnAccountReceivableFee = await invMod.AccountReceivableFee.Query().GetNextNumber();

            view.AccountReceivableFeeNumber = nnAccountReceivableFee.NextNumberValue;

            AccountReceivableFee accountReceivableFee = await invMod.AccountReceivableFee.Query().MapToEntity(view);

            invMod.AccountReceivableFee.AddAccountReceivableFee(accountReceivableFee).Apply();

            AccountReceivableFeeView newView = await invMod.AccountReceivableFee.Query().GetViewByNumber(view.AccountReceivableFeeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableFeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccountReceivableFee([FromBody]AccountReceivableFeeView view)
        {
            AccountReceivableFeeModule invMod = new AccountReceivableFeeModule();
            AccountReceivableFee accountReceivableFee = await invMod.AccountReceivableFee.Query().MapToEntity(view);
            invMod.AccountReceivableFee.DeleteAccountReceivableFee(accountReceivableFee).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableFeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccountReceivableFee([FromBody]AccountReceivableFeeView view)
        {
            AccountReceivableFeeModule invMod = new AccountReceivableFeeModule();

            AccountReceivableFee accountReceivableFee = await invMod.AccountReceivableFee.Query().MapToEntity(view);


            invMod.AccountReceivableFee.UpdateAccountReceivableFee(accountReceivableFee).Apply();

            AccountReceivableFeeView retView = await invMod.AccountReceivableFee.Query().GetViewById(accountReceivableFee.AccountReceivableId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AccountReceivableFeeId}")]
        [ProducesResponseType(typeof(AccountReceivableFeeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAccountReceivableFeeView(long accountReceivableFeeId)
        {
            AccountReceivableFeeModule invMod = new AccountReceivableFeeModule();

            AccountReceivableFeeView view = await invMod.AccountReceivableFee.Query().GetViewById(accountReceivableFeeId);
            return Ok(view);
        }
        }
	}
        