

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.BuyerDomain
{
public interface IBuyerRepository
    {
        Task<Buyer> GetEntityById(long ? buyerId);
	  
    }
}
