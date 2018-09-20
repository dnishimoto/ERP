using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

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
        public void TestCreatePersonalExpense()
        {
            //ToDo
        }
   
        [Fact]
        public async Task TestCreateIncomeRevenue()
        {
            int addressId = 1;
            decimal income = 2800M;
            long incomeDocumentNumber = 20; //manually extracted
            UnitOfWork unitOfWork = new UnitOfWork();

            GeneralLedgerModule ledgerMod = new GeneralLedgerModule();

            ChartOfAcct coa = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "300", "");
            UDC udcLedgerType = await unitOfWork.generalLedgerRepository.GetUdc("GENERALLEDGERTYPE", "AA");
            UDC udcDocType = await unitOfWork.generalLedgerRepository.GetUdc("DOCTYPE","PV");
            AddressBook addressBook = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
            GeneralLedgerView glView = new GeneralLedgerView();
            glView.DocNumber = incomeDocumentNumber;
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

            ledgerMod.GeneralLedger.CreateGeneralLedger(glView).Apply();
            ledgerMod.GeneralLedger.UpdateAccountBalances(glView);

            ChartOfAcct coa2 = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "101", "");

            long cashDocumentNumber = 21;
            GeneralLedgerView glView2 = new GeneralLedgerView();
            glView2.DocNumber = cashDocumentNumber;
            glView2.DocType = udcDocType.KeyCode;
            glView2.AccountId = coa2.AccountId;
            glView2.Amount = income;
            glView2.LedgerType = udcLedgerType.KeyCode;
            glView2.GLDate = DateTime.Parse("9/20/2018");
            glView2.CreatedDate = DateTime.Parse("9/20/2018");
            glView2.AddressId = addressBook.AddressId;
            glView2.Comment = "Week end 8/31/2018";
            glView2.DebitAmount = income;
            glView2.CreditAmount = 0;
            glView2.FiscalPeriod = 9;
            glView2.FiscalYear = 2018;

            ledgerMod.GeneralLedger.CreateGeneralLedger(glView2).Apply();

            ledgerMod.GeneralLedger.UpdateAccountBalances(glView2);


            GeneralLedgerView glViewLookup =
                ledgerMod.GeneralLedger.Query().GetGeneralLedgerView(glView2.DocNumber,glView2.DocType);
            Assert.True(glViewLookup!=null);
        }
     
    }
}
