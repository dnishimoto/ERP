using lssWebApi2.AddressBookDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPhoneQuery
{
    Task<PhoneEntity> MapToEntity(PhoneEntityView inputObject);
    Task<List<PhoneEntity>> MapToEntity(List<PhoneEntityView> inputObjects);
    Task<PhoneEntityView> MapToView(PhoneEntity inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PhoneEntity> GetEntityById(long ? phonesId);
    Task<PhoneEntity> GetEntityByNumber(long phonesNumber);
    Task<PhoneEntityView> GetViewById(long ? phonesId);
    Task<PhoneEntityView> GetViewByNumber(long phonesNumber);
    Task<IList<PhoneEntityView>> GetPhoneEntityViewsByCustomerId(long? customerId);
    Task<IList<PhoneEntity>> GetPhonesByAddressId(long ? addressId);
}
