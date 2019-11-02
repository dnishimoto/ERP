

using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollLedgerRepository
    {
        Task<PayRollLedger> GetEntityById(long _payRollLedgerId);
        Task<List<PayRollLedger>> GetEntitiesByPaySequence(Expression<Func<PayRollLedger, bool>> predicate, string include);
    }
}
