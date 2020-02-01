using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.BudgetDomain;
using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetDomain
{
    public class FluentBudgetQuery : MapperAbstract<Budget,BudgetView>, IFluentBudgetQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentBudgetQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<IList<PersonalBudgetView>> GetPersonalBudgetViews()
        {
       
            IList<PersonalBudgetView> views  = await _unitOfWork.budgetRepository.GetPersonalBudgetViews();
            return views;

        }

        public async Task<BudgetActualsView> GetBudgetActualsView(BudgetRangeView budgetRangeView)
        {
            Udc udcActuals = await _unitOfWork.udcRepository.GetUdc("GENERALLEDGERTYPE", "AA");
            Udc udcHours = await _unitOfWork.udcRepository.GetUdc("GENERALLEDGERTYPE", "HA");

            BudgetActualsView budgetActualsView =  await _unitOfWork.budgetRepository.GetActualsView(budgetRangeView,udcActuals.KeyCode,udcHours.KeyCode);
            return budgetActualsView;
        }
        public async Task<BudgetView> GetBudgetView(long budgetId)
        {
            Budget budget =  await _unitOfWork.budgetRepository.GetEntityById(budgetId);
            return await MapToView(budget);

        }
        public async Task<IList<BudgetView>> GetBudgetViews()
        {
            List<Budget> budgets = ( await _unitOfWork.budgetRepository.GetBudgets()).ToList<Budget>();
            IList<BudgetView> views = new List<BudgetView>();
            budgets.ForEach(async e => views.Add(await MapToView(e)));
            return views;
        }
        public override async Task<Budget> MapToEntity(BudgetView inputObject)
        {
            Budget outObject = mapper.Map<Budget>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<Budget>> MapToEntity(IList<BudgetView> inputObjects)
        {
            IList<Budget> list = new List<Budget>();
            foreach (var item in inputObjects)
            {
                Budget outObject = mapper.Map<Budget>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<BudgetView> MapToView(Budget inputObject)
        {
            BudgetView outObject = mapper.Map<BudgetView>(inputObject);

            Task<ChartOfAccount> chartOfAccountTask = _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);
            Task<BudgetRange> budgetRangeTask =  _unitOfWork.budgetRangeRepository.GetEntityById(inputObject.RangeId);
            Task.WaitAll(chartOfAccountTask, budgetRangeTask);

            outObject.AccountDescription = chartOfAccountTask.Result.Description;
            outObject.CompanyCode = chartOfAccountTask.Result.CompanyCode;
            outObject.BusUnit = chartOfAccountTask.Result.BusUnit;
            outObject.ObjectNumber = chartOfAccountTask.Result.ObjectNumber;
            outObject.Subsidiary = chartOfAccountTask.Result.Subsidiary;
            outObject.RangeIsActive = budgetRangeTask.Result.IsActive;
            outObject.RangeStartDate = budgetRangeTask.Result.StartDate;
            outObject.RangeEndDate = budgetRangeTask.Result.EndDate;
            outObject.SupervisorCode = budgetRangeTask.Result.SupervisorCode;

            await Task.Yield();

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfBudget.BudgetNumber.ToString());
        }
        public override async Task<BudgetView> GetViewById(long  ?  budgetId)
        {
            Budget detailItem = await _unitOfWork.budgetRepository.GetEntityById(budgetId);

            return await MapToView(detailItem);
        }
        public async Task<BudgetView> GetViewByNumber(long budgetNumber)
        {
            Budget detailItem = await _unitOfWork.budgetRepository.GetEntityByNumber(budgetNumber);

            return await MapToView(detailItem);
        }

        public override async Task<Budget> GetEntityById(long ? budgetId)
        {
            return await _unitOfWork.budgetRepository.GetEntityById(budgetId);

        }
        public async Task<Budget> GetEntityByNumber(long budgetNumber)
        {
            return await _unitOfWork.budgetRepository.GetEntityByNumber(budgetNumber);
        }
    }
}
