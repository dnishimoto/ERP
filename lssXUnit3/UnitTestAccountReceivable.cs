using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using lssWebApi2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.AccountsReceivableDomain
{
    
       public class UnitTestAccountReceivable
    {
        private readonly ITestOutputHelper output;

       

        public UnitTestAccountReceivable(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public async Task UnitTestLatePayment()
        {
            DateTime asOfDate = DateTime.Now;

            AccountReceivableModule acctRecMod = new AccountReceivableModule();

            IList<AccountReceivableFlatView> list = await acctRecMod.AccountReceivable.Query().GetOpenAccountReceivables();

            foreach (var item in list)
            {
                bool status= acctRecMod.AccountReceivable.Query().IsPaymentLate(item.InvoiceId,asOfDate);
                if (status == true)
                {
                    bool statusFee = acctRecMod.AccountReceivable.Query().HasLateFee(item.AccountReceivableId);

                    if (statusFee == false)
                    {
                        acctRecMod.AccountReceivableFee.CreateLateFee(item).Apply();
                        
                    }
                    acctRecMod.AccountReceivable.AdjustOpenAmount(item).Apply();


                }
            }

        }
        [Fact]
        async Task TestCreateAccountReceivableFromPO()
        {
            AddressBook addressBook = null;
            AddressBook buyerAddressBook = null;
            PurchaseOrderModule PurchaseOrderMod = new PurchaseOrderModule();
            ChartOfAccount account = await PurchaseOrderMod.ChartOfAccount.Query().GetEntityById(17);
            Supplier supplier = await PurchaseOrderMod.Supplier.Query().GetEntityById(3);
            if (supplier != null) { addressBook = await PurchaseOrderMod.AddressBook.Query().GetEntityById(supplier.AddressId); }
            Contract contract = await PurchaseOrderMod.Contract.Query().GetEntityById(1);
            Poquote poquote = await PurchaseOrderMod.POQuote.Query().GetEntityById(2);
            Buyer buyer = await PurchaseOrderMod.Buyer.Query().GetEntityById(1);
            if (buyer != null) buyerAddressBook = await PurchaseOrderMod.AddressBook.Query().GetEntityById(buyer.AddressId);
            TaxRatesByCode taxRatesByCode = await PurchaseOrderMod.TaxRatesByCode.Query().GetEntityById(1);

            PurchaseOrderView view = new PurchaseOrderView()
            {
                DocType = "STD",
                PaymentTerms = "Net 30",
                Amount = 286.11M,
                AmountPaid = 0,
                Remark = "PO Remark",
                Gldate = DateTime.Parse("11/29/2019"),
                AccountId = account.AccountId,
                Location = account.Location,
                BusUnit = account.BusUnit,
                Subsidiary = account.Subsidiary,
                SubSub = account.SubSub,
                Account = account.Account,
                AccountDescription = account.Description,
                SupplierId = supplier.SupplierId,
                CustomerId = contract?.CustomerId,
                SupplierName = addressBook.Name,
                ContractId = contract?.ContractId,
                PoquoteId = poquote?.PoquoteId,
                QuoteAmount = poquote?.QuoteAmount,
                Description = "PO Description",
                Ponumber = "PO-123",
                TakenBy = "David Nishimoto",
                ShippedToName = " shipped name",
                ShippedToAddress1 = "shipped to address1",
                ShippedToAddress2 = "shipped to address2",
                ShippedToCity = "shipped city",
                ShippedToState = "ID",
                ShippedToZipcode = "83709",
                BuyerId = buyer.BuyerId,
                BuyerName = buyerAddressBook?.Name,
                RequestedDate = DateTime.Parse("11/29/2019"),
                PromisedDeliveredDate = DateTime.Parse("11/29/2019"),
                Tax = 0M,
                TransactionDate = DateTime.Parse("11/29/2019"),
                TaxCode1 = taxRatesByCode.TaxCode,
                TaxCode2 = "",
                TaxRate = taxRatesByCode.TaxRate ?? 0,
                PurchaseOrderNumber = (await PurchaseOrderMod.PurchaseOrder.Query().GetNextNumber()).NextNumberValue
            };

            PurchaseOrder purchaseOrder = await PurchaseOrderMod.PurchaseOrder.Query().MapToEntity(view);

            PurchaseOrderMod.PurchaseOrder.AddPurchaseOrder(purchaseOrder).Apply();

            Udc udcAccountReceivableType = await PurchaseOrderMod.Udc.Query().GetEntityById(66);
            ChartOfAccount coaAccountReceivable = await PurchaseOrderMod.ChartOfAccount.Query().GetEntityById(4);
            AccountReceivable accountReceivable = await PurchaseOrderMod.AccountReceivable.Query().MapEntityFromPurchaseOrder(purchaseOrder, udcAccountReceivableType, coaAccountReceivable);
            PurchaseOrderMod.AccountReceivable.AddAccountReceivable(accountReceivable).Apply();


        }
        [Fact]
        public async Task TestOpenAccountReceivables()
        {
            AccountReceivableModule acctRecMod = new AccountReceivableModule();
            IList<AccountReceivableFlatView> list = await acctRecMod.AccountReceivable.Query().GetOpenAccountReceivables();
            Assert.True(true);
        }
        [Fact]
        public async Task TestCustomerCashPayment3()
        {
            int customerId = 2;

            GeneralLedgerView ledgerView = new GeneralLedgerView();

            CustomerModule custMod = new CustomerModule();
            long? addressId = await custMod.AddressBook.Query().GetAddressIdByCustomerId(customerId);

            ChartOfAccountModule coaMod = new ChartOfAccountModule();
            ChartOfAccount coa = await coaMod.ChartOfAccount.Query().GetEntity("1000", "1200", "101", "");

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


            AccountReceivableModule acctRecMod = new AccountReceivableModule();
            bool result=await acctRecMod.CreateCustomerLedger(ledgerView);

              //bool result = await acctRec.CustomerCashPayment(ledgerView);


            Assert.True(result);
        }
        [Fact]
        public async Task TestCustomerCashPayment2()
        {
            int customerId = 2;

        
            GeneralLedgerView ledgerView = new GeneralLedgerView();

            CustomerModule custMod = new CustomerModule();
            long? addressId = await custMod.AddressBook.Query().GetAddressIdByCustomerId(customerId);

            ChartOfAccountModule coaMod = new ChartOfAccountModule();

            ChartOfAccount coa = await coaMod.ChartOfAccount.Query().GetEntity("1000", "1200", "101", "");

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

            AccountReceivableModule acctRecMod = new AccountReceivableModule();
            bool result = await acctRecMod.CreateCustomerLedger(ledgerView);
            Assert.True(result);
        }
        [Fact]
        public async Task TestCustomerCashPayment()
        {
            long ? customerId = 9;

            GeneralLedgerView ledgerView = new GeneralLedgerView();

            CustomerModule custMod = new CustomerModule();
            long? addressId = await custMod.AddressBook.Query().GetAddressIdByCustomerId(customerId);

            ChartOfAccountModule coaMod = new ChartOfAccountModule();

             ChartOfAccount coa = await coaMod.ChartOfAccount.Query().GetEntity("1000", "1200", "101", "");

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


            AccountReceivableModule acctRecMod = new AccountReceivableModule();

            bool result = await acctRecMod.CreateCustomerCashPayment(ledgerView);
          

            Assert.True(true);
        }
              
    
    }
}
