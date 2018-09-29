using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
