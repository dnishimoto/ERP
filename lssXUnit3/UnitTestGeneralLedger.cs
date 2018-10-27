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
using lssWebApi2.EntityFramework;

namespace ERP_Core2.GeneralLedgerDomain
{
    
       public class UnitTestGeneralLedger
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestGeneralLedger(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestGetLedgersByFiscalYear()
        {
            long fiscalYear = 2018;
            GeneralLedgerModule glMod = new GeneralLedgerModule();

            IEnumerable<AccountSummaryView> list = glMod.GeneralLedger.Query().GetAccountSummaryByFiscalYearViews(fiscalYear);

            decimal? amount = 0;
            foreach (var item in list)
            {
                Console.Write($"{0} {1}", item.Description, item.Amount);

                amount += item.Amount;
                foreach (var ledger in item.ledgers)
                {
                    Console.Write($"{0} {1} {2} {3}", ledger.DocType, ledger.GLDate, ledger.DebitAmount,ledger.CreditAmount);

                }
            }
            if (amount > 0)
            {
                Assert.True(true);
            }
        }
        [Fact]
        public void TestCreatePersonalExpenseAndPayment()
        {
 
            GeneralLedgerModule ledgerMod = new GeneralLedgerModule();
            //UnitOfWork unitOfWork = new UnitOfWork();
            long addressId = 1;
            //long expenseDocumentNumber = 19;
            decimal expense = 768M;
            // ChartOfAccts coa = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "502", "01");
            ChartOfAccts coa = ledgerMod.ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "502", "01");
            Udc udcLedgerType = ledgerMod.UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
            //Udc udcLedgerType = await unitOfWork.generalLedgerRepository.GetUdc("GENERALLEDGERTYPE", "AA");
            Udc udcDocType = ledgerMod.UDC.Query().GetUdc("DOCTYPE", "PV");
            //Udc udcDocType = await unitOfWork.generalLedgerRepository.GetUdc("DOCTYPE", "PV");
            //AddressBook addressBook = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
            AddressBook addressBook = ledgerMod.AddressBook.Query().GetAddressBookByAddressId(addressId);
            GeneralLedgerView glView = new GeneralLedgerView();
           

            glView.DocNumber = -1;
            glView.DocType = udcDocType.KeyCode;
            glView.AccountId = coa.AccountId;
            glView.Amount = expense*-1;
            glView.LedgerType = udcLedgerType.KeyCode;
            glView.GLDate = DateTime.Parse("9/19/2018");
            glView.CreatedDate = DateTime.Parse("9/19/2018");
            glView.AddressId = addressBook.AddressId;
            glView.Comment = "Mortgage Payment";
            glView.DebitAmount = 0;
            glView.CreditAmount = expense;
            glView.FiscalPeriod = 9;
            glView.FiscalYear = 2018;

            bool result1=ledgerMod.CreatePersonalExpense(glView);

            GeneralLedgerView glViewLookup =
             ledgerMod.GeneralLedger.Query().GetGeneralLedgerView(glView.DocNumber, glView.DocType);

            //ChartOfAccts coaCash = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "101", "");

            ChartOfAccts coaCash = ledgerMod.ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "101", "");
            GeneralLedgerView glCashView = new GeneralLedgerView();

 
            //long cashDocumentNumber = 22;
            glCashView.DocNumber = -1;
            glCashView.DocType = udcDocType.KeyCode;
            glCashView.AccountId = coaCash.AccountId;
            glCashView.Amount = expense*-1;
            glCashView.LedgerType = udcLedgerType.KeyCode;
            glCashView.GLDate = DateTime.Parse("9/20/2018");
            glCashView.CreatedDate = DateTime.Parse("9/20/2018");
            glCashView.AddressId = addressBook.AddressId;
            glCashView.Comment = "Mortgage Payment";
            glCashView.DebitAmount = 0;
            glCashView.CreditAmount = expense;
            glCashView.FiscalPeriod = 9;
            glCashView.FiscalYear = 2018;

            bool result2=ledgerMod.CreateCashPayment(glCashView);
          
           
            GeneralLedgerView glCashViewLookup =
           ledgerMod.GeneralLedger.Query().GetGeneralLedgerView(glCashView.DocNumber, glCashView.DocType);

            Assert.True(glCashViewLookup != null);
        }

        [Fact]
        public void TestCreateIncomeRevenue()
        {
            int addressId = 1;
            decimal income = 2800M;
            //long incomeDocumentNumber = 20; //manually extracted
            //UnitOfWork unitOfWork = new UnitOfWork();

            GeneralLedgerModule ledgerMod = new GeneralLedgerModule();
            //Income GL

            //ChartOfAccts coa = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "300", "");
            ChartOfAccts coa=ledgerMod.ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "300", "");
          
            //Udc udcLedgerType = await unitOfWork.generalLedgerRepository.GetUdc("GENERALLEDGERTYPE", "AA");
            Udc udcLedgerType =ledgerMod.UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
            Udc udcDocType = ledgerMod.UDC.Query().GetUdc("DOCTYPE", "PV");

            //Udc udcDocType = await unitOfWork.generalLedgerRepository.GetUdc("DOCTYPE","PV");
            //AddressBook addressBook = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
            AddressBook addressBook = ledgerMod.AddressBook.Query().GetAddressBookByAddressId(addressId);
            GeneralLedgerView glView = new GeneralLedgerView();
            glView.DocNumber = -1;
            glView.DocType = udcDocType.KeyCode;
            glView.AccountId = coa.AccountId;
            glView.Amount = income;
            glView.LedgerType = udcLedgerType.KeyCode;
            glView.GLDate = DateTime.Parse("9/20/2018");
            glView.CreatedDate = DateTime.Parse("9/20/2018");
            glView.AddressId = addressBook.AddressId;
            glView.Comment = "Week end 8/31/2018";
            glView.DebitAmount = income;
            glView.CreditAmount = 0;
            glView.FiscalPeriod = 9;
            glView.FiscalYear = 2018;

            bool result=ledgerMod.CreateIncomeAndCash(glView);

         
            Assert.True(result);
        }
     
    }
}
