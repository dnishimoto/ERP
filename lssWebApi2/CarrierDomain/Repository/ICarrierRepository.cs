

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.CarrierDomain
{
public interface ICarrierRepository
    {
        Task<Carrier> GetEntityById(long ? carrierId);
	
   
    }
}
