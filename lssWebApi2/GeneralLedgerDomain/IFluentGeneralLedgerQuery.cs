using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.GeneralLedgerDomain
{
    public interface IFluentGeneralLedgerQuery
    {
        Task<NextNumber> GetDocNumber();
        Task<NextNumber> GetNextNumber();
        Task<GeneralLedgerView> GetViewByDocNumber(long? docNumber, string docType);
        IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear);
        Task<GeneralLedgerView> GetLedgerViewByExpression(Expression<Func<GeneralLedger, bool>> predicate);
        Task<GeneralLedgerView> GetViewById(long ? generalLedgerId);
        Task<GeneralLedger> GetEntityById(long? generalLedgerId);
        Task<IList<IncomeView>> GetIncomeViews();
        Task<IList<IncomeStatementView>> GetIncomeStatementViews(long fiscalYear);
        Task<GeneralLedger> MapToEntity(GeneralLedgerView inputObject);
        Task<IList<GeneralLedger>> MapToEntity(IList<GeneralLedgerView> inputObjects);
        Task<GeneralLedgerView> MapToView(GeneralLedger inputObject);
        Task<PageListViewContainer<GeneralLedgerView>> GetViewsByPage(Expression<Func<GeneralLedger, bool>> predicate, Expression<Func<GeneralLedger, object>> order, int pageSize, int pageNumber);
    }
}
