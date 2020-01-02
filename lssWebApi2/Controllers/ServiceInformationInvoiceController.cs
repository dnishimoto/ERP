using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ServiceInformationInvoiceDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ServiceInformationInvoiceController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ServiceInformationInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddServiceInformationInvoice([FromBody]ServiceInformationInvoiceView view)
        {
            ServiceInformationInvoiceModule invMod = new ServiceInformationInvoiceModule();

            NextNumber nnServiceInformationInvoice = await invMod.ServiceInformationInvoice.Query().GetNextNumber();

            view.ServiceInformationInvoiceNumber = nnServiceInformationInvoice.NextNumberValue;

            ServiceInformationInvoice serviceInformationInvoice = await invMod.ServiceInformationInvoice.Query().MapToEntity(view);

            invMod.ServiceInformationInvoice.AddServiceInformationInvoice(serviceInformationInvoice).Apply();

            ServiceInformationInvoiceView newView = await invMod.ServiceInformationInvoice.Query().GetViewByNumber(view.ServiceInformationInvoiceNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ServiceInformationInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteServiceInformationInvoice([FromBody]ServiceInformationInvoiceView view)
        {
            ServiceInformationInvoiceModule invMod = new ServiceInformationInvoiceModule();
            ServiceInformationInvoice serviceInformationInvoice = await invMod.ServiceInformationInvoice.Query().MapToEntity(view);
            invMod.ServiceInformationInvoice.DeleteServiceInformationInvoice(serviceInformationInvoice).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ServiceInformationInvoiceView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateServiceInformationInvoice([FromBody]ServiceInformationInvoiceView view)
        {
            ServiceInformationInvoiceModule invMod = new ServiceInformationInvoiceModule();

            ServiceInformationInvoice serviceInformationInvoice = await invMod.ServiceInformationInvoice.Query().MapToEntity(view);


            invMod.ServiceInformationInvoice.UpdateServiceInformationInvoice(serviceInformationInvoice).Apply();

            ServiceInformationInvoiceView retView = await invMod.ServiceInformationInvoice.Query().GetViewById(serviceInformationInvoice.ServiceInformationInvoiceId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ServiceInformationInvoiceId}")]
        [ProducesResponseType(typeof(ServiceInformationInvoiceView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetServiceInformationInvoiceView(long serviceInformationInvoiceId)
        {
            ServiceInformationInvoiceModule invMod = new ServiceInformationInvoiceModule();

            ServiceInformationInvoiceView view = await invMod.ServiceInformationInvoice.Query().GetViewById(serviceInformationInvoiceId);
            return Ok(view);
        }
        }
	}
        