using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public interface IFluentShipmentsQuery
    {
        Task<Shipments> MapToEntity(ShipmentsView inputObject);
        Task<List<Shipments>> MapToEntity(List<ShipmentsView> inputObjects);
        Task<ShipmentsView> MapToView(Shipments inputObject);
        Task<NextNumber> GetNextNumber();
        Task<ShipmentsView> GetViewById(long shipmentId);
        Task<Shipments> GetEntityById(long shipmentId);
        Task<Shipments> GetEntityByNumber(long shipmentNumber);
        Task<ShipmentsView> GetViewByNumber(long shipmentNumber);
        Task<PageListViewContainer<ShipmentsView>> GetViewsByPage(Func<Shipments, bool> predicate, Func<Shipments, object> order, int pageSize, int pageNumber);
        Task<Shipments> CreateShipmentBySalesOrder(ShipmentCreationView shipmentCreation);

        Task<Shipments> CalculatedAmountsByDetails(Shipments shipments, List<ShipmentsDetail> details);


    }
}
