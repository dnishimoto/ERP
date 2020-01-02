

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.PackingSlipDomain
{
public interface IPackingSlipRepository
    {
        Task<PackingSlip> GetEntityById(long ? packingSlipId);
       Task<PackingSlip> GetEntityBySlipDocument(string slipDocument);
        Task<IList<PackingSlip>> GetEntityByPONumber(string PONumber);
    }
}
