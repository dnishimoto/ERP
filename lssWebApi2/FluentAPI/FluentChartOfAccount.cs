using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.ChartOfAccountsDomain;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentChartOfAccount : AbstractErrorHandling, IChartOfAccount
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        private FluentChartOfAccountQuery _query = null;

        public IChartOfAccountQuery Query()
        {
            if (_query == null) { _query = new FluentChartOfAccountQuery(unitOfWork); }

            return _query as IChartOfAccountQuery;
        }
        public IChartOfAccount Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IChartOfAccount;
        }
        public IChartOfAccount CreateChartOfAccountModel()
        {
            bool status = true;
            bool result = true;

            try
            {
                status = unitOfWork.chartOfAccountRepository.CreateCash(); result = result && status;

                status = unitOfWork.chartOfAccountRepository.CreateAccountReceivable(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateInventory(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateSupplies(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateAssets(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateEquipment(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateEquipmentDepreciation(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreatePrepaidInsurance(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateLand(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateBuilding(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateBuildingDepreciation(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateLiability(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateWagesPayable(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateNotesPayable(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateUnearnedRevenue(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateInterestPayment(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateMortgageLoanPayable(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpenses(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpensesPersonal(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpensesSalary(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpensesWage(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpensesRent(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpensesCommunication(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateExpensesAdvertising(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateIncome(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateRevenue(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateEquityCapital(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateCapital(); result = result && status;
                status = unitOfWork.chartOfAccountRepository.CreateDrawing(); result = result && status;

                if (result == true)
                {
                    //either Insert or Update
                    this.processStatus = CreateProcessStatus.Insert;
                }
                return this as IChartOfAccount;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
