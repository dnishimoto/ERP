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
        public FluentChartOfAccounts ChartOfAccounts = new FluentChartOfAccounts();
        public FluentNextNumber nn = new FluentNextNumber();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentUDC UDC = new FluentUDC();

        public bool CreateIncomeAndCash(GeneralLedgerView glView)
        {

            GeneralLedgerView glLookup = GeneralLedger.Query().GetLedgerViewByExpression(
                e=>e.AccountId==glView.AccountId
                && e.AddressId==glView.AddressId
                && e.Amount ==glView.Amount
                && e.CheckNumber==glView.CheckNumber
                && e.DocType==glView.DocType
                && e.Gldate==glView.GLDate
                );
            if (glLookup == null)
            {
                //create income
                NextNumber nnObject = nn.Query().GetNextNumber("DocNumber");

                glView.DocNumber = nnObject.NextNumberValue;

                GeneralLedger.CreateGeneralLedger(glView).Apply();
                GeneralLedger.UpdateAccountBalances(glView);
            }

            //cash
            ChartOfAccts coa2 = ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "101", "");

            //long cashDocumentNumber = 21;
            GeneralLedgerView glView2 = new GeneralLedgerView();
            glView2.DocNumber = -1;
            glView2.DocType = glView.DocType;
            glView2.AccountId = coa2.AccountId;
            glView2.Amount = glView.Amount;
            glView2.LedgerType = glView.LedgerType;
            glView2.GLDate = glView.GLDate;
            glView2.CreatedDate = glView.CreatedDate;
            glView2.AddressId = glView.AddressId;
            glView2.Comment = glView.Comment;
            glView2.DebitAmount = glView.Amount;
            glView2.CreditAmount = 0;
            glView2.FiscalPeriod = 9;
            glView2.FiscalYear = 2018;

            GeneralLedgerView glLookup2 = GeneralLedger.Query().GetLedgerViewByExpression(
              e => e.AccountId == glView2.AccountId
              && e.AddressId == glView2.AddressId
              && e.Amount == glView2.Amount
              && e.CheckNumber == glView2.CheckNumber
              && e.DocType == glView2.DocType
              && e.Gldate == glView2.GLDate
              );
            if (glLookup2 == null)
            {
                //create income
                NextNumber nnObject2 = nn.Query().GetNextNumber("DocNumber");

                glView2.DocNumber = nnObject2.NextNumberValue;

                  GeneralLedger.CreateGeneralLedger(glView2).Apply();
                GeneralLedger.UpdateAccountBalances(glView2);
            }


            GeneralLedgerView glViewLookup =
                GeneralLedger.Query().GetGeneralLedgerView(glView2.DocNumber, glView2.DocType);

            return (glViewLookup != null);
        }
        public bool CreateCashPayment(GeneralLedgerView glCashView)
        {
            try
            {
                GeneralLedgerView glLookup = GeneralLedger.Query().GetLedgerViewByExpression(
              e => e.AccountId == glCashView.AccountId
              && e.AddressId == glCashView.AddressId
              && e.Amount == glCashView.Amount
              && e.CheckNumber == glCashView.CheckNumber
              && e.DocType == glCashView.DocType
              && e.Gldate == glCashView.GLDate
              );
                if (glLookup == null)
                {
                    NextNumber nnDocumentNumber = nn.Query().GetNextNumber("DocNumber");
                    glCashView.DocNumber = nnDocumentNumber.NextNumberValue;
                    GeneralLedger.CreateGeneralLedger(glCashView).Apply();
                    GeneralLedger.UpdateAccountBalances(glCashView);
                }
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCashPayment", ex); }

        }
        public bool CreatePersonalExpense(GeneralLedgerView glView)
        {
            try
            {
                GeneralLedgerView glLookup = GeneralLedger.Query().GetLedgerViewByExpression(
               e => e.AccountId == glView.AccountId
               && e.AddressId == glView.AddressId
               && e.Amount == glView.Amount
               && e.CheckNumber == glView.CheckNumber
               && e.DocType == glView.DocType
               && e.Gldate == glView.GLDate
               );
                if (glLookup == null)
                {
                    NextNumber nnDocumentNumber = nn.Query().GetNextNumber("DocNumber");
                    glView.DocNumber = nnDocumentNumber.NextNumberValue;
                    GeneralLedger.CreateGeneralLedger(glView).Apply();
                    GeneralLedger.UpdateAccountBalances(glView);
                }
                return true;
            }
            catch (Exception ex) { throw new Exception("CreatePersonalExpense", ex); }
        }
    }
}
