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
    public class FluentBudgetRange : AbstractModule, IBudgetRange
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        FluentBudgetRangeQuery _query = null;
        public IBudgetRangeQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentBudgetRangeQuery(unitOfWork);
            }
            return _query as IBudgetRangeQuery;
        }
        public IBudgetRange CreateBudgetRange(BudgetRangeView budgetRange)
        {
            try
            {
                Task<CreateProcessStatus> resultStatus = Task.Run(async () => await unitOfWork.budgetRangeRepository.CreateBudgetRange(budgetRange));
                Task.WaitAll(resultStatus);
                processStatus = resultStatus.Result;
                return this as IBudgetRange;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IBudgetRange Apply()
        {
            try
            {
                if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
                {
                    unitOfWork.CommitChanges();
                }
                return this as IBudgetRange;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
