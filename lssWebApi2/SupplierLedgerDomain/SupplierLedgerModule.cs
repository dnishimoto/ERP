using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.SupplierDomain;
using lssWebApi2.AccountPayableDomain;

namespace lssWebApi2.SupplierLedgerDomain
{
    public class SupplierLedgerModule : AbstractModule
    {
        public FluentSupplierLedger SupplierLedger = new FluentSupplierLedger();
        public FluentSupplier Supplier = new FluentSupplier();
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentAccountPayable AccountPayable = new FluentAccountPayable();
    }
}
