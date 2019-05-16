using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentSalesOrderQuery:IFluentSalesOrderQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSalesOrderQuery() { }
        public FluentSalesOrderQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
    }
}
