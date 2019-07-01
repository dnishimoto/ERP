using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP_Core2.TaxRatesByCodeDomain;
using lssWebApi2.TaxRatesByCodeDomain;


namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class TaxRatesByCodeController : Controller
    {

        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(TaxRatesByCodeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTaxRatesByCode([FromBody]TaxRatesByCodeView view)
        {
            TaxRatesByCodeModule invMod = new TaxRatesByCodeModule();

            NextNumber nnTaxRatesByCode = await invMod.TaxRatesByCode.Query().GetNextNumber();

            view.TaxRatesByCodeNumber = nnTaxRatesByCode.NextNumberValue;

            TaxRatesByCode TaxRatesByCode = await invMod.TaxRatesByCode.Query().MapToEntity(view);

            invMod.TaxRatesByCode.AddTaxRatesByCode(TaxRatesByCode).Apply();

            TaxRatesByCodeView newView = await invMod.TaxRatesByCode.Query().GetViewByNumber(view.TaxRatesByCodeNumber);

            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(TaxRatesByCodeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTaxRatesByCode([FromBody]TaxRatesByCodeView view)
        {
            TaxRatesByCodeModule invMod = new TaxRatesByCodeModule();
            TaxRatesByCode TaxRatesByCode = await invMod.TaxRatesByCode.Query().MapToEntity(view);
            invMod.TaxRatesByCode.DeleteTaxRatesByCode(TaxRatesByCode).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(TaxRatesByCodeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTaxRatesByCode([FromBody]TaxRatesByCodeView view)
        {
            TaxRatesByCodeModule invMod = new TaxRatesByCodeModule();

            TaxRatesByCode TaxRatesByCode = await invMod.TaxRatesByCode.Query().MapToEntity(view);


            invMod.TaxRatesByCode.UpdateTaxRatesByCode(TaxRatesByCode).Apply();

            TaxRatesByCodeView retView = await invMod.TaxRatesByCode.Query().GetViewById(TaxRatesByCode.TaxRatesByCodeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{TaxRatesByCodeId}")]
        [ProducesResponseType(typeof(TaxRatesByCodeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaxRatesByCodeView(long TaxRatesByCodeId)
        {
            TaxRatesByCodeModule invMod = new TaxRatesByCodeModule();

            TaxRatesByCodeView view = await invMod.TaxRatesByCode.Query().GetViewById(TaxRatesByCodeId);
            return Ok(view);
        }
    }
}