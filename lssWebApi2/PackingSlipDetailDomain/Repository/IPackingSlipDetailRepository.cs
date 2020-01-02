

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.PackingSlipDetailDomain
{
public interface IPackingSlipDetailRepository
    {
        Task<PackingSlipDetail> GetEntityById(long ? packingSlipDetailId);
        Task<IList<PackingSlipDetail>> FindByExpression(Expression<Func<PackingSlipDetail, bool>> predicate);
        Task<IList<PackingSlipDetail>> GetEntitiesByPackingSlipId(long? packingSlipId);
    }
}
