using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.BudgetDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[Controller]")]
    public class BudgetController : Controller
    {
        // GET: api/<controller>
        [HttpGet("{budgetId}")]
        public BudgetView Get(long budgetId)
        {

            BudgetModule budgetMod = new BudgetModule();

            BudgetView budgetView = budgetMod.Budget.Query().GetBudgetView(budgetId);

            return budgetView;
        }
        [HttpGet]
        public IEnumerable<BudgetView> Get()
        {
     

            BudgetModule budgetMod = new BudgetModule();

            IEnumerable<BudgetView> budgetViews = budgetMod.Budget.Query().GetBudgetViews();

            return budgetViews;

        }
        [HttpGet]
        [Route("PersonalBudgetViews")]
        public List<PersonalBudgetView> GetPersonalBudgetViews()
        {
            BudgetModule budgetMod = new BudgetModule();
            List<PersonalBudgetView> list = budgetMod.Budget.Query().GetPersonalBudgetViews();
            return list;

        }

        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //   return "value";
        //}

        // POST api/<controller>
        [HttpPost]
        [Route("Payment")]
        public void PostPayment([FromBody] PersonalBudgetView budget)
        {
          
            GeneralLedgerModule ledgerMod = new GeneralLedgerModule();
        
            //long addressId = 1;
            decimal expense =budget.BudgetAmount/(Decimal)budget.PayCycles??0;
          
           // ChartOfAccts coa = ledgerMod.ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "502", "01");
            //Udc udcLedgerType = ledgerMod.UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
           
            //Udc udcDocType = ledgerMod.UDC.Query().GetUdc("DOCTYPE", "PV");
            //AddressBook addressBook = ledgerMod.AddressBook.Query().GetAddressBookByAddressId(addressId);
            GeneralLedgerView glView = new GeneralLedgerView();


            glView.DocNumber = -1;
            //glView.DocType = udcDocType.KeyCode;
            //glView.AccountId = coa.AccountId;
            glView.Amount = expense * -1;
            //glView.LedgerType = udcLedgerType.KeyCode;
            glView.GLDate = budget.GLDate;
            glView.CreatedDate = DateTime.Now;
            //glView.AddressId = addressBook.AddressId;
            //glView.Comment = "Mortgage Payment";
            //glView.DebitAmount = 0;
            //glView.CreditAmount = expense;
            //glView.FiscalPeriod = budget.GLDate.Month;
            //glView.FiscalYear = budget.GLDate.Year;

            bool result1 = ledgerMod.CreatePersonalExpense(glView);

            //GeneralLedgerView glViewLookup =
            //ledgerMod.GeneralLedger.Query().GetGeneralLedgerView(glView.DocNumber, glView.DocType);

            //ChartOfAccts coaCash = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "101", "");

            //ChartOfAccts coaCash = ledgerMod.ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "101", "");
            GeneralLedgerView glCashView = new GeneralLedgerView();


            //long cashDocumentNumber = 22;
            glCashView.DocNumber = -1;
            //glCashView.DocType = udcDocType.KeyCode;
            //glCashView.AccountId = coaCash.AccountId;
            glCashView.Amount = budget.PaymentAmount??0;
            //glCashView.LedgerType = udcLedgerType.KeyCode;
            glCashView.GLDate = budget.GLDate;
            //glCashView.CreatedDate = DateTime.Now;
            //glCashView.AddressId = addressBook.AddressId;
            glCashView.Comment = glView.Comment;
           // glCashView.DebitAmount = 0;
            //glCashView.CreditAmount = expense;
            //glCashView.FiscalPeriod = budget.GLDate.Month;
            //glCashView.FiscalYear = budget.GLDate.Year;

            bool result2 = ledgerMod.CreateCashPayment(glCashView);


           // GeneralLedgerView glCashViewLookup =
           //ledgerMod.GeneralLedger.Query().GetGeneralLedgerView(glCashView.DocNumber, glCashView.DocType);


            

        }


        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
