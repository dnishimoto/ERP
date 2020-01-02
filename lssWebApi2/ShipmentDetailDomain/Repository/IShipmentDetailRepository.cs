
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{

    public interface IShipmentDetailRepository
    {
        Task<IList<ShipmentDetail>> GetEntitiesByShipmentId(long ? shipmentId);
        Task<ShipmentDetail> GetEntityById(long ? shipmentDetailId);
        Task<ShipmentDetail> GetEntityByNumber(long shipmentDetailNumber);
        Task<NextNumber> GetNextNumber();
    }
}
