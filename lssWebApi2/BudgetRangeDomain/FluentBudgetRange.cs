using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.BudgetDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetRangeDomain
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
        private BudgetRange MapToEntity(BudgetRangeView inputObject)
        {
            Mapper mapper = new Mapper();
            BudgetRange outObject = mapper.Map<BudgetRange>(inputObject);
            return outObject;
        }
        public IFluentBudgetRange CreateBudgetRange(BudgetRangeView budgetRangeView)
        {
            try
            {
                Task<BudgetRange> budgetRangeLookup = Task.Run(async () => await
                 unitOfWork.budgetRangeRepository.FindEntityByExpression(

                     e =>
                     e.AccountId == budgetRangeView.AccountId
                                   && e.StartDate == budgetRangeView.StartDate
                                   && e.EndDate == budgetRangeView.EndDate
                     ));
                Task.WaitAll(budgetRangeLookup);
               
                if (budgetRangeLookup.Result == null)
                {
                   
                    AddBudgetRange(MapToEntity(budgetRangeView));
                    return this as IFluentBudgetRange;
                }
                processStatus = CreateProcessStatus.AlreadyExists;
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
        public IFluentBudgetRange AddBudgetRanges(List<BudgetRange> newObjects)
        {
            unitOfWork.budgetRangeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBudgetRange;
        }
        public IFluentBudgetRange UpdateBudgetRanges(List<BudgetRange> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.budgetRangeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBudgetRange;
        }
        public IFluentBudgetRange AddBudgetRange(BudgetRange newObject)
        {
            unitOfWork.budgetRangeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBudgetRange;
        }
        public IFluentBudgetRange UpdateBudgetRange(BudgetRange updateObject)
        {
            unitOfWork.budgetRangeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBudgetRange;

        }
        public IFluentBudgetRange DeleteBudgetRange(BudgetRange deleteObject)
        {
            unitOfWork.budgetRangeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBudgetRange;
        }
        public IFluentBudgetRange DeleteBudgetRanges(List<BudgetRange> deleteObjects)
        {
            unitOfWork.budgetRangeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBudgetRange;
        }
    }

}

