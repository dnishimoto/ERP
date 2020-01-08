using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ObserverMediator
{
    public interface IEntity
    {
    }
    public interface IObserver
    {
        void SubscribeToObserver(IEntity entity, Func<IObservableAction, bool> callbackFunction);
        void TransmitMessage(IObservableAction message);
    }
    public interface IObservableAction : IEntity
    {
        List<MessageAction> Actions { get; set; }
      
    }

    
}
