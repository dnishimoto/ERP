using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using MillenniumERP.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ERP_Core2.ChartOfAccountsDomain
{
    
       public class UnitTestChartOfAccounts
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestChartOfAccounts(ITestOutputHelper output)
        {
            this.output = output;

        }
        
            
        [Fact]
        public void TestCreateAccountModel()
        {
            bool status = false;
            UnitOfWork unitOfWork = new UnitOfWork();


            status = unitOfWork.chartOfAccountRepository.CreateCash();
            status = unitOfWork.chartOfAccountRepository.CreateAccountReceivable();
            status = unitOfWork.chartOfAccountRepository.CreateInventory();
            status = unitOfWork.chartOfAccountRepository.CreateSupplies();
            status = unitOfWork.chartOfAccountRepository.CreateAssets();
            status = unitOfWork.chartOfAccountRepository.CreateEquipment();
            status = unitOfWork.chartOfAccountRepository.CreateEquipmentDepreciation();
            status = unitOfWork.chartOfAccountRepository.CreatePrepaidInsurance();
            status = unitOfWork.chartOfAccountRepository.CreateLand();
            status = unitOfWork.chartOfAccountRepository.CreateBuilding();
            status = unitOfWork.chartOfAccountRepository.CreateBuildingDepreciation();
            status = unitOfWork.chartOfAccountRepository.CreateLiability();
            status = unitOfWork.chartOfAccountRepository.CreateWagesPayable();
            status = unitOfWork.chartOfAccountRepository.CreateNotesPayable();
            status = unitOfWork.chartOfAccountRepository.CreateUnearnedRevenue();
            status = unitOfWork.chartOfAccountRepository.CreateInterestPayment();
            status = unitOfWork.chartOfAccountRepository.CreateMortgageLoanPayable();
            status = unitOfWork.chartOfAccountRepository.CreateExpenses();
            status = unitOfWork.chartOfAccountRepository.CreateExpensesSalary();
            status = unitOfWork.chartOfAccountRepository.CreateExpensesWage();
            status = unitOfWork.chartOfAccountRepository.CreateExpensesRent();
            status = unitOfWork.chartOfAccountRepository.CreateExpensesCommunication();
            status = unitOfWork.chartOfAccountRepository.CreateExpensesAdvertising();
            unitOfWork.CommitChanges();
            Assert.True(true);
        }
     
    }
}
