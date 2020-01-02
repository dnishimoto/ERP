using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.LocationAddressDomain
{
    public interface IFluentLocationAddressQuery
    {
        Task<LocationAddress> MapToEntity(LocationAddressView inputObject);
        Task<List<LocationAddress>> MapToEntity(List<LocationAddressView> inputObjects);
        Task<LocationAddressView> MapToView(LocationAddress inputObject);
        Task<NextNumber> GetNextNumber();
        Task<LocationAddress> GetEntityById(long? locationAddressId);
        Task<LocationAddress> GetEntityByNumber(long locationAddressNumber);
        Task<LocationAddressView> GetViewById(long? locationAddressId);
        Task<LocationAddressView> GetViewByNumber(long locationAddressNumber);
        Task<IList<LocationAddressView>> GetLocationAddressViewsByCustomerId(long? customerId);

    }
}
