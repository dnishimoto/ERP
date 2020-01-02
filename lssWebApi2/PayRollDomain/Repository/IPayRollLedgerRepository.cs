

using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollLedgerRepository
    {
        Task<PayRollLedger> GetEntityById(long _payRollLedgerId);
        Task<IList<PayRollLedger>> GetEntitiesByPaySequence(Expression<Func<PayRollLedger, bool>> predicate);
    }
}
