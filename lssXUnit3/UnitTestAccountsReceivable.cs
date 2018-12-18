using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.InvoiceDetailsDomain;
using ERP_Core2.CustomerLedgerDomain;
using lssWebApi2.EntityFramework;

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
        public void UnitTestLatePayment()
        {
            DateTime asOfDate = DateTime.Now;

            AccountsReceivableModule acctRecMod = new AccountsReceivableModule();

            List<AccountReceivableFlatView> list = acctRecMod.AccountsReceivable.Query().GetOpenAccountReceivables();

            foreach (var item in list)
            {
                bool status= acctRecMod.AccountsReceivable.Query().IsPaymentLate(item.InvoiceId,asOfDate);
                if (status == true)
                {
                    bool statusFee = acctRecMod.AccountsReceivable.Query().HasLateFee(item.AcctRecId);

                    if (statusFee == false)
                    {
                        acctRecMod.AccountsReceivable.CreateLateFee(item).Apply();
                        
                    }
                    acctRecMod.AccountsReceivable.AdjustOpenAmount(item).Apply();


                }
            }

        }
       [Fact]
        public void TestOpenAccountReceivables()
        {
            AccountsReceivableModule acctRecMod = new AccountsReceivableModule();
            List<AccountReceivableFlatView> list = acctRecMod.AccountsReceivable.Query().GetOpenAccountReceivables();
            Assert.True(true);
        }
        [Fact]
        public async Task TestCustomerCashPayment3()
        {
            int customerId = 2;

            UnitOfWork unitOfWork = new UnitOfWork();
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            long? addressId = await unitOfWork.customerRepository.GetAddressIdByCustomerId(customerId);
            ChartOfAccts coa = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "101", "");

            ledgerView.GeneralLedgerId = -1;
            ledgerView.DocNumber = 1;
            ledgerView.DocType = "PV";
            ledgerView.Amount = 251M;
            ledgerView.LedgerType = "AA";
            ledgerView.GLDate = DateTime.Parse("10/18/2018");
            ledgerView.AccountId = coa.AccountId;
            ledgerView.CreatedDate = DateTime.Parse("10/18/2018");
            ledgerView.AddressId = addressId ?? 0;
            ledgerView.Comment = "Second installment payment for dashboard";
            ledgerView.DebitAmount = 251;
            ledgerView.CreditAmount = 0;
            ledgerView.FiscalPeriod = 8;
            ledgerView.FiscalYear = 2018;
            ledgerView.CheckNumber = "113";


            AccountsReceivableModule acctRecMod = new AccountsReceivableModule();
            bool result=acctRecMod.CreateCustomerLedger(ledgerView);

              //bool result = await acctRec.CustomerCashPayment(ledgerView);


            Assert.True(result);
        }
        [Fact]
        public async Task TestCustomerCashPayment2()
        {
            int customerId = 2;

            UnitOfWork unitOfWork = new UnitOfWork();
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            long? addressId = await unitOfWork.customerRepository.GetAddressIdByCustomerId(customerId);
            ChartOfAccts coa = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "101", "");

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

            AccountsReceivableModule acctRecMod = new AccountsReceivableModule();
            bool result = acctRecMod.CreateCustomerLedger(ledgerView);
            Assert.True(result);
        }
        [Fact]
        public async Task TestCustomerCashPayment()
        {
            int customerId = 9;

            UnitOfWork unitOfWork = new UnitOfWork();
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            long? addressId = await unitOfWork.customerRepository.GetAddressIdByCustomerId(customerId);
            ChartOfAccts coa = await unitOfWork.chartOfAccountRepository.GetChartofAccount("1000", "1200", "101", "");

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


            AccountsReceivableModule acctRecMod = new AccountsReceivableModule();

            bool result = acctRecMod.CreateCustomerCashPayment(ledgerView);
          

            Assert.True(true);
        }
              
    
    }
}
