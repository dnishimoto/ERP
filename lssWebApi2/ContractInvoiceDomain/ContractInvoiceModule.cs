using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.ContractDomain;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;

namespace lssWebApi2.ContractInvoiceDomain
{
    public class ContractInvoiceModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentContractInvoice ContractInvoice;
        public FluentContract Contract;
        public FluentInvoice Invoice;
        public ContractInvoiceModule()
        {
            unitOfWork = new UnitOfWork();
            ContractInvoice = new FluentContractInvoice(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
        }

        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(ContractInvoice);
            bool process = false;
            IList<MessageAction> listRemove = new List<MessageAction>();
            try
            {


                var queryContractInvoice = message.Actions.Where(e => e.targetByName == className);

                foreach (var action in queryContractInvoice)
                {
                    process = false;
                    if (action.command_action == TypeOfObservableAction.InsertData)
                    {
                        if (action?.ContractInvoice.ContractInvoiceNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await ContractInvoice.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.ContractInvoice.ContractInvoiceNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        ContractInvoice.AddContractInvoice(action.ContractInvoice).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        ContractInvoice.UpdateContractInvoice(action.ContractInvoice).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        ContractInvoice.DeleteContractInvoice(action.ContractInvoice).Apply();
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
