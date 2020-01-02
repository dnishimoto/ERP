using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PackingSlipDetailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PackingSlipDetailController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PackingSlipDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPackingSlipDetail([FromBody]PackingSlipDetailView view)
        {
            PackingSlipDetailModule invMod = new PackingSlipDetailModule();

            NextNumber nnPackingSlipDetail = await invMod.PackingSlipDetail.Query().GetNextNumber();

            view.PackingSlipDetailNumber = nnPackingSlipDetail.NextNumberValue;

            PackingSlipDetail packingSlipDetail = await invMod.PackingSlipDetail.Query().MapToEntity(view);

            invMod.PackingSlipDetail.AddPackingSlipDetail(packingSlipDetail).Apply();

            PackingSlipDetailView newView = await invMod.PackingSlipDetail.Query().GetViewByNumber(view.PackingSlipDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PackingSlipDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePackingSlipDetail([FromBody]PackingSlipDetailView view)
        {
            PackingSlipDetailModule invMod = new PackingSlipDetailModule();
            PackingSlipDetail packingSlipDetail = await invMod.PackingSlipDetail.Query().MapToEntity(view);
            invMod.PackingSlipDetail.DeletePackingSlipDetail(packingSlipDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PackingSlipDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePackingSlipDetail([FromBody]PackingSlipDetailView view)
        {
            PackingSlipDetailModule invMod = new PackingSlipDetailModule();

            PackingSlipDetail packingSlipDetail = await invMod.PackingSlipDetail.Query().MapToEntity(view);


            invMod.PackingSlipDetail.UpdatePackingSlipDetail(packingSlipDetail).Apply();

            PackingSlipDetailView retView = await invMod.PackingSlipDetail.Query().GetViewById(packingSlipDetail.PackingSlipDetailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PackingSlipDetailId}")]
        [ProducesResponseType(typeof(PackingSlipDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPackingSlipDetailView(long packingSlipDetailId)
        {
            PackingSlipDetailModule invMod = new PackingSlipDetailModule();

            PackingSlipDetailView view = await invMod.PackingSlipDetail.Query().GetViewById(packingSlipDetailId);
            return Ok(view);
        }
        }
	}
        