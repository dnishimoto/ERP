using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.CarrierDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class CarrierController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(CarrierView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCarrier([FromBody]CarrierView view)
        {
            CarrierModule invMod = new CarrierModule();

            NextNumber nnCarrier = await invMod.Carrier.Query().GetNextNumber();

            view.CarrierNumber = nnCarrier.NextNumberValue;

            Carrier carrier = await invMod.Carrier.Query().MapToEntity(view);

            invMod.Carrier.AddCarrier(carrier).Apply();

            CarrierView newView = await invMod.Carrier.Query().GetViewByNumber(view.CarrierNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(CarrierView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCarrier([FromBody]CarrierView view)
        {
            CarrierModule invMod = new CarrierModule();
            Carrier carrier = await invMod.Carrier.Query().MapToEntity(view);
            invMod.Carrier.DeleteCarrier(carrier).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(CarrierView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCarrier([FromBody]CarrierView view)
        {
            CarrierModule invMod = new CarrierModule();

            Carrier carrier = await invMod.Carrier.Query().MapToEntity(view);


            invMod.Carrier.UpdateCarrier(carrier).Apply();

            CarrierView retView = await invMod.Carrier.Query().GetViewById(carrier.CarrierId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{CarrierId}")]
        [ProducesResponseType(typeof(CarrierView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetCarrierView(long carrierId)
        {
            CarrierModule invMod = new CarrierModule();

            CarrierView view = await invMod.Carrier.Query().GetViewById(carrierId);
            return Ok(view);
        }
        }
	}
        