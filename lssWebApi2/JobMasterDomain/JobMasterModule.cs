using lssWebApi2.AbstractFactory;
using lssWebApi2.JobMasterDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.JobPhaseDomain;
using lssWebApi2.JobCostTypeDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.JobMasterDomain
{
    public class JobMasterModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentJobMaster JobMaster;
        public FluentAddressBook AddressBook;
        public FluentCustomer Customer;
        public FluentContract Contract;
        public FluentJobPhase JobPhase;
        public FluentJobCostType JobCostType;

        public JobMasterModule()
        {
            unitOfWork = new UnitOfWork();
            JobMaster = new FluentJobMaster(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            JobPhase = new FluentJobPhase(unitOfWork);
            JobCostType = new FluentJobCostType(unitOfWork);
        }



        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(JobMaster);

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
                        if (action?.JobMaster.JobMasterNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await JobMaster.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.JobMaster.JobMasterNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        JobMaster.AddJobMaster(action.JobMaster).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        JobMaster.UpdateJobMaster(action.JobMaster).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        JobMaster.DeleteJobMaster(action.JobMaster).Apply();
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
