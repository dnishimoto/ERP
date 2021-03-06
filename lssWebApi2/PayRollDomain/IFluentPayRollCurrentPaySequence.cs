

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{ 

public interface IFluentPayRollCurrentPaySequence
    {
        IFluentPayRollCurrentPaySequenceQuery Query();
        IFluentPayRollCurrentPaySequence Apply();
        IFluentPayRollCurrentPaySequence AddPayRollCurrentPaySequence(PayRollCurrentPaySequence payRollCurrentPaySequence);
        IFluentPayRollCurrentPaySequence UpdatePayRollCurrentPaySequence(PayRollCurrentPaySequence payRollCurrentPaySequence);
        IFluentPayRollCurrentPaySequence DeletePayRollCurrentPaySequence(PayRollCurrentPaySequence payRollCurrentPaySequence);
     	IFluentPayRollCurrentPaySequence UpdatePayRollCurrentPaySequences(IList<PayRollCurrentPaySequence> newObjects);
        IFluentPayRollCurrentPaySequence AddPayRollCurrentPaySequences(List<PayRollCurrentPaySequence> newObjects);
        IFluentPayRollCurrentPaySequence DeletePayRollCurrentPaySequences(List<PayRollCurrentPaySequence> deleteObjects);
    }
}
