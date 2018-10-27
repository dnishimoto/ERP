using ERP_Core2.AbstractFactory;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentGeneralLedgerQuery : AbstractErrorHandling, IGeneralLedgerQuery
    {
        UnitOfWork _unitOfWork = null;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public FluentGeneralLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear)
        {
            IEnumerable<AccountSummaryView> views =  _unitOfWork.generalLedgerRepository.GetAccountSummaryByFiscalYearViews(fiscalYear);
            return views;
        }
        public GeneralLedgerView GetLedgerViewById(long accountId)
        {
            Task<GeneralLedgerView> viewTask = Task.Run(async()=>await _unitOfWork.generalLedgerRepository.GetLedgerViewById(accountId));
            Task.WaitAll(viewTask);
            return viewTask.Result;

        }
        public GeneralLedgerView GetLedgerViewByExpression(Expression<Func<GeneralLedger, bool>> predicate)
        {
            var query = _unitOfWork.generalLedgerRepository.GetObjectsQueryable(predicate) as IQueryable<GeneralLedger>;

            GeneralLedger ledger = query.FirstOrDefault<GeneralLedger>();

            GeneralLedgerView view=applicationViewFactory.MapGeneralLedgerView(ledger);
            return view;
        }
        public GeneralLedgerView GetGeneralLedgerView(long docNumber, string docType)
        {
            Task<GeneralLedgerView> viewTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(docNumber, docType));
            Task.WaitAll(viewTask);
            return viewTask.Result;
        }
    }
}
