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

        public List<IncomeStatementView> GetIncomeStatementViews(long fiscalYear)
        {
            Task<List<IncomeStatementView>> viewsTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetIncomeStatementView(fiscalYear));
            Task.WaitAll(viewsTask);
            return viewsTask.Result;
          }
        public IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear)
        {
            try
            {
                IEnumerable<AccountSummaryView> views = _unitOfWork.generalLedgerRepository.GetAccountSummaryByFiscalYearViews(fiscalYear);
                return views;
            }
            catch (Exception ex)

            { throw new Exception(GetMyMethodName(), ex); }
        }
        public List<IncomeView> GetIncomeViews()
        {
            Task<List<IncomeView>> viewTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetIncomeViews());
            Task.WaitAll(viewTask);
            return viewTask.Result;
        }
        public GeneralLedgerView GetLedgerViewById(long accountId)
        {
            try
            {
                Task<GeneralLedgerView> viewTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetLedgerViewById(accountId));
                Task.WaitAll(viewTask);
                return viewTask.Result;
            }
            catch (Exception ex)
  
                { throw new Exception(GetMyMethodName(), ex); }
 

        }
        public GeneralLedgerView GetLedgerViewByExpression(Expression<Func<GeneralLedger, bool>> predicate)
        {
            try
            {
                GeneralLedgerView view=null;
                var query = _unitOfWork.generalLedgerRepository.GetObjectsQueryable(predicate) as IQueryable<GeneralLedger>;

                GeneralLedger ledger = query.FirstOrDefault<GeneralLedger>();
                if (ledger!=null)
                {
                    view = applicationViewFactory.MapGeneralLedgerView(ledger);
                }
               
                return view;
            }
            catch (Exception ex)

            { throw new Exception(GetMyMethodName(), ex); }
        }
        public GeneralLedgerView GetGeneralLedgerView(long docNumber, string docType)
        {
            try
            {
                Task<GeneralLedgerView> viewTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetLedgerByDocNumber(docNumber, docType));
                Task.WaitAll(viewTask);
                return viewTask.Result;
            }
            catch (Exception ex)

            { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
