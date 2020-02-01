using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.Services;

namespace lssWebApi2.JobCostTypeDomain
{
    public class JobCostTypeModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentJobCostType JobCostType;
        public JobCostTypeModule()
        {
            unitOfWork = new UnitOfWork();
            JobCostType = new FluentJobCostType(unitOfWork);
        }



        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(JobCostType);

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
                        if (action?.JobCostType.JobCostTypeNumber == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async () => await JobCostType.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.JobCostType.JobCostTypeNumber = nextNumberTask.Result.NextNumberValue;
                        }
                        JobCostType.AddJobCostType(action.JobCostType).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        JobCostType.UpdateJobCostType(action.JobCostType).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        JobCostType.DeleteJobCostType(action.JobCostType).Apply();
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
