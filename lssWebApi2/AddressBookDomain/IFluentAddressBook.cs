using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.AddressBookDomain
{
    public interface IFluentAddressBook
    {
        IFluentAddressBook Apply();
        IFluentAddressBookQuery Query();
        IFluentAddressBook UpdateAddressBook(AddressBook addressBook);
        IFluentAddressBook AddAddressBook(AddressBook addressBook);
        IFluentAddressBook DeleteAddressBook(AddressBook addressBook);
        IFluentAddressBook AddAddressBooks(List<AddressBook> list);
        IFluentAddressBook UpdateAddressBooks(IList<AddressBook> newObjects);
        IFluentAddressBook DeleteAddressBooks(List<AddressBook> list);
        IFluentAddressBook UpdateAddressBookByView(AddressBookView view);
        IFluentAddressBook CreateSupplierAddressBook(AddressBook addressBook, EmailEntity email);
    }
}
