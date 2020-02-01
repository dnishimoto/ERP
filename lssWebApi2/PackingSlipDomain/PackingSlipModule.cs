using lssWebApi2.AbstractFactory;
using lssWebApi2.PackingSlipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.SupplierDomain;
using lssWebApi2.Services;

namespace lssWebApi2.PackingSlipDomain
{
    public class PackingSlipModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPackingSlip PackingSlip;
        public FluentSupplier Supplier;
        public PackingSlipModule()
        {
            unitOfWork = new UnitOfWork();
            PackingSlip = new FluentPackingSlip(unitOfWork);
            Supplier = new FluentSupplier(unitOfWork);
        }

    }
}
