
using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
public interface IShipmentsRepository
    {
        Task<Shipments> GetEntityById(long _shipmentsId);
	    Task<Shipments> GetEntityByNumber(long shipmentsNumber);
    
    }
}
