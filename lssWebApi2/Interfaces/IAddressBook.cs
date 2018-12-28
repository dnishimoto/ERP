using ERP_Core2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.Interfaces
{
    public interface IAddressBook
    {
        IAddressBook Apply();
        IAddressBookQuery Query();
        IAddressBook UpdateAddressBook(AddressBook addressBook);
        IAddressBook CreateAddressBook(AddressBook addressBook);
        IAddressBook DeleteAddressBook(AddressBook addressBook);
        IAddressBook CreateAddressBooks(List<AddressBook> list);
        IAddressBook DeleteAddressBooks(List<AddressBook> list);
        IAddressBook MapAddressBookEntity(ref AddressBook addressBook, AddressBookView addressBookView);
    }
}
