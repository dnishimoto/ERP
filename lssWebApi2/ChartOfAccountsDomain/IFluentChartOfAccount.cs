using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{
    public interface IFluentChartOfAccount
    {
        Task<bool> CreateChartOfAccountModel();
        IFluentChartOfAccountQuery Query();
        IFluentChartOfAccount Apply();

        Task ProcessAccount(string json);
        Task<bool> CreateAssets();
        Task<bool> CreateAccountReceivable();
        Task<bool> CreateSupplies();
        Task<bool> CreateEquipmentDepreciation();
        Task<bool> CreateLand();
        Task<bool> CreateBuildingDepreciation();
        Task<bool> CreateNotesPayable();
        Task<bool> CreateInterestPayment();
        Task<bool> CreateMortgageLoanPayable();
        Task<bool> CreateExpensesPersonal();
        Task<bool> CreateExpensesSalary();
        Task<bool> CreateExpensesWage();
        Task<bool> CreateExpensesRent();
        Task<bool> CreateExpensesCommunication();
        Task<bool> CreateExpensesDepreciation();
        Task<bool> CreateExpensesGainOnSaleOfAsset();
        Task<bool> CreateRevenue();
        Task<bool> CreateCapital();
        IFluentChartOfAccount AddChartOfAccounts(List<ChartOfAccount> newObjects);
        IFluentChartOfAccount UpdateChartOfAccounts(List<ChartOfAccount> newObjects);
        IFluentChartOfAccount AddChartOfAccount(ChartOfAccount newObject);
        IFluentChartOfAccount UpdateChartOfAccount(ChartOfAccount updateObject);
        IFluentChartOfAccount DeleteChartOfAccount(ChartOfAccount deleteObject);
        IFluentChartOfAccount DeleteChartOfAccounts(List<ChartOfAccount> deleteObjects);
 
    }
}
