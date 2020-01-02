using lssWebApi2.AbstractFactory;
using lssWebApi2.BuyerDomain;
using lssWebApi2.CarrierDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.FluentAPI;
using lssWebApi2.SupervisorDomain;
using lssWebApi2.SupplierDomain;

namespace lssWebApi2.AddressBookDomain
{

    public class AddressBookModule 
    {

        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentEmployee Employee = new FluentEmployee();
        public FluentSupervisor Supervisor = new FluentSupervisor();
        public FluentCarrier Carrier = new FluentCarrier();
        public FluentBuyer Buyer = new FluentBuyer();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentPhone Phone = new FluentPhone();
        public FluentEmail Email = new FluentEmail();
   
    }
}
