

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.AddressBookDomain
{
   
    public interface IPhoneRepository
    {
        Task<PhoneEntity> GetEntityById(long ? phonesId);
	    Task<IList<PhoneEntity>> GetPhonesByCustomerId(long? customerId);
        Task<IList<PhoneEntity>> GetEntitiesByAddressId(long? addressId);
    }
}
