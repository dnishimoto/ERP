using lssWebApi2.AutoMapper;
using lssWebApi2.BuyerDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentBuyerQuery
{
    Task<Buyer> MapToEntity(BuyerView inputObject);
    Task<List<Buyer>> MapToEntity(List<BuyerView> inputObjects);
    Task<BuyerView> MapToView(Buyer inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Buyer> GetEntityById(long ? buyerId);
    Task<Buyer> GetEntityByNumber(long buyerNumber);
    Task<BuyerView> GetViewById(long ? buyerId);
    Task<BuyerView> GetViewByNumber(long buyerNumber);
}
