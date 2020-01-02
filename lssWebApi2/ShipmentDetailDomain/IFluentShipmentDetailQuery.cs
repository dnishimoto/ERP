using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{

    public interface IFluentShipmentDetailQuery
    {
        Task<ShipmentDetail> MapToEntity(ShipmentDetailView inputObject);
        Task<List<ShipmentDetail>> MapToEntity(List<ShipmentDetailView> inputObjects);
        Task<IList<ShipmentDetail>> GetEntitiesByShipmentId(long ? shipmentId);
        Task<IList<ShipmentDetailView>> GetViewsByShipmentId(long ? shipmentId);
        Task<ShipmentDetailView> MapToView(ShipmentDetail inputObject);
        Task<NextNumber> GetNextNumber();
        Task<ShipmentDetailView> GetViewById(long ? shipmentDetailId);
        Task<ShipmentDetailView> GetViewByNumber(long shipmentDetailNumber);
        Task<ShipmentDetail> GetEntityById(long ? shipmentDetailId);
        Task<ShipmentDetail> GetEntityByNumber(long shipmentDetailNumber);
        Task<List<ShipmentDetail>> GetShipmentDetailBySalesOrder(ShipmentView shipmentView);
    }
}
