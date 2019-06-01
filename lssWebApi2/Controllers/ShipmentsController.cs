using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ShipmentsDomain;
using lssWebApi2.ShipmentsDomain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ShipmentsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddShipments([FromBody]ShipmentsView view)
        {
            ShipmentsModule invMod = new ShipmentsModule();

            NextNumber nnShipments = await invMod.Shipments.Query().GetNextNumber();

            view.ShipmentNumber = nnShipments.NextNumberValue;

            Shipments shipments = await invMod.Shipments.Query().MapToEntity(view);

            invMod.Shipments.AddShipments(shipments).Apply();

            ShipmentsView newView = await invMod.Shipments.Query().GetViewByNumber(view.ShipmentNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ShipmentsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteShipments([FromBody]ShipmentsView view)
        {
            ShipmentsModule invMod = new ShipmentsModule();
            Shipments shipments = await invMod.Shipments.Query().MapToEntity(view);
            invMod.Shipments.DeleteShipments(shipments).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ShipmentsView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateShipments([FromBody]ShipmentsView view)
        {
            ShipmentsModule invMod = new ShipmentsModule();

            Shipments shipments = await invMod.Shipments.Query().MapToEntity(view);


            invMod.Shipments.UpdateShipments(shipments).Apply();

            ShipmentsView retView = await invMod.Shipments.Query().GetViewById(shipments.ShipmentId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ShipmentsId}")]
        [ProducesResponseType(typeof(ShipmentsView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetShipmentsView(long shipmentsId)
        {
            ShipmentsModule invMod = new ShipmentsModule();

            ShipmentsView view = await invMod.Shipments.Query().GetViewById(shipmentsId);
            return Ok(view);
        }
    }
}