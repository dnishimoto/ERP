using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetRangeDomain
{
    public class FluentBudgetRangeQuery : MapperAbstract<BudgetRange,BudgetRangeView>, IFluentBudgetRangeQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentBudgetRangeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<BudgetRangeView> GetBudgetRange(long? accountId, DateTime? startDate, DateTime? endDate)
        {
            BudgetRange budgetRange = await _unitOfWork.budgetRangeRepository.GetBudgetRange(accountId, startDate, endDate);

            return await MapToView(budgetRange);

        }
        public override async Task<BudgetRange> MapToEntity(BudgetRangeView inputObject)
        {
            BudgetRange outObject = mapper.Map<BudgetRange>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<BudgetRange>> MapToEntity(List<BudgetRangeView> inputObjects)
        {
            List<BudgetRange> list = new List<BudgetRange>();
            foreach (var item in inputObjects)
            {
                BudgetRange outObject = mapper.Map<BudgetRange>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<BudgetRangeView> MapToView(BudgetRange inputObject)
        {
            BudgetRangeView outObject = mapper.Map<BudgetRangeView>(inputObject);

            Task<ChartOfAccount> accountTask =  _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);
       
            Task.WaitAll(accountTask);
         
            outObject.AccountDescription = accountTask.Result.Description;
            outObject.CompanyCode = accountTask.Result.CompanyCode;
            outObject.BusUnit = accountTask.Result.BusUnit;
            outObject.ObjectNumber = accountTask.Result.ObjectNumber;
            outObject.Subsidiary = accountTask.Result.Subsidiary;

            await Task.Yield();

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.budgetRangeRepository.GetNextNumber(TypeOfBudgetRange.BudgetRangeNumber.ToString());
        }
        public override async Task<BudgetRangeView> GetViewById(long ? budgetRangeId)
        {
            BudgetRange detailItem = await _unitOfWork.budgetRangeRepository.GetEntityById(budgetRangeId);

            return await MapToView(detailItem);
        }
        public async Task<BudgetRangeView> GetViewByNumber(long budgetRangeNumber)
        {
            BudgetRange detailItem = await _unitOfWork.budgetRangeRepository.GetEntityByNumber(budgetRangeNumber);

            return await MapToView(detailItem);
        }

        public override async Task<BudgetRange> GetEntityById(long ? budgetRangeId)
        {
            return await _unitOfWork.budgetRangeRepository.GetEntityById(budgetRangeId);

        }
        public async Task<BudgetRange> GetEntityByNumber(long budgetRangeNumber)
        {
            return await _unitOfWork.budgetRangeRepository.GetEntityByNumber(budgetRangeNumber);
        }
    }
}
