using lssWebApi2.AutoMapper;
using lssWebApi2.CarrierDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentCarrierQuery
{
    Task<Carrier> MapToEntity(CarrierView inputObject);
    Task<List<Carrier>> MapToEntity(List<CarrierView> inputObjects);
    Task<CarrierView> MapToView(Carrier inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Carrier> GetEntityById(long ? carrierId);
    Task<Carrier> GetEntityByNumber(long carrierNumber);
    Task<CarrierView> GetViewById(long ? carrierId);
    Task<CarrierView> GetViewByNumber(long carrierNumber);
}
