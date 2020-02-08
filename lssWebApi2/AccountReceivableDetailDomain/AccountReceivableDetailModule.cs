using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountReceivableDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.Services;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.NextNumberDomain;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.ItemMasterDomain;

namespace lssWebApi2.AccountReceivableDetailDomain
{
    public class AccountReceivableDetailModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentAccountReceivableDetail AccountReceivableDetail;
        public FluentAccountReceivable AccountReceivable;
        public FluentChartOfAccount ChartOfAccount;
        public FluentInvoice Invoice;
        public FluentInvoiceDetail InvoiceDetail;
        public FluentUdc Udc;
        public FluentCustomer Customer;
        public FluentAddressBook AddressBook;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentPurchaseOrderDetail PurchaseOrderDetail;
        public FluentItemMaster ItemMaster;
        

        public AccountReceivableDetailModule()
        {
            unitOfWork = new UnitOfWork();
            AccountReceivableDetail = new FluentAccountReceivableDetail(unitOfWork);
            AccountReceivable = new FluentAccountReceivable(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            InvoiceDetail  =new FluentInvoiceDetail(unitOfWork);
            PurchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
        }

        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(AccountReceivableDetail);

            bool process = false;

            IList<MessageAction> listRemove = new List<MessageAction>();
            try
            {
                var query = message.Actions.Where(e => e.targetByName == className);

                foreach (var action in query)
                {
                    process = false;
                    if (action.command_action == TypeOfObservableAction.InsertData)
                    {
                        if (action?.AccountReceivableDetail.AccountReceivableDetailNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await AccountReceivableDetail.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.AccountReceivableDetail.AccountReceivableDetailNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        AccountReceivableDetail.AddAccountReceivableDetail(action.AccountReceivableDetail).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        AccountReceivableDetail.UpdateAccountReceivableDetail(action.AccountReceivableDetail).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        AccountReceivableDetail.DeleteAccountReceivableDetail(action.AccountReceivableDetail).Apply();
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
    }
}
