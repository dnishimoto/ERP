using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AccountPayableDetailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AccountPayableDetailController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(AccountPayableDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccountPayableDetail([FromBody]AccountPayableDetailView view)
        {
            AccountPayableDetailModule invMod = new AccountPayableDetailModule();

            NextNumber nnAccountPayableDetail = await invMod.AccountPayableDetail.Query().GetNextNumber();

            view.AccountPayableDetailNumber = nnAccountPayableDetail.NextNumberValue;

            AccountPayableDetail accountPyableDetail = await invMod.AccountPayableDetail.Query().MapToEntity(view);

            invMod.AccountPayableDetail.AddAccountPayableDetail(accountPyableDetail).Apply();

            AccountPayableDetailView newView = await invMod.AccountPayableDetail.Query().GetViewByNumber(view.AccountPayableDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AccountPayableDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccountPayableDetail([FromBody]AccountPayableDetailView view)
        {
            AccountPayableDetailModule invMod = new AccountPayableDetailModule();
            AccountPayableDetail accountPyableDetail = await invMod.AccountPayableDetail.Query().MapToEntity(view);
            invMod.AccountPayableDetail.DeleteAccountPayableDetail(accountPyableDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AccountPayableDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccountPayableDetail([FromBody]AccountPayableDetailView view)
        {
            AccountPayableDetailModule invMod = new AccountPayableDetailModule();

            AccountPayableDetail accountPyableDetail = await invMod.AccountPayableDetail.Query().MapToEntity(view);


            invMod.AccountPayableDetail.UpdateAccountPayableDetail(accountPyableDetail).Apply();

            AccountPayableDetailView retView = await invMod.AccountPayableDetail.Query().GetViewById(accountPyableDetail.AccountPayableDetailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AccountPayableDetailId}")]
        [ProducesResponseType(typeof(AccountPayableDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAccountPayableDetailView(long accountPyableDetailId)
        {
            AccountPayableDetailModule invMod = new AccountPayableDetailModule();

            AccountPayableDetailView view = await invMod.AccountPayableDetail.Query().GetViewById(accountPyableDetailId);
            return Ok(view);
        }
        }
	}
        