using lssWebApi2.AddressBookDomain;
using lssWebApi2.SupplierDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.EmailDomain;
using lssWebApi2.Services;

namespace lssWebApi2.InventoryDomain
{
    public class InventorySupplierModule
    {
        private UnitOfWork unitOfWork;
        //public FluentInventorySupplier Supplier = new FluentInventorySupplier();
        public FluentAddressBook AddressBook;
        public FluentLocationAddress LocationAddress;
        public FluentSupplier Supplier;
        public FluentEmail Email;

        public InventorySupplierModule()
        {
            unitOfWork = new UnitOfWork();
            AddressBook = new FluentAddressBook(unitOfWork);
            LocationAddress = new FluentLocationAddress(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            Email = new FluentEmail(unitOfWork);

        }


        public bool CreateSupplier(AddressBook addressBook, EmailEntity emailEntity, LocationAddress locationAddress)
        {
            try
            {
                AddressBook
                    .CreateSupplierAddressBook(addressBook, emailEntity)
                    .Apply();

                Task<AddressBook> addressBookLookupTask = Task.Run(async () => await AddressBook.Query().GetAddressBookbyEmail(emailEntity.Email));

                locationAddress.AddressId = addressBookLookupTask.Result.AddressId;

                LocationAddress

                    .AddLocationAddress(locationAddress)
                    .Apply();

                Email.CreateSupplierEmail(addressBook.AddressId, emailEntity).Apply();

                Supplier supplier = new Supplier { AddressId = addressBook.AddressId, Identification = emailEntity.Email };

                Supplier
                    .AddSupplier(supplier)
                    .Apply();
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateSupplier", ex); }
        }


    }
}
