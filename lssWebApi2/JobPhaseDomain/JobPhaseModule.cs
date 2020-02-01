using lssWebApi2.AbstractFactory;
using lssWebApi2.JobPhaseDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.JobMasterDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.JobCostTypeDomain;
using lssWebApi2.Services;

namespace lssWebApi2.JobPhaseDomain
{
    public class JobPhaseModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentJobPhase JobPhase;
        public FluentJobMaster JobMaster;
        public FluentContract Contract;
        public FluentJobCostType JobCostType;
        public JobPhaseModule()
        {
            unitOfWork = new UnitOfWork();
            JobPhase = new FluentJobPhase(unitOfWork);
            JobMaster = new FluentJobMaster(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            JobCostType = new FluentJobCostType(unitOfWork);
        }


        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(JobPhase);

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
                        if (action?.JobPhase.JobPhaseNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await JobPhase.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.JobPhase.JobPhaseNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        JobPhase.AddJobPhase(action.JobPhase).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        JobPhase.UpdateJobPhase(action.JobPhase).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        JobPhase.DeleteJobPhase(action.JobPhase).Apply();
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
