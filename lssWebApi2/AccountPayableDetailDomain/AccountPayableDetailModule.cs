using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.AccountPayableDomain;

namespace lssWebApi2.AccountPayableDetailDomain
{
    public class AccountPayableDetailModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentAccountPayableDetail AccountPayableDetail;
        public FluentInvoice Invoice;
        public FluentInvoiceDetail InvoiceDetail;
        public FluentPurchaseOrderDetail PurchaseOrderDetail;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentAccountPayable AccountPayable;

        public AccountPayableDetailModule()
        {
            unitOfWork = new UnitOfWork();
            AccountPayableDetail = new FluentAccountPayableDetail(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            InvoiceDetail = new FluentInvoiceDetail(unitOfWork);
            PurchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            AccountPayable = new FluentAccountPayable(unitOfWork);
        }

               
        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(AccountPayableDetail);

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
                        if (action?.AccountPayableDetail.AccountPayableDetailNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await AccountPayableDetail.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.AccountPayableDetail.AccountPayableDetailNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        AccountPayableDetail.AddAccountPayableDetail(action.AccountPayableDetail).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        AccountPayableDetail.UpdateAccountPayableDetail(action.AccountPayableDetail).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        AccountPayableDetail.DeleteAccountPayableDetail(action.AccountPayableDetail).Apply();
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
