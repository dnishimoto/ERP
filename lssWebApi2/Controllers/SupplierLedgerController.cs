using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.SupplierLedgerDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SupplierLedgerController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSupplierLedger([FromBody]SupplierLedgerView view)
        {
            SupplierLedgerModule invMod = new SupplierLedgerModule();

            NextNumber nnSupplierLedger = await invMod.SupplierLedger.Query().GetNextNumber();

            view.SupplierLedgerNumber = nnSupplierLedger.NextNumberValue;

            SupplierLedger supplierLedger = await invMod.SupplierLedger.Query().MapToEntity(view);

            invMod.SupplierLedger.AddSupplierLedger(supplierLedger).Apply();

            SupplierLedgerView newView = await invMod.SupplierLedger.Query().GetViewByNumber(view.SupplierLedgerNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSupplierLedger([FromBody]SupplierLedgerView view)
        {
            SupplierLedgerModule invMod = new SupplierLedgerModule();
            SupplierLedger supplierLedger = await invMod.SupplierLedger.Query().MapToEntity(view);
            invMod.SupplierLedger.DeleteSupplierLedger(supplierLedger).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierLedgerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSupplierLedger([FromBody]SupplierLedgerView view)
        {
            SupplierLedgerModule invMod = new SupplierLedgerModule();

            SupplierLedger supplierLedger = await invMod.SupplierLedger.Query().MapToEntity(view);


            invMod.SupplierLedger.UpdateSupplierLedger(supplierLedger).Apply();

            SupplierLedgerView retView = await invMod.SupplierLedger.Query().GetViewById(supplierLedger.SupplierLedgerId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{SupplierLedgerId}")]
        [ProducesResponseType(typeof(SupplierLedgerView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSupplierLedgerView(long supplierLedgerId)
        {
            SupplierLedgerModule invMod = new SupplierLedgerModule();

            SupplierLedgerView view = await invMod.SupplierLedger.Query().GetViewById(supplierLedgerId);
            return Ok(view);
        }
        }
	}
        