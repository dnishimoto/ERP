using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;
using lssWebApi2.Services;

namespace lssWebApi2.NextNumberDomain
{
    public class NextNumberModule : AbstractModule, IEntity, IObservableMediator
    {
        private UnitOfWork unitOfWork;
        public FluentNextNumber NextNumber;

        public NextNumberModule() {
            unitOfWork = new UnitOfWork();
            NextNumber = new FluentNextNumber(unitOfWork);
         }


	public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = nameof(NextNumber);

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
                        if (action?.NextNumber.NextNumberValue == 0)
                        {
                            Task<NextNumber> nextNumberTask = Task.Run(async()=>await NextNumber.Query().GetNextNumber());
                            Task.WaitAll(nextNumberTask);
                            action.NextNumber.NextNumberValue = nextNumberTask.Result.NextNumberValue;
                        }
                        NextNumber.AddNextNumber(action.NextNumber).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.UpdateData)
                    {
                        NextNumber.UpdateNextNumber(action.NextNumber).Apply();
                        process = true;
                    }
                    else if (action.command_action == TypeOfObservableAction.DeleteData)
                    {
                        NextNumber.DeleteNextNumber(action.NextNumber).Apply();
                        process = true;

                    }
                    if (process ==true)
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
