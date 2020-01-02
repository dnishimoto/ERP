using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.SupplierInvoiceDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SupplierInvoiceController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSupplierInvoice([FromBody]SupplierInvoiceView view)
        {
            SupplierInvoiceModule invMod = new SupplierInvoiceModule();

            NextNumber nnSupplierInvoice = await invMod.SupplierInvoice.Query().GetNextNumber();

            view.SupplierInvoiceNumber = nnSupplierInvoice.NextNumberValue;

            SupplierInvoice supplierInvoice = await invMod.SupplierInvoice.Query().MapToEntity(view);

            invMod.SupplierInvoice.AddSupplierInvoice(supplierInvoice).Apply();

            SupplierInvoiceView newView = await invMod.SupplierInvoice.Query().GetViewByNumber(view.SupplierInvoiceNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSupplierInvoice([FromBody]SupplierInvoiceView view)
        {
            SupplierInvoiceModule invMod = new SupplierInvoiceModule();
            SupplierInvoice supplierInvoice = await invMod.SupplierInvoice.Query().MapToEntity(view);
            invMod.SupplierInvoice.DeleteSupplierInvoice(supplierInvoice).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSupplierInvoice([FromBody]SupplierInvoiceView view)
        {
            SupplierInvoiceModule invMod = new SupplierInvoiceModule();

            SupplierInvoice supplierInvoice = await invMod.SupplierInvoice.Query().MapToEntity(view);


            invMod.SupplierInvoice.UpdateSupplierInvoice(supplierInvoice).Apply();

            SupplierInvoiceView retView = await invMod.SupplierInvoice.Query().GetViewById(supplierInvoice.SupplierInvoiceId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{SupplierInvoiceId}")]
        [ProducesResponseType(typeof(SupplierInvoiceView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSupplierInvoiceView(long supplierInvoiceId)
        {
            SupplierInvoiceModule invMod = new SupplierInvoiceModule();

            SupplierInvoiceView view = await invMod.SupplierInvoice.Query().GetViewById(supplierInvoiceId);
            return Ok(view);
        }
        }
	}
        