using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.GeneralLedgerDomain.Repository
{
    public interface IGeneralLedgerRepository
    {
        Task<IList<IncomeStatementView>> GetIncomeStatementView(long fiscalYear);
        IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear);
        Task<GeneralLedger> GetEntityByDocNumber(long? docNumber, string docType);
        Task<GeneralLedger> FindEntityByView(GeneralLedgerView view);
        Task<GeneralLedger> GetEntityById(long? generalLedgerId);
        Task<GeneralLedger> FindEntityByDocNumber(long? docNumber);
        Task<Decimal> GetGLAmountByDocNumber(long ? docNumber);
        Task<GeneralLedger> FindEntityByExpression(Expression<Func<GeneralLedger, bool>> predicate);
        Task<IList<GeneralLedger>> FindEntitiesByExpression(Expression<Func<GeneralLedger, bool>> predicate);
        IQueryable<GeneralLedger> GetEntitiesByExpression(Expression<Func<GeneralLedger, bool>> predicate);

    }
}
