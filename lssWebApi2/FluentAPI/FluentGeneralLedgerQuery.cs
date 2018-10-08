using ERP_Core2.AbstractFactory;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentGeneralLedgerQuery : AbstractErrorHandling, IGeneralLedgerQuery
    {
        UnitOfWork _unitOfWork = null;
        public FluentGeneralLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public List<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear)
        {
            Task<List<AccountSummaryView>> viewTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetAccountSummaryByFiscalYearViews(fiscalYear));
            Task.WaitAll(viewTask);
            return viewTask.Result;
        }

        public GeneralLedgerView GetGeneralLedgerView(long docNumber, string docType)
        {
            Task<GeneralLedgerView> viewTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(docNumber, docType));
            Task.WaitAll(viewTask);
            return viewTask.Result;
        }
    }
}
