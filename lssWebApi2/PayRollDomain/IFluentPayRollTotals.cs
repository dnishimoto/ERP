

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{ 

public interface IFluentPayRollTotals
    {
        IFluentPayRollTotalsQuery Query();
        IFluentPayRollTotals Apply();
        IFluentPayRollTotals AddPayRollTotals(PayRollTotals payRollTotals);
        IFluentPayRollTotals UpdatePayRollTotals(PayRollTotals payRollTotals);
        IFluentPayRollTotals DeletePayRollTotals(PayRollTotals payRollTotals);
     	IFluentPayRollTotals UpdatePayRollTotalss(IList<PayRollTotals> newObjects);
        IFluentPayRollTotals AddPayRollTotalss(List<PayRollTotals> newObjects);
        IFluentPayRollTotals DeletePayRollTotalss(List<PayRollTotals> deleteObjects);
    }
}
