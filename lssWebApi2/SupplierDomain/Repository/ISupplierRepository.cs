

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.SupplierDomain
{
public interface ISupplierRepository
    {
        Task<Supplier> GetEntityById(long ? supplierId);
      Task<Supplier> GetEntityByEmail(EmailEntity email);
        Task<Supplier> FindEntityByAddressId(long ? addressId);
    }
}
