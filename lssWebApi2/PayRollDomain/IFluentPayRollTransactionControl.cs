

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{ 

public interface IFluentPayRollTransactionControl
    {
        IFluentPayRollTransactionControlQuery Query();
        IFluentPayRollTransactionControl Apply();
        IFluentPayRollTransactionControl AddPayRollTransactionControl(PayRollTransactionControl payRollTransactionControl);
        IFluentPayRollTransactionControl UpdatePayRollTransactionControl(PayRollTransactionControl payRollTransactionControl);
        IFluentPayRollTransactionControl DeletePayRollTransactionControl(PayRollTransactionControl payRollTransactionControl);
     	IFluentPayRollTransactionControl UpdatePayRollTransactionControls(List<PayRollTransactionControl> newObjects);
        IFluentPayRollTransactionControl AddPayRollTransactionControls(List<PayRollTransactionControl> newObjects);
        IFluentPayRollTransactionControl DeletePayRollTransactionControls(List<PayRollTransactionControl> deleteObjects);
    }
}
