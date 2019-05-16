using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAccountPayable : AbstractErrorHandling, IFluentAccountPayable
    {
        private FluentAccountPayableQuery _query = null;
        private UnitOfWork unitOfWork = new UnitOfWork();
        public IFluentAccountPayableQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentAccountPayableQuery(unitOfWork);
            }
            return _query as IFluentAccountPayableQuery;
        }
    }
}
