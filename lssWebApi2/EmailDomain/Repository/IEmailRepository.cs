

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;


namespace lssWebApi2.AddressBookDomain
{
public interface IEmailRepository
    {
        Task<EmailEntity> GetEntityById(long ? emailId);
        Task<EmailEntity> FindAccountEmailbyAddressId(long ? addressId, string email);
        Task<IList<EmailEntity>> GetEmailByCustomerId(long? customerId);
        Task<IList<EmailEntity>> GetEmailsByAddressId(long addressId);
    }
}
