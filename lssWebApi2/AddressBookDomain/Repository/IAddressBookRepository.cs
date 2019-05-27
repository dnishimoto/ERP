using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain.Repository
{
    public interface IAddressBookRepository
    {
        void MapAddressBookEntity(ref AddressBook addressBook, AddressBookView addressBookView);
        List<Phones> GetPhonesByAddressId(long addressId);
         List<Emails> GetEmailsByAddressId(long addressId);
         Task<CreateProcessStatus> CreateAddressBook(CustomerView customerView);
         List<AddressBookView> GetAddressBookByName(string name);
         Task<AddressBookView> GetAddressBookViewByAddressId(long addressId);
         Task<AddressBook> GetAddressBookByAddressId(long addressId);
    }
}
