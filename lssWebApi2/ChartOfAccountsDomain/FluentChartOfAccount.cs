using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{
    public class FluentChartOfAccount : AbstractErrorHandling, IFluentChartOfAccount
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        private FluentChartOfAccountQuery _query = null;

        public IFluentChartOfAccountQuery Query()
        {
            if (_query == null) { _query = new FluentChartOfAccountQuery(unitOfWork); }

            return _query as IFluentChartOfAccountQuery;
        }
        public IFluentChartOfAccount Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentChartOfAccount;
        }
        public IFluentChartOfAccount AddChartOfAccounts(List<ChartOfAccount> newObjects)
        {
            unitOfWork.chartOfAccountRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentChartOfAccount;
        }
        public IFluentChartOfAccount UpdateChartOfAccounts(List<ChartOfAccount> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.chartOfAccountRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentChartOfAccount;
        }
        public IFluentChartOfAccount AddChartOfAccount(ChartOfAccount newObject)
        {
            unitOfWork.chartOfAccountRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentChartOfAccount;
        }
        public IFluentChartOfAccount UpdateChartOfAccount(ChartOfAccount updateObject)
        {
            unitOfWork.chartOfAccountRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentChartOfAccount;

        }
        public IFluentChartOfAccount DeleteChartOfAccount(ChartOfAccount deleteObject)
        {
            unitOfWork.chartOfAccountRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentChartOfAccount;
        }
        public IFluentChartOfAccount DeleteChartOfAccounts(List<ChartOfAccount> deleteObjects)
        {
            unitOfWork.chartOfAccountRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentChartOfAccount;
        }
        public async Task<bool> CreateChartOfAccountModel()
        {
            bool status = true;
            bool result = true;

            try
            {
                status = await CreateCash(); result = result && status;
                status = await CreateAccountReceivable(); result = result && status;
                status = await CreateInventory(); result = result && status;
                status = await CreateSupplies(); result = result && status;
                status = await CreateAssets(); result = result && status;
                status = await CreateEquipment(); result = result && status;
                status = await CreateEquipmentDepreciation(); result = result && status;
                status = await CreatePrepaidInsurance(); result = result && status;
                status = await CreateLand(); result = result && status;
                status = await CreateBuilding(); result = result && status;
                status = await CreateBuildingDepreciation(); result = result && status;
                status = await CreateLiability(); result = result && status;
                status = await CreateWagesPayable(); result = result && status;
                status = await CreateNotesPayable(); result = result && status;
                status = await CreateUnearnedRevenue(); result = result && status;
                status = await CreateInterestPayment(); result = result && status;
                status = await CreateMortgageLoanPayable(); result = result && status;
                status = await CreateExpenses(); result = result && status;
                status = await CreateExpensesPersonal(); result = result && status;
                status = await CreateExpensesSalary(); result = result && status;
                status = await CreateExpensesWage(); result = result && status;
                status = await CreateExpensesRent(); result = result && status;
                status = await CreateExpensesCommunication(); result = result && status;
                status = await CreateExpensesAdvertising(); result = result && status;
                status = await CreateIncome(); result = result && status;
                status = await CreateRevenue(); result = result && status;
                status = await CreateEquityCapital(); result = result && status;
                status = await CreateCapital(); result = result && status;
                status = await CreateDrawing(); result = result && status;

                return status;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        private async Task<ChartOfAccountView> MapToView(ChartOfAccount inputObject)
        {
            Mapper mapper = new Mapper();
            ChartOfAccountView outObject = mapper.Map<ChartOfAccountView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task ProcessAccount(string json)
        {
            try
            {
                ChartOfAccount chartOfAcct = JsonConvert.DeserializeObject<ChartOfAccount>(json);

                Company company = await unitOfWork.companyRepository.GetEntityById(chartOfAcct.CompanyId);
                //chartOfAcct.Company = company;

                ChartOfAccountView view = await MapToView (chartOfAcct);

                IList<ChartOfAccount> list = await unitOfWork.chartOfAccountRepository.GetEntitiesByAccount(company.CompanyCode, view.BusUnit, view.ObjectNumber, view.Subsidiary);

                if (list.Count == 0)
                {
                    AddChartOfAccount(chartOfAcct);
                }
                else
                {

                    //foreach (var item in query)
                    for (int i = 0; i < list.Count; i++)
                    {

                        var item = list[i];
                        
                        UpdateChartOfAccount(item);
                    }
                }
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateAssets()
        {
            string json = "";
            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.100""
      ,""Description"":""Assets (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""100""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateCash()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.101""
      ,""Description"":""Cash (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""101""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

               await ProcessAccount(json);
                Apply();

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

            return true;
        }
        public async Task<bool> CreateAccountReceivable()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.120""
      ,""Description"":""Account Receivables (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""120""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateInventory()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.140""
      ,""Description"":""Inventory (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""140""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateSupplies()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.150""
      ,""Description"":""Supplies (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""150""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateEquipment()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.180""
      ,""Description"":""Equipment (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""180""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateEquipmentDepreciation()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.188""
      ,""Description"":""Equipment Depreciation (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""188""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreatePrepaidInsurance()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.160""
      ,""Description"":""Prepaid Insurance (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""160""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateLand()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.170""
      ,""Description"":""Land (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""170""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateBuilding()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.175""
      ,""Description"":""Building (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""175""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateBuildingDepreciation()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.178""
      ,""Description"":""Building Depreciation (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""178""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateLiability()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.200""
      ,""Description"":""Liabilities (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""200""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateNotesPayable()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.210""
      ,""Description"":""Notes Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""210""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateWagesPayable()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.215""
      ,""Description"":""Wages Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""215""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<bool> CreateInterestPayment()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.230""
      ,""Description"":""Interest Payment (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""230""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateUnearnedRevenue()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.240""
      ,""Description"":""Unearned Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""240""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateMortgageLoanPayable()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.250""
      ,""Description"":""Mortgage Loan Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""250""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<bool> CreateExpenses()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.500""
      ,""Description"":""Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""500""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

               await ProcessAccount(json);
              
                Apply();

                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesPersonal()
        {
            string json = "";
            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502""
      ,""Description"":""Expense Personal (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();

                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.01""
      ,""Description"":""Expense Personal (DB) Mortgage"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
      ,""Subsidiary"":""01""
,""SupCode"":""4366""
,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();

                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.02""
      ,""Description"":""Expense Personal (DB) Utilities: Gas, Electricity, Sewer, Trash, Water"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""02""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();

                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.03""
      ,""Description"":""Expense Personal (DB) Car Payment"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""03""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.04""
      ,""Description"":""Expense Personal (DB) Food"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""04""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.05""
      ,""Description"":""Expense Personal (DB) Tithing"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""05""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.06""
      ,""Description"":""Expense Personal (DB) Medical"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""06""
,""SupervisorCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.07""
      ,""Description"":""Expense Personal (DB) Missionary Fund"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""07""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();

                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.08""
      ,""Description"":""Expense Personal (DB) Entertainment and Travel"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""08""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesSalary()
        {
            string json = "";
            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.501""
      ,""Description"":""Salary Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""501""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesWage()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.510""
      ,""Description"":""Wage Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""510""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesSupply()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.540""
      ,""Description"":""Supply Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""540""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesRent()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.560""
      ,""Description"":""Rent Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""560""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

               await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesUtilities()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.570""
      ,""Description"":""Utilities Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""570""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesCommunication()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.576""
      ,""Description"":""Communication Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""576""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesAdvertising()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.610""
      ,""Description"":""Advertising Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""610""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesDepreciation()
        {
            string json = "";
            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.750""
      ,""Description"":""Advertising Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""750""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesInterestRevenue()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.810""
      ,""Description"":""Interest Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""810""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateExpensesGainOnSaleOfAsset()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.900""
      ,""Description"":""Gain On Sale of Asset (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""900""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateIncome()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.300""
      ,""Description"":""Income (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""300""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateRevenue()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.310""
      ,""Description"":""Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""310""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateEquityCapital()
        {
            string json = "";
            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.200""
      ,""Description"":""Equity Capital (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""200""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateCapital()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.250""
      ,""Description"":""Capital (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""250""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> CreateDrawing()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.295""
      ,""Description"":""Drawing (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""295""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                await ProcessAccount(json);
                Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
