using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class GeneralLedgerController : Controller
    {

        [Route("CreateRevenueIncome")]
        [HttpPost]
        public void CreateRevenueIncome([FromBody] decimal income, DateTime glDate)
        {
            int addressId = 1;
            //decimal income = 2800M;

            GeneralLedgerModule ledgerMod = new GeneralLedgerModule();
            //Income GL

            //ChartOfAccts coa = await unitOfWork.generalLedgerRepository.GetChartofAccount("1000", "1200", "300", "");
            //ChartOfAccts coa = ledgerMod.ChartOfAccounts.Query().GetChartofAccount("1000", "1200", "300", "");

            //Udc udcLedgerType = await unitOfWork.generalLedgerRepository.GetUdc("GENERALLEDGERTYPE", "AA");
            //Udc udcLedgerType = ledgerMod.UDC.Query().GetUdc("GENERALLEDGERTYPE", "AA");
            //Udc udcDocType = ledgerMod.UDC.Query().GetUdc("DOCTYPE", "PV");

            //Udc udcDocType = await unitOfWork.generalLedgerRepository.GetUdc("DOCTYPE","PV");
            //AddressBook addressBook = await unitOfWork.addressBookRepository.GetAddressBookByAddressId(addressId);
            AddressBook addressBook = ledgerMod.AddressBook.Query().GetAddressBookByAddressId(addressId);
            GeneralLedgerView glView = new GeneralLedgerView();
            glView.DocNumber = -1;
            //glView.DocType = udcDocType.KeyCode;
            //glView.AccountId = coa.AccountId;
            glView.Amount = income;
            //glView.LedgerType = udcLedgerType.KeyCode;
            glView.GLDate = glDate;
            //glView.CreatedDate = DateTime.Now;
            glView.AddressId = addressBook.AddressId;
            glView.Comment = "Week end "+glDate.ToShortDateString();
            //glView.DebitAmount = income;
            //glView.CreditAmount = 0;
            //glView.FiscalPeriod = glDate.Month;
            //glView.FiscalYear = glDate.Year;

            bool result = ledgerMod.CreateIncomeAndCash(glView);

        }
       
        [Route("ById/{generalLedgerId}")]
        [HttpGet]
        public GeneralLedgerView GetByAccountId(long generalLedgerId)
        {
            GeneralLedgerModule glMod = new GeneralLedgerModule();
            return glMod.GeneralLedger.Query().GetLedgerViewById(generalLedgerId);
        }

       
        // GET: api/<controller>
        [Route("BySummary/{fiscalYear}")]
        [HttpGet]
        public IEnumerable<AccountSummaryView> GetSummary(int fiscalYear)
        {
  
            GeneralLedgerModule glMod = new GeneralLedgerModule();

            IEnumerable<AccountSummaryView> list = glMod.GeneralLedger.Query().GetAccountSummaryByFiscalYearViews(fiscalYear);

            return list;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
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
