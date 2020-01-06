

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{ 

public interface IFluentPayRollGroup
    {
        IFluentPayRollGroupQuery Query();
        IFluentPayRollGroup Apply();
        IFluentPayRollGroup AddPayRollGroup(PayRollGroup payRollGroup);
        IFluentPayRollGroup UpdatePayRollGroup(PayRollGroup payRollGroup);
        IFluentPayRollGroup DeletePayRollGroup(PayRollGroup payRollGroup);
     	IFluentPayRollGroup UpdatePayRollGroups(IList<PayRollGroup> newObjects);
        IFluentPayRollGroup AddPayRollGroups(List<PayRollGroup> newObjects);
        IFluentPayRollGroup DeletePayRollGroups(List<PayRollGroup> deleteObjects);
    }
}
