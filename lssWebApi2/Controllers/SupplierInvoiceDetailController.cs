using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.SupplierInvoiceDetailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SupplierInvoiceDetailController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierInvoiceDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSupplierInvoiceDetail([FromBody]SupplierInvoiceDetailView view)
        {
            SupplierInvoiceDetailModule invMod = new SupplierInvoiceDetailModule();

            NextNumber nnSupplierInvoiceDetail = await invMod.SupplierInvoiceDetail.Query().GetNextNumber();

            view.SupplierInvoiceDetailNumber = nnSupplierInvoiceDetail.NextNumberValue;

            SupplierInvoiceDetail supplierInvoiceDetail = await invMod.SupplierInvoiceDetail.Query().MapToEntity(view);

            invMod.SupplierInvoiceDetail.AddSupplierInvoiceDetail(supplierInvoiceDetail).Apply();

            SupplierInvoiceDetailView newView = await invMod.SupplierInvoiceDetail.Query().GetViewByNumber(view.SupplierInvoiceDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierInvoiceDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSupplierInvoiceDetail([FromBody]SupplierInvoiceDetailView view)
        {
            SupplierInvoiceDetailModule invMod = new SupplierInvoiceDetailModule();
            SupplierInvoiceDetail supplierInvoiceDetail = await invMod.SupplierInvoiceDetail.Query().MapToEntity(view);
            invMod.SupplierInvoiceDetail.DeleteSupplierInvoiceDetail(supplierInvoiceDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierInvoiceDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSupplierInvoiceDetail([FromBody]SupplierInvoiceDetailView view)
        {
            SupplierInvoiceDetailModule invMod = new SupplierInvoiceDetailModule();

            SupplierInvoiceDetail supplierInvoiceDetail = await invMod.SupplierInvoiceDetail.Query().MapToEntity(view);


            invMod.SupplierInvoiceDetail.UpdateSupplierInvoiceDetail(supplierInvoiceDetail).Apply();

            SupplierInvoiceDetailView retView = await invMod.SupplierInvoiceDetail.Query().GetViewById(supplierInvoiceDetail.SupplierInvoiceDetailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{SupplierInvoiceDetailId}")]
        [ProducesResponseType(typeof(SupplierInvoiceDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSupplierInvoiceDetailView(long supplierInvoiceDetailId)
        {
            SupplierInvoiceDetailModule invMod = new SupplierInvoiceDetailModule();

            SupplierInvoiceDetailView view = await invMod.SupplierInvoiceDetail.Query().GetViewById(supplierInvoiceDetailId);
            return Ok(view);
        }
        }
	}
        