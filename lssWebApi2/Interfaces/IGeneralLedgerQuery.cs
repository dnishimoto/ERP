using ERP_Core2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IGeneralLedgerQuery
    {
        GeneralLedgerView GetGeneralLedgerView(long docNumber,string docType);
        IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear);
        GeneralLedgerView GetLedgerViewByExpression(Expression<Func<GeneralLedger, bool>> predicate);
        GeneralLedgerView GetLedgerViewById(long accountId);
    }
}
