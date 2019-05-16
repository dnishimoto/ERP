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
    public class FluentBudgetRange : AbstractModule, IFluentBudgetRange
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        FluentBudgetRangeQuery _query = null;
        public IFluentBudgetRangeQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentBudgetRangeQuery(unitOfWork);
            }
            return _query as IFluentBudgetRangeQuery;
        }
        public IFluentBudgetRange CreateBudgetRange(BudgetRangeView budgetRange)
        {
            try
            {
                Task<CreateProcessStatus> resultStatus = Task.Run(async () => await unitOfWork.budgetRangeRepository.CreateBudgetRange(budgetRange));
                Task.WaitAll(resultStatus);
                processStatus = resultStatus.Result;
                return this as IFluentBudgetRange;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IFluentBudgetRange Apply()
        {
            try
            {
                if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
                {
                    unitOfWork.CommitChanges();
                }
                return this as IFluentBudgetRange;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
