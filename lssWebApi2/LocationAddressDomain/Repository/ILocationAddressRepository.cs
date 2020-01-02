

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using lssWebApi2.CustomerDomain;

namespace lssWebApi2.LocationAddressDomain
{
public interface ILocationAddressRepository
    {
        Task<LocationAddress> GetEntityById(long ? locationAddressId);
        Task<LocationAddress> GetEntityByCustomer(CustomerView customerView);
        Task<IList<LocationAddress>> GetLocationAddressByCustomerId(long ? customerId);
        Task<IList<LocationAddress>> GetEntitiesByAddressId(long? addressId);
        Task<IList<LocationAddress>> GetEntitiesByCustomerId(long? customerId);
    }
}
