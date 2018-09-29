using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.AddressBookDomain
{

    class AddressBookModule : AbstractModule
    {

        public FluentAddressBook AddressBook = new FluentAddressBook();
   
    }
}
