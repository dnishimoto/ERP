
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain.Repository
{

    public interface IShipmentsDetailRepository
    {
        Task<List<ShipmentsDetail>> GetEntitiesByShipmentId(long shipmentId);
        Task<ShipmentsDetail> GetEntityById(long shipmentId);
        Task<ShipmentsDetail> GetEntityByNumber(long shipmentDetailNumber);
        Task<List<ShipmentsDetail>> CreateShipmentsDetailBySalesOrder(ShipmentCreationView shipmentCreation);
        Task<NextNumber> GetNextNumber();
    }
}
