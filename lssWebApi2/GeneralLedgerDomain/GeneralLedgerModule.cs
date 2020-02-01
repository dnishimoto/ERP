using lssWebApi2.UDCDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.NextNumberDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.Enumerations;

namespace lssWebApi2.GeneralLedgerDomain
{

    public class GeneralLedgerModule
    {
        private UnitOfWork unitOfWork;
        public FluentGeneralLedger GeneralLedger;
        public FluentChartOfAccount ChartOfAccounts;
        public FluentNextNumber nn;
        public FluentAddressBook AddressBook;
        public FluentUdc UDC;

        public GeneralLedgerModule()
        {
            unitOfWork = new UnitOfWork();
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            ChartOfAccounts = new FluentChartOfAccount(unitOfWork);
            nn = new FluentNextNumber(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            UDC = new FluentUdc(unitOfWork);
        }

        public async Task<bool> CreateIncomeAndCash(GeneralLedgerView glView)
        {
            bool results = false;
            results = await CreateCash(glView);
            results = results && await CreateIncome(glView);
            return results;
        }

        public async Task<bool> CreateIncome(GeneralLedgerView glView)
        {
            try
            {
                //ChartOfAccts coa = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "300", "");
                ChartOfAccount coa = await ChartOfAccounts.Query().GetEntity("1000", "1200", "300", "");

                //Udc udcLedgerType = await unitOfWork.generalLedgerRepository.GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcLedgerType = await UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcDocType = await UDC.Query().GetUdc("DOCTYPE", "PV");

                //Udc udcDocType = await unitOfWork.generalLedgerRepository.GetUdc("DOCTYPE","PV");
                //AddressBook addressBook = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
                AddressBook addressBook = await AddressBook.Query().GetEntityById(glView.AddressId);



                glView.DocType = udcDocType.KeyCode;
                glView.AccountId = coa.AccountId;
                glView.LedgerType = udcLedgerType.KeyCode;
                glView.CreatedDate = DateTime.Now;
                glView.AddressId = addressBook.AddressId;
                glView.FiscalPeriod = glView.GLDate.Month;
                glView.FiscalYear = glView.GLDate.Year;
                glView.DebitAmount = glView.Amount;
                glView.CreditAmount = 0;


                GeneralLedgerView glLookup = null;

                if (String.IsNullOrEmpty(glView.CheckNumber) == false)
                {
                    glLookup = await GeneralLedger.Query().GetLedgerViewByExpression(
                   e => e.AccountId == glView.AccountId
                   && e.AddressId == glView.AddressId
                   && e.Amount == glView.Amount
                   && e.CheckNumber == glView.CheckNumber
                   && e.DocType == glView.DocType
                   && e.Gldate == glView.GLDate
                   );
                }
                if (glLookup == null)
                {
                    //create income
                    
                    GeneralLedger.CreateGeneralLedgerByView(glView).Apply();
                    GeneralLedger.UpdateAccountBalances(glView);
                    GeneralLedgerView glViewLookup = await GeneralLedger.Query().GetViewByDocNumber(glView.DocNumber, glView.DocType);

                    return (glViewLookup != null);
                }
                else
                {
                    glView.DocNumber = glLookup.DocNumber;
                }
                return true;

            }
            catch (Exception ex) { throw new Exception("CreateCash", ex); }
        }
        public async Task<bool> CreateCash(GeneralLedgerView glView)
        {
            try
            {
                //cash
                ChartOfAccount coa2 = await ChartOfAccounts.Query().GetEntity("1000", "1200", "101", "");
                Udc udcLedgerType = await UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcDocType = await UDC.Query().GetUdc("DOCTYPE", "PV");
                AddressBook addressBook = await AddressBook.Query().GetEntityById(glView.AddressId);

                glView.DocNumber = -1;
                glView.DocType = udcDocType.KeyCode;
                glView.AccountId = coa2.AccountId;
                glView.LedgerType = udcLedgerType.KeyCode;
                glView.CreatedDate = DateTime.Now;
                glView.AddressId = addressBook.AddressId;

                glView.DebitAmount = glView.Amount;
                glView.CreditAmount = 0;
                glView.FiscalPeriod = glView.GLDate.Month;
                glView.FiscalYear = glView.GLDate.Year;

                GeneralLedgerView glLookup2 = null;

                if (String.IsNullOrEmpty(glView.CheckNumber) == false)
                {
                    glLookup2 = await GeneralLedger.Query().GetLedgerViewByExpression(
                   e => e.AccountId == glView.AccountId
                   && e.AddressId == glView.AddressId
                   && e.Amount == glView.Amount
                   && e.CheckNumber == glView.CheckNumber
                   && e.DocType == glView.DocType
                   && e.Gldate == glView.GLDate
                   );
                }
                if (glLookup2 == null)
                {
                    //create income

                    GeneralLedger.CreateGeneralLedgerByView(glView).Apply();
                    GeneralLedger.UpdateAccountBalances(glView);

                    GeneralLedgerView glViewLookup = await GeneralLedger.Query().GetViewByDocNumber(glView.DocNumber, glView.DocType);

                    return (glViewLookup != null);
                }
                else
                {
                    glView.DocNumber = glLookup2.DocNumber;
                }
                return true;


            }
            catch (Exception ex) { throw new Exception("CreateIncome", ex); }
        }
        public async Task<bool> CreateCashPayment(GeneralLedgerView glCashView)
        {
            try
            {
                ChartOfAccount coaCash = await ChartOfAccounts.Query().GetEntity("1000", "1200", "101", "");
                Udc udcLedgerType = await UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcDocType = await UDC.Query().GetUdc("DOCTYPE", "PV");
                AddressBook addressBook = await AddressBook.Query().GetEntityById(glCashView.AddressId);

                glCashView.AccountId = coaCash.AccountId;
                glCashView.DebitAmount = 0;
                glCashView.CreditAmount = glCashView.Amount;
                glCashView.FiscalPeriod = glCashView.GLDate.Month;
                glCashView.FiscalYear = glCashView.GLDate.Year;

                glCashView.DocType = udcDocType.KeyCode;
                glCashView.LedgerType = udcLedgerType.KeyCode;
                glCashView.CreatedDate = DateTime.Now;
                glCashView.AddressId = addressBook.AddressId;


                GeneralLedgerView glLookup = null;

                if (glCashView.CheckNumber != null)
                {
                    glLookup = await GeneralLedger.Query().GetLedgerViewByExpression(
                   e => e.AccountId == glCashView.AccountId
                   && e.AddressId == glCashView.AddressId
                   && e.Amount == glCashView.Amount
                   && e.CheckNumber == glCashView.CheckNumber
                   && e.DocType == glCashView.DocType
                   && e.Gldate == glCashView.GLDate
                   );
                }
                if (glLookup == null)
                {
                    GeneralLedger.CreateGeneralLedgerByView(glCashView).Apply();
                    GeneralLedger.UpdateAccountBalances(glCashView);
                }
                else
                {
                    glCashView.DocNumber = glLookup.DocNumber;
                }
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCashPayment", ex); }

        }
        public async Task<bool> CreatePersonalExpense(GeneralLedgerView glView)
        {
            try
            {


                ChartOfAccount coa = await ChartOfAccounts.Query().GetEntity("1000", "1200", "502", "01");
                Udc udcLedgerType = await UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");

                Udc udcDocType = await UDC.Query().GetUdc("DOCTYPE", "PV");

                AddressBook addressBook = await AddressBook.Query().GetEntityById(glView.AddressId);
                glView.DocType = udcDocType.KeyCode;
                glView.AccountId = coa.AccountId;
                glView.LedgerType = udcLedgerType.KeyCode;
                glView.AddressId = addressBook.AddressId;
                glView.CreatedDate = DateTime.Now;
                glView.DebitAmount = 0;
                glView.CreditAmount = glView.Amount;
                glView.FiscalPeriod = glView.GLDate.Month;
                glView.FiscalYear = glView.GLDate.Year;

                GeneralLedgerView glLookup = null;
                if (glView.CheckNumber != null)
                {
                    glLookup = await GeneralLedger.Query().GetLedgerViewByExpression(
                    e => e.AccountId == glView.AccountId
                    && e.AddressId == glView.AddressId
                    && e.Amount == glView.Amount
                    && e.CheckNumber == glView.CheckNumber
                    && e.DocType == glView.DocType
                    && e.Gldate == glView.GLDate
               );
                }
                if (glLookup == null)
                {
                    GeneralLedger.CreateGeneralLedgerByView(glView).Apply();
                    GeneralLedger.UpdateAccountBalances(glView);
                }
                else
                {
                    glView.DocNumber = glLookup.DocNumber;
                }
                return true;
            }
            catch (Exception ex) { throw new Exception("CreatePersonalExpense", ex); }
        }
    }
}
