using lssWebApi2.entityframework;
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
    }
}
