using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.FluentAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.GeneralLedgerDomain
{

    public class GeneralLedgerModule
    {
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        public FluentChartOfAccount ChartOfAccounts = new FluentChartOfAccount();
        public FluentNextNumber nn = new FluentNextNumber();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentUDC UDC = new FluentUDC();

        public bool CreateIncomeAndCash(GeneralLedgerView glView)
        {
            bool results = false;
            results = CreateCash(glView);
            results = results && CreateIncome(glView);
            return results;
        }

        public bool CreateIncome(GeneralLedgerView glView)
        {
            try
            {
                //ChartOfAccts coa = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "300", "");
                ChartOfAccts coa = ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "300", "");

                //Udc udcLedgerType = await unitOfWork.generalLedgerRepository.GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcLedgerType =UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcDocType = UDC.Query().GetUdc("DOCTYPE", "PV");

                //Udc udcDocType = await unitOfWork.generalLedgerRepository.GetUdc("DOCTYPE","PV");
                //AddressBook addressBook = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
                AddressBook addressBook = AddressBook.Query().GetEntityById(glView.AddressId);


            
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

                if (String.IsNullOrEmpty(glView.CheckNumber) ==false)
                {
                    glLookup=GeneralLedger.Query().GetLedgerViewByExpression(
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
                    NextNumber nnObject = nn.Query().GetNextNumber("DocNumber");

                    glView.DocNumber = nnObject.NextNumberValue;

                    GeneralLedger.CreateGeneralLedger(glView).Apply();
                    GeneralLedger.UpdateAccountBalances(glView);
                    GeneralLedgerView glViewLookup =
              GeneralLedger.Query().GetGeneralLedgerView(glView.DocNumber, glView.DocType);

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
        public bool CreateCash(GeneralLedgerView glView)
        {
            try
            {
                //cash
                ChartOfAccts coa2 = ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "101", "");
                Udc udcLedgerType = UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcDocType = UDC.Query().GetUdc("DOCTYPE", "PV");
                AddressBook addressBook = AddressBook.Query().GetEntityById(glView.AddressId);

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
                    glLookup2 = GeneralLedger.Query().GetLedgerViewByExpression(
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
                    NextNumber nnObject2 = nn.Query().GetNextNumber("DocNumber");

                    glView.DocNumber = nnObject2.NextNumberValue;

                    GeneralLedger.CreateGeneralLedger(glView).Apply();
                    GeneralLedger.UpdateAccountBalances(glView);

                    GeneralLedgerView glViewLookup =
                        GeneralLedger.Query().GetGeneralLedgerView(glView.DocNumber, glView.DocType);

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
        public bool CreateCashPayment(GeneralLedgerView glCashView)
        {
            try
            {
                ChartOfAccts coaCash = ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "101", "");
                Udc udcLedgerType = UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
                Udc udcDocType = UDC.Query().GetUdc("DOCTYPE", "PV");
                AddressBook addressBook = AddressBook.Query().GetEntityById(glCashView.AddressId);

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
                   glLookup= GeneralLedger.Query().GetLedgerViewByExpression(
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
                    NextNumber nnDocumentNumber = nn.Query().GetNextNumber("DocNumber");
                    glCashView.DocNumber = nnDocumentNumber.NextNumberValue;
                    GeneralLedger.CreateGeneralLedger(glCashView).Apply();
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
        public bool CreatePersonalExpense(GeneralLedgerView glView)
        {
            try
            {

             
                ChartOfAccts coa = ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "502", "01");
                Udc udcLedgerType = UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
              
                Udc udcDocType = UDC.Query().GetUdc("DOCTYPE", "PV");
               
                AddressBook addressBook = AddressBook.Query().GetEntityById(glView.AddressId);
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
                    glLookup = GeneralLedger.Query().GetLedgerViewByExpression(
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
                    NextNumber nnDocumentNumber = nn.Query().GetNextNumber("DocNumber");
                    glView.DocNumber = nnDocumentNumber.NextNumberValue;
                    GeneralLedger.CreateGeneralLedger(glView).Apply();
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
