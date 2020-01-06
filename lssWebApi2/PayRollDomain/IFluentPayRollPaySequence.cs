

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{ 

public interface IFluentPayRollPaySequence
    {
        IFluentPayRollPaySequenceQuery Query();
        IFluentPayRollPaySequence Apply();
        IFluentPayRollPaySequence AddPayRollPaySequence(PayRollPaySequence payRollPaySequence);
        IFluentPayRollPaySequence UpdatePayRollPaySequence(PayRollPaySequence payRollPaySequence);
        IFluentPayRollPaySequence DeletePayRollPaySequence(PayRollPaySequence payRollPaySequence);
     	IFluentPayRollPaySequence UpdatePayRollPaySequences(IList<PayRollPaySequence> newObjects);
        IFluentPayRollPaySequence AddPayRollPaySequences(List<PayRollPaySequence> newObjects);
        IFluentPayRollPaySequence DeletePayRollPaySequences(List<PayRollPaySequence> deleteObjects);
    }
}
