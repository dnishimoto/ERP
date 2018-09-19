using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using ERP_Core2.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.ChartOfAccountsDomain
{
    public class ChartOfAccountView
    {
        public ChartOfAccountView() { }
        public ChartOfAccountView(ChartOfAcct chartOfAcct)
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
    public class ChartOfAccountRepository : Repository<ChartOfAcct>
    {
        private Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public ChartOfAccountRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
      
        public void ProcessAccount(string json)
        {
            try
            {
                ChartOfAcct chartOfAcct = JsonConvert.DeserializeObject<ChartOfAcct>(json);
                Task<Company> companyTask = (from e in _dbContext.Companies
                                             where e.CompanyId == chartOfAcct.CompanyId
                                             select e).FirstOrDefaultAsync<Company>();
                chartOfAcct.Company = companyTask.Result;
                ChartOfAccountView view = applicationViewFactory.MapChartOfAccountView(chartOfAcct);
                IQueryable<ChartOfAcct> query = GetObjectsQueryable(e => e.ObjectNumber == view.ObjectNumber, "");
                List<ChartOfAcct> list = query.ToList<ChartOfAcct>();
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

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.100""
      ,""Description"":""Assets (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""100""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);



            return true;
        }
        public bool CreateCash()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.101""
      ,""Description"":""Cash (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""101""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);



            return true;
        }
        public bool CreateAccountReceivable()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.120""
      ,""Description"":""Account Receivables (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""120""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateInventory()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.140""
      ,""Description"":""Inventory (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""140""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateSupplies()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.150""
      ,""Description"":""Supplies (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""150""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateEquipment()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.180""
      ,""Description"":""Equipment (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""180""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateEquipmentDepreciation()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.188""
      ,""Description"":""Equipment Depreciation (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""188""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreatePrepaidInsurance()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.160""
      ,""Description"":""Prepaid Insurance (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""160""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateLand()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.170""
      ,""Description"":""Land (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""170""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateBuilding()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.175""
      ,""Description"":""Building (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""175""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateBuildingDepreciation()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.178""
      ,""Description"":""Building Depreciation (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""178""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateLiability()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.200""
      ,""Description"":""Liabilities (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""200""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateNotesPayable()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.210""
      ,""Description"":""Notes Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""210""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateWagesPayable()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.215""
      ,""Description"":""Wages Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""215""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateInterestPayment()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.230""
      ,""Description"":""Interest Payment (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""230""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateUnearnedRevenue()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.240""
      ,""Description"":""Unearned Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""240""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateMortgageLoanPayable()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.250""
      ,""Description"":""Mortgage Loan Payable (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""250""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }

        public bool CreateExpenses()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.500""
      ,""Description"":""Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""500""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);

            return true;
        }
        public bool CreateExpensesPersonal()
        {
            string json = "";
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
        public bool CreateExpensesSalary()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.501""
      ,""Description"":""Salary Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""501""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesWage()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.510""
      ,""Description"":""Wage Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""510""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesSupply()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.540""
      ,""Description"":""Supply Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""540""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesRent()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.560""
      ,""Description"":""Rent Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""560""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesUtilities()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.570""
      ,""Description"":""Utilities Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""570""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesCommunication()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.576""
      ,""Description"":""Communication Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""576""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesAdvertising()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.610""
      ,""Description"":""Advertising Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""610""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesDepreciation()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.750""
      ,""Description"":""Advertising Expenses (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""750""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesInterestRevenue()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.810""
      ,""Description"":""Interest Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""810""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateExpensesGainOnSaleOfAsset()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.900""
      ,""Description"":""Gain On Sale of Asset (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""900""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateIncome()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.300""
      ,""Description"":""Income (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""300""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateRevenue()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.310""
      ,""Description"":""Revenue (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""310""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateEquityCapital()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.200""
      ,""Description"":""Equity Capital (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""200""
       ,""CompanyId"":1,""Level"":1,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateCapital()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.250""
      ,""Description"":""Capital (CR)"",""CompanyNumber"":""1000"",""ObjectNumber"":""250""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
        public bool CreateDrawing()
        {
            string json = "";

            json = @"{ ""Location"":""01"" ,""BusUnit"":""1200"",""Account"":""1200.295""
      ,""Description"":""Drawing (DB)"",""CompanyNumber"":""1000"",""ObjectNumber"":""295""
       ,""CompanyId"":1,""Level"":2,""PostEditCode"":""P""}";

            ProcessAccount(json);
            return true;
        }
    }
}
