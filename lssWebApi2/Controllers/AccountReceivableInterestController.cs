using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AccountReceivableInterestDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AccountReceivableInterestController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableInterestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccountReceivableInterest([FromBody]AccountReceivableInterestView view)
        {
            AccountReceivableInterestModule invMod = new AccountReceivableInterestModule();

            NextNumber nnAccountReceivableInterest = await invMod.AccountReceivableInterest.Query().GetNextNumber();

            view.AccountReceivableInterestNumber = nnAccountReceivableInterest.NextNumberValue;

            AccountReceivableInterest accountReceivableInterest = await invMod.AccountReceivableInterest.Query().MapToEntity(view);

            invMod.AccountReceivableInterest.AddAccountReceivableInterest(accountReceivableInterest).Apply();

            AccountReceivableInterestView newView = await invMod.AccountReceivableInterest.Query().GetViewByNumber(view.AccountReceivableInterestNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableInterestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccountReceivableInterest([FromBody]AccountReceivableInterestView view)
        {
            AccountReceivableInterestModule invMod = new AccountReceivableInterestModule();
            AccountReceivableInterest accountReceivableInterest = await invMod.AccountReceivableInterest.Query().MapToEntity(view);
            invMod.AccountReceivableInterest.DeleteAccountReceivableInterest(accountReceivableInterest).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableInterestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccountReceivableInterest([FromBody]AccountReceivableInterestView view)
        {
            AccountReceivableInterestModule invMod = new AccountReceivableInterestModule();

            AccountReceivableInterest accountReceivableInterest = await invMod.AccountReceivableInterest.Query().MapToEntity(view);


            invMod.AccountReceivableInterest.UpdateAccountReceivableInterest(accountReceivableInterest).Apply();

            AccountReceivableInterestView retView = await invMod.AccountReceivableInterest.Query().GetViewById(accountReceivableInterest.AcctRecInterestId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AccountReceivableInterestId}")]
        [ProducesResponseType(typeof(AccountReceivableInterestView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAccountReceivableInterestView(long accountReceivableInterestId)
        {
            AccountReceivableInterestModule invMod = new AccountReceivableInterestModule();

            AccountReceivableInterestView view = await invMod.AccountReceivableInterest.Query().GetViewById(accountReceivableInterestId);
            return Ok(view);
        }
        }
	}
        