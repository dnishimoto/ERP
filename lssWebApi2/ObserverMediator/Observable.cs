using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ObserverMediator
{
  

    public class Observer: IObserver
    {
        Dictionary<object, Func<IObservableAction, bool>> _subscriberContainer = new Dictionary<object, Func<IObservableAction, bool>>();

        public Observer()
        {
        }
        public void SubscribeToObserver(IEntity entity, Func<IObservableAction, bool> callbackFunction)
        {
            _subscriberContainer.Add(entity, callbackFunction);
        }
        public void TransmitMessage(IObservableAction message)
        {
            foreach (KeyValuePair<object, Func<IObservableAction, bool>> item in _subscriberContainer)
            {
                item.Value.Invoke(message);
            }
        }
    }
    public interface IObservableMediator
    {

        bool MessageFromObserver(IObservableAction message);

    }

   
}
