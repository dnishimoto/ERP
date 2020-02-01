using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using Xunit.Abstractions;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.ContractInvoiceDomain;
using lssWebApi2.AccountReceivableDomain;

namespace lssWebApi2.InvoiceDomain
{

    public class UnitTestInvoices
    {

        private readonly ITestOutputHelper output;

        public UnitTestInvoices(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestGetInvoiceFlatViews()
        {
            DateTime startDate = DateTime.Parse("1/1/2018");
            DateTime endDate = DateTime.Now;

            InvoiceModule invoiceModule = new InvoiceModule();

            List<InvoiceFlatView> list = invoiceModule.Invoice.Query().GetInvoicesByDate(startDate, endDate);
        }
        [Fact]
        public async Task TestObserver()
        {
            InvoiceModule invMod = new InvoiceModule();
            ContractInvoiceModule contractInvoiceMod = new ContractInvoiceModule();

            Customer customer = await invMod.Customer.Query().GetEntityById(9);
            AddressBook addressBookCustomer = await invMod.AddressBook.Query().GetEntityById(customer?.AddressId);
            NextNumber nextNumber = await invMod.Invoice.Query().GetNextNumber();
            TaxRatesByCode taxRatesByCode = await invMod.TaxRatesByCode.Query().GetEntityById(1);

            InvoiceView invoiceView = new InvoiceView();

            invoiceView.InvoiceDocument = "Inv-99";
            invoiceView.InvoiceDate = DateTime.Parse("8/10/2018");
            invoiceView.Amount = 1500.0M;
            invoiceView.CustomerId = customer?.CustomerId;
            invoiceView.CustomerName = addressBookCustomer?.Name;
            invoiceView.Description = "VNW Fixed Asset project";
            invoiceView.PaymentTerms = "Net 30";
            invoiceView.TaxAmount = 0;
            invoiceView.CompanyId = 1;
            invoiceView.TaxRatesByCodeId = taxRatesByCode.TaxRatesByCodeId;

            invoiceView.InvoiceNumber = nextNumber.NextNumberValue;

            Invoice newInvoice = await invMod.Invoice.Query().MapToEntity(invoiceView);

            Observer mediator = new Observer();
            mediator.SubscribeToObserver(invMod, invMod.MessageFromObserver);
            mediator.SubscribeToObserver(contractInvoiceMod, contractInvoiceMod.MessageFromObserver);


            IObservableAction observedAction = new ObservableAction();

            MessageAction action = new MessageAction { targetByName = nameof(Invoice), command_action = TypeOfObservableAction.InsertData, Invoice = newInvoice };
            observedAction.Actions.Add(action);

            NextNumber nextNumberContractInvoice = await invMod.ContractInvoice.Query().GetNextNumber();

            ContractInvoiceView contractInvoiceView = new ContractInvoiceView
            {
                InvoiceId = 5,
                ContractId = 1,
                ContractInvoiceNumber = nextNumberContractInvoice.NextNumberValue
            };

            ContractInvoice contractInvoice = await invMod.ContractInvoice.Query().MapToEntity(contractInvoiceView);

            MessageAction actionContractInvoice = new MessageAction { targetByName = nameof(ContractInvoice), command_action = TypeOfObservableAction.InsertData, ContractInvoice = contractInvoice };
            observedAction.Actions.Add(actionContractInvoice);

            mediator.TransmitMessage(observedAction);

            if (observedAction.Actions.Count() > 0) Assert.True(false);

            Invoice lookupInvoice = await invMod.Invoice.Query().GetEntityByNumber(invoiceView.InvoiceNumber);

            lookupInvoice.Amount = 9999;

            ContractInvoice lookupContractInvoice = await contractInvoiceMod.ContractInvoice.Query().GetEntityByNumber(contractInvoiceView.ContractInvoiceNumber);


            //*******Contract Invoice


            MessageAction actionInvoiceUpdate = new MessageAction
            {
                targetByName = nameof(Invoice),
                command_action = TypeOfObservableAction.UpdateData,
                Invoice = lookupInvoice
            };
            observedAction.Actions.Add(actionInvoiceUpdate);

            MessageAction actionInvoiceDelete = new MessageAction
            {
                targetByName = nameof(Invoice),
                command_action = TypeOfObservableAction.DeleteData,
                Invoice = lookupInvoice
            };
            observedAction.Actions.Add(actionInvoiceDelete);

            MessageAction actionContractInvoiceDelete = new MessageAction
            {
                targetByName = nameof(ContractInvoice),
                command_action = TypeOfObservableAction.DeleteData,
                ContractInvoice = lookupContractInvoice
            };
            observedAction.Actions.Add(actionContractInvoiceDelete);

            mediator.TransmitMessage(observedAction);

            if (observedAction.Actions.Count() > 0) Assert.True(false);

            await Task.Yield();
            Assert.True(true);

        }
        [Fact]
        public async Task TestAddInvoice()
        {
            InvoiceModule invMod = new InvoiceModule();
            InvoiceDetailModule invDetailMod = new InvoiceDetailModule();

            Customer customer = await invMod.Customer.Query().GetEntityById(9);
            AddressBook addressBookCustomer = await invMod.AddressBook.Query().GetEntityById(customer?.AddressId);
            TaxRatesByCode taxRatesByCode = await invMod.TaxRatesByCode.Query().GetEntityById(1);
            NextNumber nextNumber = await invMod.Invoice.Query().GetNextNumber();

            InvoiceView invoiceView = new InvoiceView();

            invoiceView.InvoiceDocument = "Inv-99";
            invoiceView.InvoiceDate = DateTime.Parse("8/10/2018");
            invoiceView.Amount = 1500.0M;
            invoiceView.CustomerId = customer?.CustomerId;
            invoiceView.CustomerName = addressBookCustomer?.Name;
            invoiceView.Description = "VNW Fixed Asset project";
            invoiceView.PaymentTerms = "Net 30";
            invoiceView.TaxAmount = 0;
            invoiceView.CompanyId = 1;
            invoiceView.TaxRatesByCodeId = taxRatesByCode.TaxRatesByCodeId;

            invoiceView.InvoiceNumber = nextNumber.NextNumberValue;

            Invoice invoice = await invMod.Invoice.Query().MapToEntity(invoiceView);
            invMod.Invoice.AddInvoice(invoice).Apply();

            Invoice newInvoice = await invMod.Invoice.Query().GetEntityByNumber(invoiceView.InvoiceNumber);

            InvoiceDetailView invoiceDetailView = new InvoiceDetailView();
            NextNumber nextNumberInvoiceDetail = await invDetailMod.InvoiceDetail.Query().GetNextNumber();

            invoiceDetailView.Amount = 1500M;

            invoiceDetailView.InvoiceId = newInvoice.InvoiceId;
            invoiceDetailView.InvoiceDetailNumber = nextNumberInvoiceDetail.NextNumberValue;

            invoiceDetailView.UnitOfMeasure = "Project";
            invoiceDetailView.Quantity = 1;
            invoiceDetailView.UnitPrice = 1500M;
            invoiceDetailView.Amount = 1500M;
            invoiceDetailView.DiscountPercent = 0;
            invoiceDetailView.DiscountAmount = 0;
            invoiceDetailView.ItemId = 4;
            IList<InvoiceDetailView> listInvoiceDetails = new List<InvoiceDetailView>();
            listInvoiceDetails.Add(invoiceDetailView);
            invoiceView.InvoiceDetailViews = listInvoiceDetails;

            List<InvoiceDetail> list = (await invDetailMod.InvoiceDetail.Query().MapToEntity(invoiceView.InvoiceDetailViews)).ToList<InvoiceDetail>();
            invDetailMod.InvoiceDetail.AddInvoiceDetails(list).Apply();

            invDetailMod.InvoiceDetail.DeleteInvoiceDetails(list).Apply();
            invMod.Invoice.DeleteInvoice(newInvoice).Apply();

            Invoice invoiceLookup = await invMod.Invoice.Query().GetEntityById(newInvoice.InvoiceId);

            Assert.Null(invoiceLookup);
        }
        [Fact]
        public async Task TestPostInvoiceAndDetailToAcctRec()
        {
            try
            {

                //NextNumber nextNumber = await unitOfWork.invoiceRepository.Get("InvoiceNumber");
                InvoiceModule invoiceModule = new InvoiceModule();
                AccountReceivableModule acctRecMod = new AccountReceivableModule();
                Customer customer = await acctRecMod.Customer.Query().GetEntityById(9);
                AddressBook addressBookCustomer = await acctRecMod.AddressBook.Query().GetEntityById(customer?.AddressId);
                ChartOfAccount chartOfAccount = await acctRecMod.ChartOfAccount.Query().GetEntity("1000", "1200", "101", "");
                PurchaseOrder purchaseOrder = await acctRecMod.PurchaseOrder.Query().GetEntityById(20);
                Udc udcReceivable = await acctRecMod.Udc.Query().GetEntityById(66);
                TaxRatesByCode taxRatesByCode = await invoiceModule.TaxRatesByCode.Query().GetEntityById(1);
                AccountReceivableView newAccountReceivableView = new AccountReceivableView
                {
                    Amount = 1500M,
                    OpenAmount = 1500M,
                    DiscountDueDate = DateTime.Parse("1/21/2020"),
                    PaymentDueDate = DateTime.Parse("1/21/2020"),
                    Gldate = DateTime.Parse("1/21/2020"),
                    CreateDate = DateTime.Parse("1/21/2020"),
                    DocNumber = (await acctRecMod.AccountReceivable.Query().GetDocNumber()).NextNumberValue,
                    Remarks = " partial payment",
                    PaymentTerms = "Net 30",
                    CustomerId = customer.CustomerId,
                    CustomerName = addressBookCustomer?.Name,
                    PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                    Description = "Fixed Asset Project",
                    AcctRecDocTypeXrefId = udcReceivable.XrefId,
                    DocType = udcReceivable.KeyCode,
                    AccountId = chartOfAccount.AccountId,
                    AccountReceivableNumber = (await acctRecMod.AccountReceivable.Query().GetNextNumber()).NextNumberValue
                };

                AccountReceivable  accountReceivable = await acctRecMod.AccountReceivable.Query().MapToEntity(newAccountReceivableView);
                acctRecMod.AccountReceivable.AddAccountReceivable(accountReceivable).Apply();

                InvoiceView invoiceView = new InvoiceView();

                invoiceView.InvoiceDocument = accountReceivable.DocNumber.ToString();
                invoiceView.InvoiceDate = DateTime.Parse("8/10/2018");
                invoiceView.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
                invoiceView.Amount = 1500.0M;
                invoiceView.CustomerId = customer.CustomerId;
                invoiceView.Description = "VNW Fixed Asset project";
                invoiceView.PaymentTerms = "Net 30";
                invoiceView.TaxAmount = 0;
                invoiceView.CompanyId = 1;
                invoiceView.TaxRatesByCodeId = taxRatesByCode.TaxRatesByCodeId;
                invoiceView.TaxCode = taxRatesByCode.TaxCode;
                invoiceView.InvoiceNumber = (await invoiceModule.Invoice.Query().GetNextNumber()).NextNumberValue;


                IList<PurchaseOrderDetail> listPurchaseOrderDetail = await acctRecMod.PurchaseOrderDetail.Query().GetEntitiesByPurchaseOrderId(purchaseOrder.PurchaseOrderId);

                IList<InvoiceDetailView> listInvoiceDetailView = new List<InvoiceDetailView>();
                //invoiceDetailView.InvoiceId = invoice.InvoiceId;

                foreach (var item in listPurchaseOrderDetail)
                {
                    listInvoiceDetailView.Add(
                    new InvoiceDetailView()
                    {
                        UnitOfMeasure = item.UnitOfMeasure,
                        Quantity = (int)item.OrderedQuantity,
                        PurchaseOrderId = item.PurchaseOrderId,
                        PurchaseOrderDetailId = item.PurchaseOrderDetailId,
                        UnitPrice = item.UnitPrice,
                        Amount = item.Amount,
                        DiscountPercent = 0,
                        DiscountAmount = 0,
                        ItemId = item.ItemId,
                        InvoiceDetailNumber=(await invoiceModule.InvoiceDetail.Query().GetNextNumber()).NextNumberValue
                    });
                }

                invoiceView.InvoiceDetailViews=listInvoiceDetailView;

                bool result = await invoiceModule.PostInvoiceAndDetailToAcctRec(invoiceView);



                Assert.True(result);
            }
            catch (Exception ex)
            {
                throw new Exception("TestPostInvoiceAndDetailToAcctRec", ex);
            }

        }


    }
}
