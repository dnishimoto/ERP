using lssWebApi2.AbstractFactory;
using lssWebApi2.PackingSlipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.SupplierDomain;

namespace lssWebApi2.PackingSlipDomain
{
    public class PackingSlipModule : AbstractModule
    {
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentSupplier Supplier = new FluentSupplier();
    }
}
