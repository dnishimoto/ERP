﻿using System;
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
        [Route("IncomeShortView")]
        [HttpPost]
        public void PostIncome([FromBody] IncomeShortView incomeShortView)
        {
            int addressId = 1;
         
            GeneralLedgerModule ledgerMod = new GeneralLedgerModule();
            //Income GL

            GeneralLedgerView glView = new GeneralLedgerView();
            glView.DocNumber = -1;
           glView.Amount = incomeShortView.Amount;
            glView.GLDate = incomeShortView.GLDate;
            glView.AddressId = addressId;
            glView.Comment = incomeShortView.Comment;
             glView.CheckNumber = incomeShortView.CheckNumber;

            bool result = ledgerMod.CreateIncomeAndCash(glView);



        }
        [Route("IncomeViews")]
        [HttpGet]
        public List<IncomeView> GetIncomeViews()
        {
            GeneralLedgerModule glMod = new GeneralLedgerModule();
            List<IncomeView> list = glMod.GeneralLedger.Query().GetIncomeViews();
            return list;
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
