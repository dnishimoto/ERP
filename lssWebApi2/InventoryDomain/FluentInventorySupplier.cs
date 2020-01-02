using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class FluentInventorySupplier : AbstractErrorHandling, IFluentInventorySupplier
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected CreateProcessStatus processStatus;

        protected FluentInventorySupplierQuery _query = null;
        public IFluentInventorySupplierQuery Query()
        {
            if (_query == null) { _query = new FluentInventorySupplierQuery(unitOfWork); }
            return _query as IFluentInventorySupplierQuery;
        }
       
      

    }
}
