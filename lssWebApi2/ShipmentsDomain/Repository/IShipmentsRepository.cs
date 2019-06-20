
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
public interface IShipmentsRepository
    {
        Task<Shipments> GetEntityById(long _shipmentsId);
	    Task<Shipments> GetEntityByNumber(long shipmentsNumber);
        Task<PageListViewContainer<ShipmentsView>> GetViewsByPage(Func<Shipments, bool> predicate, Func<Shipments, object> order, int pageSize, int pageNumber);
        Task<ShipmentsView> MapToView(Shipments inputObject);
        Task<ShipmentsDetailView> MapToDetailView(ShipmentsDetail inputObject);
        Task<Shipments> CreateShipmentBySalesOrder(ShipmentCreationView shipmentCreation);
    }
}
