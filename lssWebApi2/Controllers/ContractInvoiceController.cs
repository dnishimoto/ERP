using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ContractInvoiceDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ContractInvoiceController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ContractInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddContractInvoice([FromBody]ContractInvoiceView view)
        {
            ContractInvoiceModule invMod = new ContractInvoiceModule();

            NextNumber nnContractInvoice = await invMod.ContractInvoice.Query().GetNextNumber();

            view.ContractInvoiceNumber = nnContractInvoice.NextNumberValue;

            ContractInvoice contractInvoice = await invMod.ContractInvoice.Query().MapToEntity(view);

            invMod.ContractInvoice.AddContractInvoice(contractInvoice).Apply();

            ContractInvoiceView newView = await invMod.ContractInvoice.Query().GetViewByNumber(view.ContractInvoiceNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ContractInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteContractInvoice([FromBody]ContractInvoiceView view)
        {
            ContractInvoiceModule invMod = new ContractInvoiceModule();
            ContractInvoice contractInvoice = await invMod.ContractInvoice.Query().MapToEntity(view);
            invMod.ContractInvoice.DeleteContractInvoice(contractInvoice).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ContractInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateContractInvoice([FromBody]ContractInvoiceView view)
        {
            ContractInvoiceModule invMod = new ContractInvoiceModule();

            ContractInvoice contractInvoice = await invMod.ContractInvoice.Query().MapToEntity(view);


            invMod.ContractInvoice.UpdateContractInvoice(contractInvoice).Apply();

            ContractInvoiceView retView = await invMod.ContractInvoice.Query().GetViewById(contractInvoice.ContractInvoiceId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ContractInvoiceId}")]
        [ProducesResponseType(typeof(ContractInvoiceView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetContractInvoiceView(long contractInvoiceId)
        {
            ContractInvoiceModule invMod = new ContractInvoiceModule();

            ContractInvoiceView view = await invMod.ContractInvoice.Query().GetViewById(contractInvoiceId);
            return Ok(view);
        }
        }
	}
        