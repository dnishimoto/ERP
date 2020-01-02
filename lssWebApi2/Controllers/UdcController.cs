using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class UdcController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(UdcView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUdc([FromBody]UdcView view)
        {
            UdcModule invMod = new UdcModule();

            NextNumber nnUdc = await invMod.Udc.Query().GetNextNumber();

            view.UdcNumber = nnUdc.NextNumberValue;

            Udc udc = await invMod.Udc.Query().MapToEntity(view);

            invMod.Udc.AddUdc(udc).Apply();

            UdcView newView = await invMod.Udc.Query().GetViewByNumber(view.UdcNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(UdcView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUdc([FromBody]UdcView view)
        {
            UdcModule invMod = new UdcModule();
            Udc udc = await invMod.Udc.Query().MapToEntity(view);
            invMod.Udc.DeleteUdc(udc).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(UdcView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUdc([FromBody]UdcView view)
        {
            UdcModule invMod = new UdcModule();

            Udc udc = await invMod.Udc.Query().MapToEntity(view);


            invMod.Udc.UpdateUdc(udc).Apply();

            UdcView retView = await invMod.Udc.Query().GetViewById(udc.XrefId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{UdcId}")]
        [ProducesResponseType(typeof(UdcView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetUdcView(long udcId)
        {
            UdcModule invMod = new UdcModule();

            UdcView view = await invMod.Udc.Query().GetViewById(udcId);
            return Ok(view);
        }
        }
	}
        