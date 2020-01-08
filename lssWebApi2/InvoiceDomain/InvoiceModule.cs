using lssWebApi2.AbstractFactory;
using lssWebApi2.FluentAPI;
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

namespace lssWebApi2.InvoiceDomain
{


    public class InvoiceModule : IEntity, IObservableMediator
    {
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentAccountReceivable AccountsReceivable = new FluentAccountReceivable();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentContractInvoice ContractInvoice = new FluentContractInvoice();

        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string classInvoice = nameof(Invoice);
            string classContractInvoice = nameof(ContractInvoice);

            try
            {
                var queryInvoice = message.Actions.Where(e => e.targetByName == classInvoice);

                foreach (var action in queryInvoice)
                {
                    if (action.command_action == TypeOfObservableAction.InsertData)
                    {
                        Invoice.AddInvoice(action.Invoice).Apply();
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        Invoice.UpdateInvoice(action.Invoice).Apply();
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        Invoice.DeleteInvoice(action.Invoice).Apply();

                    }
                }

                var queryContractInvoice = message.Actions.Where(e => e.targetByName == classContractInvoice);
                foreach (var action in queryContractInvoice)
                {
                    if (action.command_action == TypeOfObservableAction.InsertData)
                    {
                        ContractInvoice.AddContractInvoice(action.ContractInvoice).Apply();
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        ContractInvoice.UpdateContractInvoice(action.ContractInvoice).Apply();
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        ContractInvoice.DeleteContractInvoice(action.ContractInvoice).Apply();

                    }
                }

                return retVal;
            }
            catch (Exception ex) { throw new Exception("MessageFromObserver", ex);
    }
}
        public bool PostInvoiceAndDetailToAcctRec(InvoiceView invoiceView)
        {
            try
            {
                Invoice
                    .CreateInvoiceByView(invoiceView)
                    .Apply()
                    .MergeWithInvoiceNumber(ref invoiceView);

                InvoiceDetail
                    .CreateInvoiceDetailsByInvoiceView(invoiceView)
                    .Apply();
                AccountsReceivable
                    .CreateAcctRecByInvoiceView(invoiceView)
                    .Apply();

                GeneralLedger
                    .CreateGeneralLedgerByInvoiceView(invoiceView)
                    .Apply()
                    .UpdateLedgerBalances();
                return true;
            }
            catch (Exception ex) { throw new Exception("PostInvoiceAndDetailToAcctRec", ex); }
        }

   }
}
