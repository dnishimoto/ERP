using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentInventory : IFluentInventory
    {
        

        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public FluentInventory()
        {

        }

        public IFluentInventoryQuery Query()
        {
            return new FluentInventoryQuery(unitOfWork) as IFluentInventoryQuery;
        }
    }
}
