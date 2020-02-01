
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using System.Linq;
using lssWebApi2.ContractInvoiceDomain;
using System.Threading.Tasks;
using lssWebApi2.SupplierDomain;
using lssWebApi2.TaxRatesByCodeDomain;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using lssWebApi2.GeneralLedgerDomain;

namespace lssWebApi2.InvoiceDomain
{


    public class InvoiceModule : IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentInvoice Invoice;
        public FluentInvoiceDetail InvoiceDetail;
        public FluentAccountReceivable AccountReceivable;
        public FluentGeneralLedger GeneralLedger;
        public FluentCustomer Customer;
        public FluentSupplier Supplier;
        public FluentAddressBook AddressBook;
        public FluentContractInvoice ContractInvoice;
        public FluentTaxRatesByCode TaxRatesByCode;
        public FluentCustomerLedger CustomerLedger;

        public InvoiceModule()
        {
            unitOfWork = new UnitOfWork();
            Invoice = new FluentInvoice(unitOfWork);
            InvoiceDetail = new FluentInvoiceDetail(unitOfWork);
            AccountReceivable = new FluentAccountReceivable(unitOfWork);
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            ContractInvoice = new FluentContractInvoice(unitOfWork);
            TaxRatesByCode = new FluentTaxRatesByCode(unitOfWork);
            CustomerLedger = new FluentCustomerLedger(unitOfWork);
        }



        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(Invoice);

            bool process = false;

            IList<MessageAction> listRemove = new List<MessageAction>();
            try
            {
                var queryInvoice = message.Actions.Where(e => e.targetByName == className);

                foreach (var action in queryInvoice)
                {
                    process = false;
                    if (action.command_action == TypeOfObservableAction.InsertData)
                    {
                        if (action?.Invoice.InvoiceNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await Invoice.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.Invoice.InvoiceNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        Invoice.AddInvoice(action.Invoice).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        Invoice.UpdateInvoice(action.Invoice).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        Invoice.DeleteInvoice(action.Invoice).Apply();
                        process = true;

                    }
                    if (process == true)
                    {
                        listRemove.Add(action);
                    }
                }



                foreach (var item in listRemove)
                {
                    message.Actions.Remove(item);
                }



                return retVal;
            }
            catch (Exception ex)
            {
                throw new Exception("MessageFromObserver", ex);
            }
        }
        public async Task<bool> PostInvoiceAndDetailToAcctRec(InvoiceView invoiceView)
        {
            try
            {
                Invoice invoiceNew = await Invoice.Query().MapToEntity(invoiceView);

                Invoice
                    .AddInvoice(invoiceNew)
                    .Apply();

                Invoice invoiceLookup = await Invoice.Query().GetEntityByInvoiceDocument(invoiceView.InvoiceDocument);

                invoiceView.InvoiceId = invoiceLookup.InvoiceId;

                //Assign the InvoiceId
                for (int i = 0; i < invoiceView.InvoiceDetailViews.Count; i++)
                {
                    invoiceView.InvoiceDetailViews[i].InvoiceId = invoiceLookup.InvoiceId;
                    InvoiceDetail invoiceDetail = await InvoiceDetail.Query().MapToEntity(invoiceView.InvoiceDetailViews[i]);

                    InvoiceDetail.AddInvoiceDetail(invoiceDetail);

                }

                InvoiceDetail.Apply();

                await AccountReceivable
                    .UpdateAcctRecByInvoiceView(invoiceView);
                AccountReceivable.Apply();

                await GeneralLedger
                    .CreateGeneralLedgerByInvoiceView(invoiceView);
                GeneralLedger.Apply();

                AccountReceivable acctRecLookup = await AccountReceivable.Query().GetEntityByPurchaseOrderId(invoiceView.PurchaseOrderId);

                GeneralLedger.UpdateLedgerBalances(acctRecLookup.DocNumber ?? 0, "OV");

                await CustomerLedger
                    .CreateCustomerLedgerByInvoiceView(invoiceView);
                CustomerLedger.Apply();


                return true;
            }
            catch (Exception ex) { throw new Exception("PostInvoiceAndDetailToAcctRec", ex); }
        }

    }
}
