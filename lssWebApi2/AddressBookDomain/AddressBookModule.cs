using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.AddressBookDomain
{

    public class AddressBookModule 
    {

        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentEmployee Employee = new FluentEmployee();
   
    }
}
