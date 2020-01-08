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

            Customer customer = await invMod.Customer.Query().GetEntityById(9);
            AddressBook addressBookCustomer = await invMod.AddressBook.Query().GetEntityById(customer?.AddressId);
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

            invoiceView.InvoiceNumber = nextNumber.NextNumberValue;

            Invoice newInvoice = await invMod.Invoice.Query().MapToEntity(invoiceView);

            Observer mediator = new Observer();

            mediator.SubscribeToObserver(invMod, invMod.MessageFromObserver);
            

            IObservableAction observedAction = new ObservableAction();

            MessageAction action = new MessageAction
            {
                observed_action = "Add an Invoice",
                targetByName = nameof(Invoice),
                command_action = TypeOfObservableAction.InsertData,
                Invoice=newInvoice
            };
            observedAction.Actions.Add(action);

            NextNumber nextNumberContractInvoice = await invMod.ContractInvoice.Query().GetNextNumber();

            ContractInvoiceView contractInvoiceView = new ContractInvoiceView
            {
                InvoiceId = 5,
                ContractId = 1,
                ContractInvoiceNumber=nextNumberContractInvoice.NextNumberValue
            };

            ContractInvoice contractInvoice = await invMod.ContractInvoice.Query().MapToEntity(contractInvoiceView);

            MessageAction actionContractInvoice = new MessageAction
            {
                observed_action = "Add a Contract Invoice",
                targetByName = nameof(ContractInvoice),
                command_action = TypeOfObservableAction.InsertData,
                ContractInvoice = contractInvoice
            };
            observedAction.Actions.Add(actionContractInvoice);

            mediator.TransmitMessage(observedAction);
            observedAction.Actions.Clear();

            Invoice lookupInvoice = await invMod.Invoice.Query().GetEntityByNumber(invoiceView.InvoiceNumber);

            lookupInvoice.Amount = 9999;

            ContractInvoice lookupContractInvoice = await invMod.ContractInvoice.Query().GetEntityByNumber(contractInvoiceView.ContractInvoiceNumber);
     

            //*******Contract Invoice


            MessageAction actionInvoiceUpdate = new MessageAction
            {
                observed_action = "Update an Invoice",
                targetByName = nameof(Invoice),
                command_action = TypeOfObservableAction.UpdateData,
                Invoice = lookupInvoice
            };
            observedAction.Actions.Add(actionInvoiceUpdate);

            MessageAction actionInvoiceDelete = new MessageAction
            {
                observed_action = "Delete an Invoice",
                targetByName = nameof(Invoice),
                command_action = TypeOfObservableAction.DeleteData,
                Invoice = lookupInvoice
            };
            observedAction.Actions.Add(actionInvoiceDelete);

            MessageAction actionContractInvoiceDelete = new MessageAction
            {
                observed_action = "Delete an Contract Invoice",
                targetByName = nameof(ContractInvoice),
                command_action = TypeOfObservableAction.DeleteData,
                ContractInvoice = lookupContractInvoice
            };
            observedAction.Actions.Add(actionContractInvoiceDelete);

            mediator.TransmitMessage(observedAction);

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
            invoiceView.InvoiceDetailViews=listInvoiceDetails;

            List<InvoiceDetail> list = (await invDetailMod.InvoiceDetail.Query().MapToEntity(invoiceView.InvoiceDetailViews)).ToList<InvoiceDetail>();
            invDetailMod.InvoiceDetail.AddInvoiceDetails(list).Apply();

            invDetailMod.InvoiceDetail.DeleteInvoiceDetails(list).Apply();
            invMod.Invoice.DeleteInvoice(newInvoice).Apply();

            Invoice invoiceLookup = await invMod.Invoice.Query().GetEntityById(newInvoice.InvoiceId);

            Assert.NotNull(invoiceLookup);
        }
        [Fact]
        public void TestPostInvoiceAndDetailToAcctRec()
        {
            try
            {
                //NextNumber nextNumber = await unitOfWork.invoiceRepository.Get("InvoiceNumber");

                InvoiceView invoiceView = new InvoiceView();

                invoiceView.InvoiceDocument = "Inv-03";
                invoiceView.InvoiceDate = DateTime.Parse("8/10/2018");
                invoiceView.Amount = 1500.0M;
                invoiceView.CustomerId = 9;
                invoiceView.Description = "VNW Fixed Asset project";
                invoiceView.PaymentTerms = "Net 30";
                invoiceView.TaxAmount = 0;
                invoiceView.CompanyId = 1;

                InvoiceDetailView invoiceDetailView = new InvoiceDetailView();
                invoiceDetailView.Amount = 1500M;
                //invoiceDetailView.InvoiceId = invoice.InvoiceId;


                invoiceDetailView.UnitOfMeasure = "Project";
                invoiceDetailView.Quantity = 1;
                invoiceDetailView.UnitPrice = 1500M;
                invoiceDetailView.Amount = 1500M;
                invoiceDetailView.DiscountPercent = 0;
                invoiceDetailView.DiscountAmount = 0;
                invoiceDetailView.ItemId = 4;

              
                invoiceView.InvoiceDetailViews.Add(invoiceDetailView);

                InvoiceModule invoiceModule = new InvoiceModule();

                bool result = invoiceModule.PostInvoiceAndDetailToAcctRec(invoiceView);

              
            
                Assert.True(result);
            }
            catch (Exception ex)
            {
                throw new Exception("TestPostInvoiceAndDetailToAcctRec", ex);
            }

        }
                 
    
    }
}
