using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentNextNumber : AbstractErrorHandling, INextNumber
    {
        private FluentNextNumberQuery _query = null;
        private UnitOfWork unitOfWork = new UnitOfWork();
        public INextNumberQuery Query()
        {
            if (_query == null) { _query = new FluentNextNumberQuery(unitOfWork); }
            return _query as INextNumberQuery;
        }
    }
}
