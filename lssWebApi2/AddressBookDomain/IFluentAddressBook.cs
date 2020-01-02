using lssWebApi2.AddressBookDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.Interfaces
{
    public interface IFluentAddressBook
    {
        IFluentAddressBook Apply();
        IFluentAddressBookQuery Query();
        IFluentAddressBook UpdateAddressBook(AddressBook addressBook);
        IFluentAddressBook AddAddressBook(AddressBook addressBook);
        IFluentAddressBook DeleteAddressBook(AddressBook addressBook);
        IFluentAddressBook AddAddressBooks(List<AddressBook> list);
        IFluentAddressBook DeleteAddressBooks(List<AddressBook> list);
        IFluentAddressBook UpdateAddressBookByView(AddressBookView view);
        IFluentAddressBook CreateSupplierAddressBook(AddressBook addressBook, EmailEntity email);
    }
}
