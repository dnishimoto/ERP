using ERP_Core2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.Interfaces
{
    public interface IFluentAddressBook
    {
        IFluentAddressBook Apply();
        IFluentAddressBookQuery Query();
        IFluentAddressBook UpdateAddressBook(AddressBook addressBook);
        IFluentAddressBook CreateAddressBook(AddressBook addressBook);
        IFluentAddressBook DeleteAddressBook(AddressBook addressBook);
        IFluentAddressBook CreateAddressBooks(List<AddressBook> list);
        IFluentAddressBook DeleteAddressBooks(List<AddressBook> list);
        IFluentAddressBook MapAddressBookEntity(ref AddressBook addressBook, AddressBookView addressBookView);
    }
}
