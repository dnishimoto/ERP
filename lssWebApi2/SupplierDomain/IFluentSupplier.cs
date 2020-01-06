

using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.SupplierDomain
{

    public interface IFluentSupplier
    {
        IFluentSupplierQuery Query();
        IFluentSupplier Apply();
        IFluentSupplier AddSupplier(Supplier supplier);
        IFluentSupplier UpdateSupplier(Supplier supplier);
        IFluentSupplier DeleteSupplier(Supplier supplier);
        IFluentSupplier UpdateSuppliers(IList<Supplier> newObjects);
        IFluentSupplier AddSuppliers(List<Supplier> newObjects);
        IFluentSupplier DeleteSuppliers(List<Supplier> deleteObjects);
        IFluentSupplier CreateSupplier(Supplier supplier);
    }
}
