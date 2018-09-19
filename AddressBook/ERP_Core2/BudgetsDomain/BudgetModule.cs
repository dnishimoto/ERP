using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.BudgetDomain
{

    public interface IBudgetQuery
    {
        BudgetActualsView GetBudgetActuals(BudgetRangeView budgetRangeView);
    }
    public class FluentBudgetQuery : AbstractModule, IBudgetQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentBudgetQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public BudgetActualsView GetBudgetActuals(BudgetRangeView budgetRangeView)
        {
            Task<BudgetActualsView> budgetActualsViewTask = Task.Run(async()=>await _unitOfWork.budgetRepository.GetActuals(budgetRangeView));
            Task.WaitAll(budgetActualsViewTask);
            return budgetActualsViewTask.Result;
        }
    }
    public interface IBudget
    {
        IBudget TransactBudget(BudgetView budgetView);
        IBudget Apply();
        IBudget MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        IBudgetQuery Query();
    }

    public class FluentBudget : AbstractModule, IBudget
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        FluentBudgetQuery _query = null;
        public IBudgetQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentBudgetQuery(unitOfWork);
            }
            return _query as IBudgetQuery;
        }
        public IBudget MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView)
        {
            unitOfWork.budgetRepository.MapRangeToBudgetView(ref budgetView, budgetRangeView);
            return this as IBudget;
        }
        public IBudget TransactBudget(BudgetView budgetView)
        {
            try
            {
                Task<CreateProcessStatus> resultStatus = Task.Run(async () => await unitOfWork.budgetRepository.TransactBudget(budgetView));
                Task.WaitAll(resultStatus);
                processStatus = resultStatus.Result;
                return this as IBudget;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }


        }
        public IBudget Apply()
        {
            try
            {
                if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
                {
                    unitOfWork.CommitChanges();
                }
                return this as IBudget;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

          }
    }
       
    public class BudgetModule
    {
        public FluentBudget Budget = new FluentBudget();
        public FluentBudgetRange BudgetRange = new FluentBudgetRange();
    }
}
