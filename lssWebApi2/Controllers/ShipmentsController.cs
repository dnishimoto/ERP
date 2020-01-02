using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
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
        [ProducesResponseType(typeof(ShipmentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddShipments([FromBody]ShipmentView shipmentCreation)
        {
            ShipmentModule ShipmentsMod = new ShipmentModule();

            Shipment newShipment = await ShipmentsMod.Shipment.Query().GetShipmentBySalesOrder(shipmentCreation);

            List<ShipmentDetail> newShipmentsDetails = await ShipmentsMod.ShipmentDetail.Query().GetShipmentDetailBySalesOrder(shipmentCreation);

            newShipment = await ShipmentsMod.Shipment.Query().CalculatedAmountsByDetails(newShipment, newShipmentsDetails);


            //TODO Calculate the amount, duty, taxes, shipping cost
            //decimal taxes = await ShipmentsMod.Shipments.Query().CalculateTaxes(newShipments);
            //decimal shippingCost = await ShipmentsMod.Shipments.Query().CalculateShippingCost(newShipments);
            //decimal codCost=await ShipmentsMod.Shipments.Query().CalculateCodCost(newShipments);
            //decimal duty=await ShipmentsMod.Shipments.Query().CalculateDuty(newShipments);

            ShipmentsMod.Shipment.AddShipment(newShipment).Apply();

            Shipment lookupShipments = await ShipmentsMod.Shipment.Query().GetEntityByNumber(newShipment.ShipmentNumber);

            newShipmentsDetails.ForEach(m => m.ShipmentId = lookupShipments.ShipmentId);

            ShipmentsMod.ShipmentDetail.AddShipmentDetails(newShipmentsDetails).Apply();

            ShipmentsMod.SalesOrderDetail.UpdateSalesOrderDetailByShipmentsDetail(newShipmentsDetails).Apply();

            ShipmentsMod.SalesOrder.UpdateSalesOrderAmountByShipmentsDetail(newShipment, newShipmentsDetails.Sum(e => e.Amount)).Apply();

            ShipmentView newView = await ShipmentsMod.Shipment.Query().GetViewByNumber(newShipment.ShipmentNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ShipmentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteShipments([FromBody]ShipmentView view)
        {
            ShipmentModule invMod = new ShipmentModule();
            Shipment shipments = await invMod.Shipment.Query().MapToEntity(view);
            invMod.Shipment.DeleteShipment(shipments).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ShipmentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateShipments([FromBody]ShipmentView view)
        {
            ShipmentModule invMod = new ShipmentModule();

            Shipment shipment = await invMod.Shipment.Query().MapToEntity(view);


            invMod.Shipment.UpdateShipment(shipment).Apply();

            ShipmentView retView = await invMod.Shipment.Query().GetViewById(shipment.ShipmentId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ShipmentsId}")]
        [ProducesResponseType(typeof(ShipmentView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetShipmentsView(long shipmentsId)
        {
            ShipmentModule invMod = new ShipmentModule();

            ShipmentView view = await invMod.Shipment.Query().GetViewById(shipmentsId);
            return Ok(view);
        }
    }
}