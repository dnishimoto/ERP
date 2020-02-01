using lssWebApi2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.EmailDomain
{
    public interface IFluentEmailQuery
    {
        Task<EmailEntity> MapToEntity(EmailEntityView inputObject);
        Task<IList<EmailEntity>> MapToEntity(IList<EmailEntityView> inputObjects);
        Task<EmailEntityView> MapToView(EmailEntity inputObject);
        Task<NextNumber> GetNextNumber();
        Task<EmailEntity> GetEntityById(long? emailId);
        Task<EmailEntity> GetEntityByNumber(long emailNumber);
        Task<EmailEntityView> GetViewById(long? emailId);
        Task<EmailEntityView> GetViewByNumber(long emailNumber);
        Task<IList<EmailEntityView>> GetEmailsViewsByCustomerId(long? customerId);
        Task<IList<EmailEntity>> GetEmailsByAddressId(long addressId);
    }
}
