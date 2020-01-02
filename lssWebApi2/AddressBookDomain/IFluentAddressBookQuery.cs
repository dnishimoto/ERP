using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.Interfaces
{
    public interface IFluentAddressBookQuery
    {
       
        Task<List<AddressBookView>> GetAddressBookByName(string namePattern);
        Task<AddressBook> GetEntityById(long ? addressId);
        Task<AddressBookView> GetViewById(long? addressId);
        IQueryable<AddressBook> GetAddressBooksByExpression(Expression<Func<AddressBook, bool>> predicate);
        Task<long> GetAddressIdByCustomerId(long? customerId);
        Task<AddressBook> GetAddressBookbyEmail(string email);
        Task<AddressBook> MapToEntity(AddressBookView inputObject);
        Task<List<AddressBook>> MapToEntity(List<AddressBookView> inputObjects);
        Task<AddressBookView> MapToView(AddressBook inputObject);
        Task<PageListViewContainer<AddressBookView>> GetViewsByPage(Expression<Func<AddressBook, bool>> predicate, Expression<Func<AddressBook, object>> order, int pageSize, int pageNumber);
    }
}
