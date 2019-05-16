using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.BudgetDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentBudget : AbstractModule, IFluentBudget
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        FluentBudgetQuery _query = null;
        public IFluentBudgetQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentBudgetQuery(unitOfWork);
            }
            return _query as IFluentBudgetQuery;
        }
        public IFluentBudget MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView)
        {
            unitOfWork.budgetRepository.MapRangeToBudgetView(ref budgetView, budgetRangeView);
            return this as IFluentBudget;
        }
        public IFluentBudget TransactBudget(BudgetView budgetView)
        {
            try
            {
                Task<CreateProcessStatus> resultStatus = Task.Run(async () => await unitOfWork.budgetRepository.TransactBudget(budgetView));
                Task.WaitAll(resultStatus);
                processStatus = resultStatus.Result;
                return this as IFluentBudget;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }


        }
        public IFluentBudget Apply()
        {
            try
            {
                if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
                {
                    unitOfWork.CommitChanges();
                }
                return this as IFluentBudget;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
