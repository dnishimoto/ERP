using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.SupplierDomain;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.SupplierLedgerDomain
{
    public class SupplierLedgerModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentSupplierLedger SupplierLedger;
        public FluentSupplier Supplier;
        public FluentInvoice Invoice;
        public FluentGeneralLedger GeneralLedger;
        public FluentAddressBook AddressBook;
        public FluentAccountPayable AccountPayable;

        public SupplierLedgerModule()
        {
            unitOfWork = new UnitOfWork();
            SupplierLedger = new FluentSupplierLedger(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            AccountPayable = new FluentAccountPayable(unitOfWork);
        }
    }
}
