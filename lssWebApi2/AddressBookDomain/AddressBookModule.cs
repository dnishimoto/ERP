using lssWebApi2.AbstractFactory;
using lssWebApi2.BuyerDomain;
using lssWebApi2.CarrierDomain;
using lssWebApi2.EmailDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.Services;
using lssWebApi2.SupervisorDomain;
using lssWebApi2.SupplierDomain;

namespace lssWebApi2.AddressBookDomain
{

    public class AddressBookModule
    {
        private UnitOfWork unitOfWork;

        public FluentAddressBook AddressBook;
        public FluentEmployee Employee;
        public FluentSupervisor Supervisor;
        public FluentCarrier Carrier;
        public FluentBuyer Buyer;
        public FluentSupplier Supplier;
        public FluentPhone Phone;
        public FluentEmail Email;
        public AddressBookModule()
        {
            unitOfWork = new UnitOfWork();
            AddressBook = new FluentAddressBook(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
            Supervisor = new FluentSupervisor(unitOfWork);
            Carrier = new FluentCarrier(unitOfWork);
            Buyer = new FluentBuyer(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            Phone = new FluentPhone(unitOfWork);
            Email = new FluentEmail(unitOfWork);


        }


    }
}
