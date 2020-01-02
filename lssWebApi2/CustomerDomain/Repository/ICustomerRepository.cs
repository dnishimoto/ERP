

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.CustomerDomain
{
public interface ICustomerRepository
    {
        Task<Customer> GetEntityById(long ? customerId);
	    Task<Customer> GetEntityByAddressId(long ? addressId);


    }
}
