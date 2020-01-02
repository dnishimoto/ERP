using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class LocationAddressController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(LocationAddressView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddLocationAddress([FromBody]LocationAddressView view)
        {
            LocationAddressModule invMod = new LocationAddressModule();

            NextNumber nnLocationAddress = await invMod.LocationAddress.Query().GetNextNumber();

            view.LocationAddressNumber = nnLocationAddress.NextNumberValue;

            LocationAddress locationAddress = await invMod.LocationAddress.Query().MapToEntity(view);

            invMod.LocationAddress.AddLocationAddress(locationAddress).Apply();

            LocationAddressView newView = await invMod.LocationAddress.Query().GetViewByNumber(view.LocationAddressNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(LocationAddressView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLocationAddress([FromBody]LocationAddressView view)
        {
            LocationAddressModule invMod = new LocationAddressModule();
            LocationAddress locationAddress = await invMod.LocationAddress.Query().MapToEntity(view);
            invMod.LocationAddress.DeleteLocationAddress(locationAddress).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(LocationAddressView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLocationAddress([FromBody]LocationAddressView view)
        {
            LocationAddressModule invMod = new LocationAddressModule();

            LocationAddress locationAddress = await invMod.LocationAddress.Query().MapToEntity(view);


            invMod.LocationAddress.UpdateLocationAddress(locationAddress).Apply();

            LocationAddressView retView = await invMod.LocationAddress.Query().GetViewById(locationAddress.LocationAddressId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{LocationAddressId}")]
        [ProducesResponseType(typeof(LocationAddressView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetLocationAddressView(long locationAddressId)
        {
            LocationAddressModule invMod = new LocationAddressModule();

            LocationAddressView view = await invMod.LocationAddress.Query().GetViewById(locationAddressId);
            return Ok(view);
        }
        }
	}
        