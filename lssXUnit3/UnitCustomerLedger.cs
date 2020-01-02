using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.CustomerLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.CustomerLedgerDomain
{

    public class UnitCustomerLedger
    {

        private readonly ITestOutputHelper output;

        public UnitCustomerLedger(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        { 

            AddressBook addressBook = null;
            CustomerLedgerModule CustomerLedgerMod = new CustomerLedgerModule();
            Customer customer = await CustomerLedgerMod.Customer.Query().GetEntityById(9);
            Invoice invoice = await CustomerLedgerMod.Invoice.Query().GetEntityById(18);
            AccountReceivable accountReceivable = await CustomerLedgerMod.AccountReceivable.Query().GetEntityById(12);
            ChartOfAccount chartOfAccount = await CustomerLedgerMod.ChartOfAccount.Query().GetEntityById(3);
            GeneralLedger generalLedger = await CustomerLedgerMod.GeneralLedger.Query().GetEntityById(12);
             if(customer !=null) addressBook= await CustomerLedgerMod.AddressBook.Query().GetEntityById(customer.AddressId);

            CustomerLedgerView view = new CustomerLedgerView()
            {
                CustomerId = customer.CustomerId,
                CustomerName = addressBook?.Name,
                InvoiceId = invoice.InvoiceId,
                InvoiceDocument = invoice.InvoiceDocument,
                AcctRecId = accountReceivable.AcctRecId,
                Amount = generalLedger.Amount,
                GLDate = generalLedger.Gldate,
                AccountId = chartOfAccount.AccountId,
                Account = chartOfAccount.Account,
                AccountDescription = chartOfAccount.Description,
                GeneralLedgerId = generalLedger.GeneralLedgerId,
                DocNumber = generalLedger.DocNumber,
                Comment = generalLedger.Comment,
                AddressId = addressBook.AddressId,
                CreatedDate = generalLedger.CreatedDate,
                DocType = generalLedger.DocType,
                DebitAmount=generalLedger.DebitAmount,
                CreditAmount=generalLedger.CreditAmount,
                FiscalYear=2019,
                FiscalPeriod=7,
                CheckNumber=generalLedger.CheckNumber

            };
            NextNumber nnNextNumber = await CustomerLedgerMod.CustomerLedger.Query().GetNextNumber();

            view.CustomerLedgerNumber = nnNextNumber.NextNumberValue;

            CustomerLedger customerLedger = await CustomerLedgerMod.CustomerLedger.Query().MapToEntity(view);

            CustomerLedgerMod.CustomerLedger.AddCustomerLedger(customerLedger).Apply();

            CustomerLedger newCustomerLedger = await CustomerLedgerMod.CustomerLedger.Query().GetEntityByNumber(view.CustomerLedgerNumber);

            Assert.NotNull(newCustomerLedger);

            newCustomerLedger.Comment = "payment in part update";

            CustomerLedgerMod.CustomerLedger.UpdateCustomerLedger(newCustomerLedger).Apply();

            CustomerLedgerView updateView = await CustomerLedgerMod.CustomerLedger.Query().GetViewById(newCustomerLedger.CustomerLedgerId);

            Assert.Same(updateView.Comment, "payment in part update");
              CustomerLedgerMod.CustomerLedger.DeleteCustomerLedger(newCustomerLedger).Apply();
            CustomerLedger lookupCustomerLedger= await CustomerLedgerMod.CustomerLedger.Query().GetEntityById(view.CustomerLedgerId);

            Assert.Null(lookupCustomerLedger);
        }
       
      

    }
}
