using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AddressBookDomain;

using lssWebApi2.ItemMasterDomain;
using lssWebApi2.SupplierLedgerDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.EmailDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.Services;

namespace lssWebApi2.SupplierDomain
{
    public class SupplierModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentSupplier Supplier;
        public FluentGeneralLedger GeneralLedger;
        public FluentSupplierLedger SupplierLedger;
        public FluentAddressBook AddressBook;
        public FluentEmail Emails;
        public FluentAccountPayable AccountPayable;
        public FluentUdc Udc;
        public FluentChartOfAccount ChartOfAccount;
        public FluentItemMaster ItemMaster;
        public FluentLocationAddress LocationAddress;

        public SupplierModule()
        {
            unitOfWork = new UnitOfWork();
            Supplier = new FluentSupplier(unitOfWork);
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            SupplierLedger = new FluentSupplierLedger(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Emails = new FluentEmail(unitOfWork);
            AccountPayable = new FluentAccountPayable(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
            LocationAddress = new FluentLocationAddress(unitOfWork);
        }

        public bool CreateSupplierProfile(AddressBook addressBook, EmailEntity emailEntity, LocationAddress locationAddress)

        {

            try

            {

                AddressBook

                    .CreateSupplierAddressBook(addressBook, emailEntity).Apply();

                Task<AddressBook> addressBookLookupTask = Task.Run(async () => await AddressBook.Query().GetAddressBookbyEmail(emailEntity.Email));
                Task.WaitAll(addressBookLookupTask);

                LocationAddress

                    .CreateSupplierLocationAddress(addressBookLookupTask.Result.AddressId, locationAddress).Apply();

                Emails
                    .CreateEmailByAddressId(addressBookLookupTask.Result.AddressId, emailEntity).Apply();


                Supplier supplier = new Supplier { AddressId = addressBookLookupTask.Result.AddressId, Identification = emailEntity.Email };



                Supplier

                    .CreateSupplier(supplier)

                    .Apply();

                return true;

            }

            catch (Exception ex) { throw new Exception("CreateSupplier", ex); }

        }
    }
}
