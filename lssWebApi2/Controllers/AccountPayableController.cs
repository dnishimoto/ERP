using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AccountPayableDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AccountPayableController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(AccountPayableView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccountPayable([FromBody]AccountPayableView view)
        {
            AccountPayableModule invMod = new AccountPayableModule();

            NextNumber nnAccountPayable = await invMod.AccountPayable.Query().GetNextNumber();

            view.AccountPayableNumber = nnAccountPayable.NextNumberValue;

            AccountPayable accountPayable = await invMod.AccountPayable.Query().MapToEntity(view);

            invMod.AccountPayable.AddAccountPayable(accountPayable).Apply();

            AccountPayableView newView = await invMod.AccountPayable.Query().GetViewByNumber(view.AccountPayableNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AccountPayableView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccountPayable([FromBody]AccountPayableView view)
        {
            AccountPayableModule invMod = new AccountPayableModule();
            AccountPayable accountPayable = await invMod.AccountPayable.Query().MapToEntity(view);
            invMod.AccountPayable.DeleteAccountPayable(accountPayable).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AccountPayableView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccountPayable([FromBody]AccountPayableView view)
        {
            AccountPayableModule invMod = new AccountPayableModule();

            AccountPayable accountPayable = await invMod.AccountPayable.Query().MapToEntity(view);


            invMod.AccountPayable.UpdateAccountPayable(accountPayable).Apply();

            AccountPayableView retView = await invMod.AccountPayable.Query().GetViewById(accountPayable.AcctPayId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AccountPayableId}")]
        [ProducesResponseType(typeof(AccountPayableView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAccountPayableView(long accountPayableId)
        {
            AccountPayableModule invMod = new AccountPayableModule();

            AccountPayableView view = await invMod.AccountPayable.Query().GetViewById(accountPayableId);
            return Ok(view);
        }
        }
	}
        