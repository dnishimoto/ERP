using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.SupplierDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSupplier([FromBody]SupplierView view)
        {
            SupplierModule invMod = new SupplierModule();

            NextNumber nnSupplier = await invMod.Supplier.Query().GetNextNumber();

            view.SupplierNumber = nnSupplier.NextNumberValue;

            Supplier supplier = await invMod.Supplier.Query().MapToEntity(view);

            invMod.Supplier.AddSupplier(supplier).Apply();

            SupplierView newView = await invMod.Supplier.Query().GetViewByNumber(view.SupplierNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSupplier([FromBody]SupplierView view)
        {
            SupplierModule invMod = new SupplierModule();
            Supplier supplier = await invMod.Supplier.Query().MapToEntity(view);
            invMod.Supplier.DeleteSupplier(supplier).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SupplierView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSupplier([FromBody]SupplierView view)
        {
            SupplierModule invMod = new SupplierModule();

            Supplier supplier = await invMod.Supplier.Query().MapToEntity(view);


            invMod.Supplier.UpdateSupplier(supplier).Apply();

            SupplierView retView = await invMod.Supplier.Query().GetViewById(supplier.SupplierId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{SupplierId}")]
        [ProducesResponseType(typeof(SupplierView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSupplierView(long supplierId)
        {
            SupplierModule invMod = new SupplierModule();

            SupplierView view = await invMod.Supplier.Query().GetViewById(supplierId);
            return Ok(view);
        }
        }
	}
        