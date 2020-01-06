

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.BuyerDomain
{ 

public interface IFluentBuyer
    {
        IFluentBuyerQuery Query();
        IFluentBuyer Apply();
        IFluentBuyer AddBuyer(Buyer buyer);
        IFluentBuyer UpdateBuyer(Buyer buyer);
        IFluentBuyer DeleteBuyer(Buyer buyer);
     	IFluentBuyer UpdateBuyers(IList<Buyer> newObjects);
        IFluentBuyer AddBuyers(List<Buyer> newObjects);
        IFluentBuyer DeleteBuyers(List<Buyer> deleteObjects);
    }
}
