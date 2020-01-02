using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain.Repository
{
    public interface IAddressBookRepository
    {

        List<AddressBook> GetEntityByName(string name);
        Task<AddressBook> GetEntityById(long ? employeeId);
        Task<AddressBook> GetEntityByCustomerId(long? customerId);
        Task<long> GetAddressIdByCustomerId(long? customerId);
        Task<AddressBook> GetAddressBookByCustomerView(CustomerView customerView);
        Task<AddressBook> GetEntityByAccountEmail(string email);
        Task<AddressBook> FindEntityByAddressIdAndEmail(long addressId, string email);
        IQueryable<AddressBook> GetEntitiesByExpression(Expression<Func<AddressBook, bool>> predicate);
    }

}
