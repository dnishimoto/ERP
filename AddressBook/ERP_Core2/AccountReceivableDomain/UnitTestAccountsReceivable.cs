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
using System;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.InvoiceDetailsDomain;
using MillenniumERP.CustomerLedgerDomain;

namespace ERP_Core2.AccountsReceivableDomain
{
    
       public class UnitTestAccountsReceivable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestAccountsReceivable(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task TestCustomerCashPayment2()
        {
            int customerId = 2;

            UnitOfWork unitOfWork = new UnitOfWork();
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            long? addressId = await unitOfWork.customerRepository.GetAddressIdByCustomerId(customerId);
            ChartOfAcct coa = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "101", "");

            ledgerView.GeneralLedgerId = -1;
            ledgerView.DocNumber = 1;
            ledgerView.DocType = "PV";
            ledgerView.Amount = 250M;
            ledgerView.LedgerType = "AA";
            ledgerView.GLDate = DateTime.Parse("8/10/2018");
            ledgerView.AccountId = coa.AccountId;
            ledgerView.CreatedDate = DateTime.Parse("8/10/2018");
            ledgerView.AddressId = addressId ?? 0;
            ledgerView.Comment = "First installment payment for dashboard";
            ledgerView.DebitAmount = 250;
            ledgerView.CreditAmount = 0;
            ledgerView.FiscalPeriod = 8;
            ledgerView.FiscalYear = 2018;
            ledgerView.CheckNumber = "112";


            AccountsReceivable acctRec = new AccountsReceivable();

            bool result = await acctRec.CustomerCashPayment(ledgerView);


            Assert.True(result);
        }
        [Fact]
        public async Task TestCustomerCashPayment()
        {
            int customerId = 9;

            UnitOfWork unitOfWork = new UnitOfWork();
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            long? addressId = await unitOfWork.customerRepository.GetAddressIdByCustomerId(customerId);
            ChartOfAcct coa = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "101", "");

            ledgerView.GeneralLedgerId = -1;
            ledgerView.DocNumber = 12;
            ledgerView.DocType = "PV";
            ledgerView.Amount = 189.63M;
            ledgerView.LedgerType = "AA";
            ledgerView.GLDate = DateTime.Parse("7/21/2018");
            ledgerView.AccountId = coa.AccountId;
            ledgerView.CreatedDate = DateTime.Parse("7/21/2018");
            ledgerView.AddressId = addressId ?? 0;
            ledgerView.Comment = "Payment in Part for 50% sharing of project income";
            ledgerView.DebitAmount = 189.63M;
            ledgerView.CreditAmount = 0;
            ledgerView.FiscalPeriod = 7;
            ledgerView.FiscalYear = 2018;
            ledgerView.CheckNumber = "111";


            AccountsReceivable acctRec = new AccountsReceivable();

            bool result = await acctRec.CustomerCashPayment(ledgerView);


            Assert.True(result);
        }
              
    
    }
}
