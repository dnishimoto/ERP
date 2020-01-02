

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.POQuoteDomain;

namespace lssWebApi2.POQuoteDomain
{ 

public interface IFluentPOQuote
    {
        IFluentPOQuoteQuery Query();
        IFluentPOQuote Apply();
        IFluentPOQuote AddPOQuote(Poquote poQuote);
        IFluentPOQuote UpdatePOQuote(Poquote poQuote);
        IFluentPOQuote DeletePOQuote(Poquote poQuote);
     	IFluentPOQuote UpdatePOQuotes(List<Poquote> newObjects);
        IFluentPOQuote AddPOQuotes(List<Poquote> newObjects);
        IFluentPOQuote DeletePOQuotes(List<Poquote> deleteObjects);
    }
}
