using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public interface IFluentShipmentQuery
    {
        Task<Shipment> MapToEntity(ShipmentView inputObject);
        Task<IList<Shipment>> MapToEntity(IList<ShipmentView> inputObjects);
        Task<ShipmentView> MapToView(Shipment inputObject);
        Task<NextNumber> GetNextNumber();
        Task<ShipmentView> GetViewById(long ? shipmentId);
        Task<Shipment> GetEntityById(long ? shipmentId);
        Task<Shipment> GetEntityByNumber(long shipmentNumber);
        Task<ShipmentView> GetViewByNumber(long shipmentNumber);
        Task<PageListViewContainer<ShipmentView>> GetViewsByPage(Expression<Func<Shipment, bool>> predicate, Expression<Func<Shipment, object>> order, int pageSize, int pageNumber);
        Task<Shipment> CalculatedAmountsByDetails(Shipment shipments, List<ShipmentDetail> details);
        Task<Shipment> GetShipmentBySalesOrder(ShipmentView shipmentCreation);
    }
}
