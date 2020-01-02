

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.BudgetNoteDomain
{
public interface IBudgetNoteRepository
    {
        Task<BudgetNote> GetEntityById(long ? budgetNoteId);
	    Task<BudgetNote> FindEntityByExpression(Expression<Func<BudgetNote, bool>> predicate);
		Task<IList<BudgetNote>> FindEntitiesByExpression(Expression<Func<BudgetNote, bool>> predicate);
    }
}
