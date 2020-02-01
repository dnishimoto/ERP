

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.NextNumberDomain;

namespace lssWebApi2.NextNumberDomain
{ 

public interface IFluentNextNumber
    {
        IFluentNextNumberQuery Query();
        IFluentNextNumber Apply();
        IFluentNextNumber AddNextNumber(NextNumber nextNumber);
        IFluentNextNumber UpdateNextNumber(NextNumber nextNumber);
        IFluentNextNumber DeleteNextNumber(NextNumber nextNumber);
     	IFluentNextNumber UpdateNextNumbers(List<NextNumber> newObjects);
        IFluentNextNumber AddNextNumbers(List<NextNumber> newObjects);
        IFluentNextNumber DeleteNextNumbers(List<NextNumber> deleteObjects);
    }
}
