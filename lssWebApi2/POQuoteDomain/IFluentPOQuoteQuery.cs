using lssWebApi2.AutoMapper;
using lssWebApi2.POQuoteDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPOQuoteQuery
{
    Task<Poquote> MapToEntity(POQuoteView inputObject);
    Task<List<Poquote>> MapToEntity(List<POQuoteView> inputObjects);
    Task<POQuoteView> MapToView(Poquote inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Poquote> GetEntityById(long  ? poQuoteId);
    Task<Poquote> GetEntityByNumber(long poQuoteNumber);
    Task<POQuoteView> GetViewById(long ? poQuoteId);
    Task<POQuoteView> GetViewByNumber(long poQuoteNumber);
}
