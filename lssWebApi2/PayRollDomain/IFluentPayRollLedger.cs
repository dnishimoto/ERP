

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{ 

public interface IFluentPayRollLedger
    {
        IFluentPayRollLedgerQuery Query();
        IFluentPayRollLedger Apply();
        IFluentPayRollLedger AddPayRollLedger(PayRollLedger payRollLedger);
        IFluentPayRollLedger UpdatePayRollLedger(PayRollLedger payRollLedger);
        IFluentPayRollLedger DeletePayRollLedger(PayRollLedger payRollLedger);
     	IFluentPayRollLedger UpdatePayRollLedgers(List<PayRollLedger> newObjects);
        IFluentPayRollLedger AddPayRollLedgers(List<PayRollLedger> newObjects);
        IFluentPayRollLedger DeletePayRollLedgers(List<PayRollLedger> deleteObjects);
    }
}
