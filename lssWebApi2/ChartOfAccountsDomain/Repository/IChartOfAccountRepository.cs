using ERP_Core2.ChartOfAccountsDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain.Repository
{
    public interface IChartOfAccountRepository
    {
        Task<ChartOfAccts> GetChartOfAccountById(long? accountId);
        List<ChartOfAccountView> GetChartOfAccountViewsByAccount(string companyNumber, string busUnit, string objectNumber, string subsidiary);
        List<ChartOfAccountView> GetChartOfAccountsByIds(long[] accountIds);
        void ProcessAccount(string json);
        bool CreateAssets();
        bool CreateAccountReceivable();
        bool CreateSupplies();
        bool CreateEquipmentDepreciation();
        bool CreateLand();
        bool CreateBuildingDepreciation();
        bool CreateNotesPayable();
        bool CreateInterestPayment();
        bool CreateMortgageLoanPayable();
        bool CreateExpensesPersonal();
        bool CreateExpensesSalary();
        bool CreateExpensesWage();
        bool CreateExpensesRent();
        bool CreateExpensesCommunication();
        bool CreateExpensesDepreciation();
        bool CreateExpensesGainOnSaleOfAsset();
        bool CreateRevenue();
        bool CreateCapital();

    }
}
