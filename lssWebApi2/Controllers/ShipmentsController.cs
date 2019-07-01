using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ShipmentsDomain;
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
        public async Task<IActionResult> AddShipments([FromBody]ShipmentCreationView shipmentCreation)
        {
            ShipmentsModule ShipmentsMod = new ShipmentsModule();

            Shipments newShipments = await ShipmentsMod.Shipments.Query().CreateShipmentBySalesOrder(shipmentCreation);

            List<ShipmentsDetail> newShipmentsDetails = await ShipmentsMod.ShipmentsDetail.Query().CreateShipmentsDetailBySalesOrder(shipmentCreation);

            newShipments = await ShipmentsMod.Shipments.Query().CalculatedAmountsByDetails(newShipments, newShipmentsDetails);


            //TODO Calculate the amount, duty, taxes, shipping cost
            //decimal taxes = await ShipmentsMod.Shipments.Query().CalculateTaxes(newShipments);
            //decimal shippingCost = await ShipmentsMod.Shipments.Query().CalculateShippingCost(newShipments);
            //decimal codCost=await ShipmentsMod.Shipments.Query().CalculateCodCost(newShipments);
            //decimal duty=await ShipmentsMod.Shipments.Query().CalculateDuty(newShipments);

            ShipmentsMod.Shipments.AddShipments(newShipments).Apply();

            Shipments lookupShipments = await ShipmentsMod.Shipments.Query().GetEntityByNumber(newShipments.ShipmentNumber);

            newShipmentsDetails.ForEach(m => m.ShipmentId = lookupShipments.ShipmentId);

            ShipmentsMod.ShipmentsDetail.AddShipmentsDetails(newShipmentsDetails).Apply();

            ShipmentsMod.SalesOrderDetail.UpdateSalesOrderDetailByShipmentsDetail(newShipmentsDetails).Apply();

            ShipmentsMod.SalesOrder.UpdateSalesOrderAmountByShipmentsDetail(newShipments, newShipmentsDetails.Sum(e => e.Amount)).Apply();

            ShipmentsView newView = await ShipmentsMod.Shipments.Query().GetViewByNumber(newShipments.ShipmentNumber);


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