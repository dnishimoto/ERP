using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.NextNumberDomain
{
    public interface INextNumberRepository
    {
        Task<NextNumber> GetNextNumber(string nextNumberName);
        Task<NextNumber> GetEntityById(long? nextNumberId);
        Task<NextNumber> GetEntityByNumber(long nextNumberValue);
        IQueryable<NextNumber> GetEntitiesByExpression(Expression<Func<NextNumber, bool>> predicate);
    }
}
