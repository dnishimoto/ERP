using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAccountPayable : AbstractErrorHandling, IAccountPayable
    {
        private FluentAccountPayableQuery _query = null;
        private UnitOfWork unitOfWork = new UnitOfWork();
        public IAccountPayableQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentAccountPayableQuery(unitOfWork);
            }
            return _query as IAccountPayableQuery;
        }
    }
}
