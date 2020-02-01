using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.BudgetDomain;
using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetDomain
{
    public class FluentBudget : AbstractModule, IFluentBudget
    {
        UnitOfWork unitOfWork;
        CreateProcessStatus processStatus;
        ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public FluentBudget(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
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
            applicationViewFactory.MapRangeToBudgetViewEntity(ref budgetView, budgetRangeView);

            return this as IFluentBudget;
        }
        private Budget MapToEntity(BudgetView inputObject)
        {
            Mapper mapper = new Mapper();
            Budget outObject = mapper.Map<Budget>(inputObject);
            return outObject;
        }

        public IFluentBudget TransactBudget(BudgetView budgetView)
        {
            try
            {

                Task<Budget> budgetLookupTask = Task.Run(async () => await unitOfWork.budgetRepository.FindEntityByExpression
                (
                    e =>e.AccountId == budgetView.AccountId
                    && e.RangeId == budgetView.RangeId
                    ));
                Task.WaitAll(budgetLookupTask);

               
                if (budgetLookupTask.Result == null)
                {
                    Budget newBudget = MapToEntity(budgetView);

                    AddBudget(newBudget);
                    return this as IFluentBudget;
                }
                else

                {
                    budgetView.BudgetId = budgetLookupTask.Result.BudgetId;
                    budgetLookupTask.Result.BudgetId = budgetView.BudgetId;
                    budgetLookupTask.Result.BudgetHours = budgetView.BudgetHours;
                    budgetLookupTask.Result.BudgetAmount = budgetView.BudgetAmount;
                    budgetLookupTask.Result.ActualHours = budgetView.ActualHours;
                    budgetLookupTask.Result.ActualAmount = budgetView.ActualAmount;
                    budgetLookupTask.Result.AccountId = budgetView.AccountId;
                    budgetLookupTask.Result.RangeId = budgetView.RangeId;
                    budgetLookupTask.Result.ProjectedHours = budgetView.ProjectedHours ?? 0;
                    budgetLookupTask.Result.ProjectedAmount = budgetView.ProjectedAmount ?? 0;
                    budgetLookupTask.Result.ActualsAsOfDate = budgetView.ActualsAsOfDate;
                   
                    UpdateBudget(budgetLookupTask.Result);
                    return this as IFluentBudget;
                }
               
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
        public IFluentBudget AddBudgets(List<Budget> newObjects)
        {
            unitOfWork.budgetRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBudget;
        }
        public IFluentBudget UpdateBudgets(IList<Budget> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.budgetRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBudget;
        }
        public IFluentBudget AddBudget(Budget newObject)
        {
            unitOfWork.budgetRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBudget;
        }
        public IFluentBudget UpdateBudget(Budget updateObject)
        {
            unitOfWork.budgetRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBudget;

        }
        public IFluentBudget DeleteBudget(Budget deleteObject)
        {
            unitOfWork.budgetRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBudget;
        }
        public IFluentBudget DeleteBudgets(List<Budget> deleteObjects)
        {
            unitOfWork.budgetRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBudget;
        }
    }
}
