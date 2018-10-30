using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.ChartOfAccountsDomain
{
    public class ChartOfAccountView
    {
        public ChartOfAccountView() { }
        public ChartOfAccountView(ChartOfAccts chartOfAcct)
        {
            this.AccountId = chartOfAcct.AccountId;
            this.Location = chartOfAcct.Location;
            this.BusUnit = chartOfAcct.BusUnit;
            this.Subsidiary = chartOfAcct.Subsidiary;
            this.SubSub = chartOfAcct.SubSub;
            this.Account = chartOfAcct.Account;
            this.Description = chartOfAcct.Description;
            this.CompanyNumber = chartOfAcct.CompanyNumber;
            this.GenCode = chartOfAcct.GenCode;
            this.SubCode = chartOfAcct.SubCode;
            this.ObjectNumber = chartOfAcct.ObjectNumber;
            this.SupCode = chartOfAcct.SupCode;
            this.ThirdAccount = chartOfAcct.ThirdAccount;
            this.CategoryCode1 = chartOfAcct.CategoryCode1;
            this.CategoryCode2 = chartOfAcct.CategoryCode2;
            this.CategoryCode3 = chartOfAcct.CategoryCode3;
            this.PostEditCode = chartOfAcct.PostEditCode;
            this.CompanyId = chartOfAcct.CompanyId;
            this.CompanyName = chartOfAcct.Company?.CompanyName;
            this.Level = chartOfAcct.Level;
        }
        public long AccountId { get; set; }
        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string Subsidiary { get; set; }
        public string SubSub { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CompanyNumber { get; set; }
        public string GenCode { get; set; }
        public string SubCode { get; set; }
        public string ObjectNumber { get; set; }
        public string SupCode { get; set; }
        public string ThirdAccount { get; set; }
        public string CategoryCode1 { get; set; }
        public string CategoryCode2 { get; set; }
        public string CategoryCode3 { get; set; }
        public string PostEditCode { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int Level { get; set; }
    }
    public class ChartOfAccountRepository : Repository<ChartOfAccts>
    {
        private ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public ChartOfAccountRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public List<ChartOfAccountView> GetChartOfAccountViewsByAccount(string companyNumber, string busUnit, string objectNumber, string subsidiary)
        {
            try
            {

                IQueryable<ChartOfAccts> query = _dbContext.ChartOfAccts;

                if (companyNumber != "")
                {
                    query = query.Where(e => e.CompanyNumber == companyNumber);
                }
                if (busUnit != "")
                {
                    query = query.Where(e => e.BusUnit == busUnit);
                }
                if (objectNumber != "")
                {
                    query = query.Where(e => e.ObjectNumber == objectNumber);
                }
                if (subsidiary != "")
                {
                    query = query.Where(e => e.Subsidiary == subsidiary);
                }
                List<ChartOfAccountView> list = new List<ChartOfAccountView>();
                foreach (var item in query)
                {
                    list.Add
                        (
                          applicationViewFactory.MapChartOfAccountView(item)
                        );

                }
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public List<ChartOfAccountView> GetChartOfAccountsByIds(long[] accountIds)
        {
            try
            {
                List<ChartOfAccountView> list = (from coa in _dbContext.ChartOfAccts
                                                 where accountIds.Contains(coa.AccountId)
                                                 select new ChartOfAccountView
                                                 {
                                                     AccountId = coa.AccountId,
                                                     Location = coa.Location,
                                                     BusUnit = coa.BusUnit,
                                                     Subsidiary = coa.Subsidiary,
                                                     SubSub = coa.SubSub,
                                                     Account = coa.Account,
                                                     Description = coa.Description,
                                                     CompanyNumber = coa.CompanyNumber,
                                                     GenCode = coa.GenCode,
                                                     SubCode = coa.SubCode,
                                                     ObjectNumber = coa.ObjectNumber,
                                                     SupCode = coa.SubCode,
                                                     ThirdAccount = coa.ThirdAccount,
                                                     CategoryCode1 = coa.CategoryCode1,
                                                     CategoryCode2 = coa.CategoryCode2,
                                                     CategoryCode3 = coa.CategoryCode3,
                                                     PostEditCode = coa.PostEditCode,
                                                     CompanyId = coa.CompanyId,
                                                     CompanyName = coa.Company.CompanyName,
                                                     Level = coa.Level
                                                 }).ToList<ChartOfAccountView>();

                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public void ProcessAccount(string json)
        {
            try
            {
                ChartOfAccts chartOfAcct = JsonConvert.DeserializeObject<ChartOfAccts>(json);
                Task<Company> companyTask = (from e in _dbContext.Company
                                             where e.CompanyId == chartOfAcct.CompanyId
                                             select e).FirstOrDefaultAsync<Company>();
                chartOfAcct.Company = companyTask.Result;
                ChartOfAccountView view = applicationViewFactory.MapChartOfAccountView(chartOfAcct);
                IQueryable<ChartOfAccts> query = GetObjectsQueryable(e => e.BusUnit==view.BusUnit && e.ObjectNumber == view.ObjectNumber && e.Subsidiary==view.Subsidiary, "");
                List<ChartOfAccts> list = query.ToList<ChartOfAccts>();
                if (list.Count == 0)
                {
                    AddObject(chartOfAcct);
                }
                else
                {

                    //foreach (var item in query)
                    for (int i = 0; i < list.Count; i++)
                    {

                        var item = list[i];
                        applicationViewFactory.MapChartOfAccountEntity(ref item, view);

                        UpdateObject(item);
                    }
                }
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateAssets()
        {
            string json = "";
            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.100""
      ,""Description"":""Assets (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""100""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateCash()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.101""
      ,""Description"":""Cash (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""101""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

            return true;
        }
        public bool CreateAccountReceivable()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.120""
      ,""Description"":""Account Receivables (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""120""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateInventory()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.140""
      ,""Description"":""Inventory (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""140""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateSupplies()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.150""
      ,""Description"":""Supplies (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""150""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateEquipment()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.180""
      ,""Description"":""Equipment (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""180""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateEquipmentDepreciation()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.188""
      ,""Description"":""Equipment Depreciation (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""188""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreatePrepaidInsurance()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.160""
      ,""Description"":""Prepaid Insurance (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""160""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateLand()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.170""
      ,""Description"":""Land (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""170""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateBuilding()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.175""
      ,""Description"":""Building (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""175""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateBuildingDepreciation()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.178""
      ,""Description"":""Building Depreciation (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""178""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateLiability()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.200""
      ,""Description"":""Liabilities (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""200""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateNotesPayable()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.210""
      ,""Description"":""Notes Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""210""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateWagesPayable()
        {
            string json = "";

            try
            {
                json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.215""
      ,""Description"":""Wages Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""215""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

                ProcessAccount(json);
                return true;

              }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex);
    }
}
        public bool CreateInterestPayment()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.230""
      ,""Description"":""Interest Payment (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""230""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateUnearnedRevenue()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.240""
      ,""Description"":""Unearned Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""240""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateMortgageLoanPayable()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.250""
      ,""Description"":""Mortgage Loan Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""250""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public bool CreateExpenses()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.500""
      ,""Description"":""Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""500""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);

            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesPersonal()
        {
            string json = "";
            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502""
      ,""Description"":""Expense Personal (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.01""
      ,""Description"":""Expense Personal (DB) Mortgage"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
      ,""Subsidiary"":""01""
,""SupCode"":""4366""
,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.02""
      ,""Description"":""Expense Personal (DB) Utilities: Gas, Electricity, Sewer, Trash, Water"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""02""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.03""
      ,""Description"":""Expense Personal (DB) Car Payment"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""03""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.04""
      ,""Description"":""Expense Personal (DB) Food"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""04""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.05""
      ,""Description"":""Expense Personal (DB) Tithing"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""05""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.06""
      ,""Description"":""Expense Personal (DB) Medical"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""06""
,""SupervisorCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.07""
      ,""Description"":""Expense Personal (DB) Missionary Fund"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""07""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.502.08""
      ,""Description"":""Expense Personal (DB) Entertainment and Travel"",""CompanyNumber"":""1000"",""ObjectNumber"":""502""
,""Subsidiary"":""08""
,""SupCode"":""4366""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";
            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesSalary()
        {
            string json = "";
            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.501""
      ,""Description"":""Salary Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""501""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesWage()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.510""
      ,""Description"":""Wage Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""510""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesSupply()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.540""
      ,""Description"":""Supply Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""540""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesRent()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.560""
      ,""Description"":""Rent Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""560""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesUtilities()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.570""
      ,""Description"":""Utilities Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""570""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesCommunication()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.576""
      ,""Description"":""Communication Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""576""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesAdvertising()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.610""
      ,""Description"":""Advertising Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""610""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesDepreciation()
        {
            string json = "";
            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.750""
      ,""Description"":""Advertising Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""750""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesInterestRevenue()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.810""
      ,""Description"":""Interest Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""810""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateExpensesGainOnSaleOfAsset()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.900""
      ,""Description"":""Gain On Sale of Asset (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""900""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateIncome()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.300""
      ,""Description"":""Income (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""300""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateRevenue()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.310""
      ,""Description"":""Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""310""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateEquityCapital()
        {
            string json = "";
            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.200""
      ,""Description"":""Equity Capital (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""200""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateCapital()
        {
            string json = "";

            try
            { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.250""
      ,""Description"":""Capital (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""250""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public bool CreateDrawing()
        {
            string json = "";

            try { 
            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.295""
      ,""Description"":""Drawing (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""295""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
