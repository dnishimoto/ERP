using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PackingSlipDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PackingSlipController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PackingSlipView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPackingSlip([FromBody]PackingSlipView view)
        {
            PackingSlipModule invMod = new PackingSlipModule();

            NextNumber nnPackingSlip = await invMod.PackingSlip.Query().GetNextNumber();

            view.PackingSlipNumber = nnPackingSlip.NextNumberValue;

            PackingSlip packingSlip = await invMod.PackingSlip.Query().MapToEntity(view);

            invMod.PackingSlip.AddPackingSlip(packingSlip).Apply();

            PackingSlipView newView = await invMod.PackingSlip.Query().GetViewByNumber(view.PackingSlipNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PackingSlipView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePackingSlip([FromBody]PackingSlipView view)
        {
            PackingSlipModule invMod = new PackingSlipModule();
            PackingSlip packingSlip = await invMod.PackingSlip.Query().MapToEntity(view);
            invMod.PackingSlip.DeletePackingSlip(packingSlip).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PackingSlipView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePackingSlip([FromBody]PackingSlipView view)
        {
            PackingSlipModule invMod = new PackingSlipModule();

            PackingSlip packingSlip = await invMod.PackingSlip.Query().MapToEntity(view);


            invMod.PackingSlip.UpdatePackingSlip(packingSlip).Apply();

            PackingSlipView retView = await invMod.PackingSlip.Query().GetViewById(packingSlip.PackingSlipId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PackingSlipId}")]
        [ProducesResponseType(typeof(PackingSlipView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPackingSlipView(long packingSlipId)
        {
            PackingSlipModule invMod = new PackingSlipModule();

            PackingSlipView view = await invMod.PackingSlip.Query().GetViewById(packingSlipId);
            return Ok(view);
        }
        }
	}
        