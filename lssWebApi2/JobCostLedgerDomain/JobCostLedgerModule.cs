using lssWebApi2.AbstractFactory;
using lssWebApi2.JobCostLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.ContractDomain;
using lssWebApi2.JobMasterDomain;
using lssWebApi2.JobPhaseDomain;
using lssWebApi2.JobCostTypeDomain;
using lssWebApi2.Services;
using System.Threading.Tasks;

namespace lssWebApi2.JobCostLedgerDomain
{
    public class JobCostLedgerModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentJobCostLedger JobCostLedger;
        public FluentContract Contract;
        public FluentJobMaster JobMaster;
        public FluentJobPhase JobPhase;
        public FluentJobCostType JobCostType;

        public JobCostLedgerModule()
        {
            unitOfWork = new UnitOfWork();
            JobCostLedger = new FluentJobCostLedger(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            JobMaster = new FluentJobMaster(unitOfWork);
            JobPhase = new FluentJobPhase(unitOfWork);
            JobCostType = new FluentJobCostType(unitOfWork);
        }


        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(JobCostLedger);

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
                        if (action?.JobCostLedger.JobCostLedgerNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await JobCostLedger.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.JobCostLedger.JobCostLedgerNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        JobCostLedger.AddJobCostLedger(action.JobCostLedger).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        JobCostLedger.UpdateJobCostLedger(action.JobCostLedger).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        JobCostLedger.DeleteJobCostLedger(action.JobCostLedger).Apply();
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
