using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AccountReceivableDetailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class AccountReceivableDetailController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAccountReceivableDetail([FromBody]AccountReceivableDetailView view)
        {
            AccountReceivableDetailModule invMod = new AccountReceivableDetailModule();

            NextNumber nnAccountReceivableDetail = await invMod.AccountReceivableDetail.Query().GetNextNumber();

            view.AccountReceivableDetailNumber = nnAccountReceivableDetail.NextNumberValue;

            AccountReceivableDetail accountReceivableDetail = await invMod.AccountReceivableDetail.Query().MapToEntity(view);

            invMod.AccountReceivableDetail.AddAccountReceivableDetail(accountReceivableDetail).Apply();

            AccountReceivableDetailView newView = await invMod.AccountReceivableDetail.Query().GetViewByNumber(view.AccountReceivableDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccountReceivableDetail([FromBody]AccountReceivableDetailView view)
        {
            AccountReceivableDetailModule invMod = new AccountReceivableDetailModule();
            AccountReceivableDetail accountReceivableDetail = await invMod.AccountReceivableDetail.Query().MapToEntity(view);
            invMod.AccountReceivableDetail.DeleteAccountReceivableDetail(accountReceivableDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(AccountReceivableDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccountReceivableDetail([FromBody]AccountReceivableDetailView view)
        {
            AccountReceivableDetailModule invMod = new AccountReceivableDetailModule();

            AccountReceivableDetail accountReceivableDetail = await invMod.AccountReceivableDetail.Query().MapToEntity(view);


            invMod.AccountReceivableDetail.UpdateAccountReceivableDetail(accountReceivableDetail).Apply();

            AccountReceivableDetailView retView = await invMod.AccountReceivableDetail.Query().GetViewById(accountReceivableDetail.AccountReceivableDetailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{AccountReceivableDetailId}")]
        [ProducesResponseType(typeof(AccountReceivableDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAccountReceivableDetailView(long accountReceivableDetailId)
        {
            AccountReceivableDetailModule invMod = new AccountReceivableDetailModule();

            AccountReceivableDetailView view = await invMod.AccountReceivableDetail.Query().GetViewById(accountReceivableDetailId);
            return Ok(view);
        }
        }
	}
        