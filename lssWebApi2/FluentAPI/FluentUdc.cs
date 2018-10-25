using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentUDC : AbstractErrorHandling, IUDC
    {
        private FluentUDCQuery _query = null;
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IUDCQuery Query()
        {
            if (_query == null) { _query = new FluentUDCQuery(unitOfWork); }
            return _query as IUDCQuery;
        }

    }
}
