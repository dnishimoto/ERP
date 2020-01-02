using lssWebApi2.AddressBookDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentEmailQuery
{
    Task<EmailEntity> MapToEntity(EmailEntityView inputObject);
    Task<List<EmailEntity>> MapToEntity(List<EmailEntityView> inputObjects);
    Task<EmailEntityView> MapToView(EmailEntity inputObject);
    Task<NextNumber> GetNextNumber();
    Task<EmailEntity> GetEntityById(long ? emailId);
    Task<EmailEntity> GetEntityByNumber(long emailNumber);
    Task<EmailEntityView> GetViewById(long ? emailId);
    Task<EmailEntityView> GetViewByNumber(long emailNumber);
    Task<IList<EmailEntityView>> GetEmailsViewsByCustomerId(long? customerId);
    Task<IList<EmailEntity>> GetEmailsByAddressId(long addressId);
}
