using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.InvoiceDetailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceDetailController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(InvoiceDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddInvoiceDetail([FromBody]InvoiceDetailView view)
        {
            InvoiceDetailModule invMod = new InvoiceDetailModule();

            NextNumber nnInvoiceDetail = await invMod.InvoiceDetail.Query().GetNextNumber();

            view.InvoiceDetailNumber = nnInvoiceDetail.NextNumberValue;

            InvoiceDetail invoiceDetail = await invMod.InvoiceDetail.Query().MapToEntity(view);

            invMod.InvoiceDetail.AddInvoiceDetail(invoiceDetail).Apply();

            InvoiceDetailView newView = await invMod.InvoiceDetail.Query().GetViewByNumber(view.InvoiceDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(InvoiceDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteInvoiceDetail([FromBody]InvoiceDetailView view)
        {
            InvoiceDetailModule invMod = new InvoiceDetailModule();
            InvoiceDetail invoiceDetail = await invMod.InvoiceDetail.Query().MapToEntity(view);
            invMod.InvoiceDetail.DeleteInvoiceDetail(invoiceDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(InvoiceDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInvoiceDetail([FromBody]InvoiceDetailView view)
        {
            InvoiceDetailModule invMod = new InvoiceDetailModule();

            InvoiceDetail invoiceDetail = await invMod.InvoiceDetail.Query().MapToEntity(view);


            invMod.InvoiceDetail.UpdateInvoiceDetail(invoiceDetail).Apply();

            InvoiceDetailView retView = await invMod.InvoiceDetail.Query().GetViewById(invoiceDetail.InvoiceDetailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{InvoiceDetailId}")]
        [ProducesResponseType(typeof(InvoiceDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetInvoiceDetailView(long invoiceDetailId)
        {
            InvoiceDetailModule invMod = new InvoiceDetailModule();

            InvoiceDetailView view = await invMod.InvoiceDetail.Query().GetViewById(invoiceDetailId);
            return Ok(view);
        }
        }
	}
        