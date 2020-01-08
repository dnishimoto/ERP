using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace lssWebApi2.Enumerations
{
    public static class ObservableMessaging
    {
        public const int MessagingInsertData = 1;
        public const int MessagingUpdateData = 2;
        public const int MessagingDeleteData = 3;
    }
    public enum TypeOfObservableAction
    {
        InsertData= ObservableMessaging.MessagingInsertData,
        UpdateData= ObservableMessaging.MessagingUpdateData,
        DeleteData= ObservableMessaging.MessagingDeleteData
    }
}
