using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.GeneralLedgerDomain.Repository
{
    public interface IGeneralLedgerRepository
    {
        Task<List<IncomeStatementView>> GetIncomeStatementView(long fiscalYear);
         IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear);
         Task<CreateProcessStatus> CreateLedgerFromView(GeneralLedgerView view);
         Task<GeneralLedgerView> GetLedgerViewById(long generalLedgerId);
        Task<CreateProcessStatus> CreateLedgerFromReceiveable(AccountReceiveableView accountReceivableView);
        Task<CreateProcessStatus> UpdateGeneralLedger(GeneralLedger generalLedger);
      CreateProcessStatus DeleteGeneralLedger(GeneralLedger generalLedger);
    }
}
