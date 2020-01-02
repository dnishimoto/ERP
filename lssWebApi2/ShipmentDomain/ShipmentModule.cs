using lssWebApi2.TaxRatesByCodeDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SalesOrderDetailDomain;
using lssWebApi2.SalesOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentModule
    {
        public FluentShipment Shipment = new FluentShipment();
        public FluentShipmentDetail ShipmentDetail = new FluentShipmentDetail();
        public FluentSalesOrderDetail SalesOrderDetail = new FluentSalesOrderDetail();
        public FluentSalesOrder SalesOrder = new FluentSalesOrder();
        public FluentTaxRatesByCode TaxRatesByCode = new FluentTaxRatesByCode();

        public async Task<bool> CreateBySalesOrder(ShipmentView shipmentCreation)
        {
            try
            {
                Shipment newShipment = await Shipment.Query().GetShipmentBySalesOrder(shipmentCreation);

                List<ShipmentDetail> newShipmentDetails = await ShipmentDetail.Query().GetShipmentDetailBySalesOrder(shipmentCreation);


                newShipment = await Shipment.Query().CalculatedAmountsByDetails(newShipment, newShipmentDetails);

                TaxRatesByCodeView lookupTaxesByCode = await TaxRatesByCode.Query().GetViewByCode(TypeofTaxRatesByCode.StateTaxUT.ToString());

                newShipment.Tax = newShipment.Amount * lookupTaxesByCode.TaxRate;

                //TODO Calculate the codCost, duty, shipping cost
                //decimal shippingCost = await ShipmentsMod.Shipments.Query().CalculateShippingCost(newShipments);
                //decimal codCost=await ShipmentsMod.Shipments.Query().CalculateCodCost(newShipments);
                //decimal duty=await ShipmentsMod.Shipments.Query().CalculateDuty(newShipments);

                Shipment.AddShipment(newShipment).Apply();

                Shipment lookupShipment = await Shipment.Query().GetEntityByNumber(newShipment.ShipmentNumber);

                newShipmentDetails.ForEach(m => m.ShipmentId = lookupShipment.ShipmentId);
                ShipmentDetail.AddShipmentDetails(newShipmentDetails).Apply();

                IList<ShipmentDetail> updateShipmentDetails = await ShipmentDetail.Query().GetEntitiesByShipmentId(lookupShipment.ShipmentId);
                SalesOrderDetail.UpdateSalesOrderDetailByShipmentsDetail(newShipmentDetails).Apply();

                SalesOrder.UpdateSalesOrderAmountByShipmentsDetail(newShipment, newShipmentDetails.Sum(e => e.Amount)).Apply();

                return true;
            }
            catch (Exception ex) { throw new Exception("CreateBySalesOrder",ex); }


        }
    }
}
