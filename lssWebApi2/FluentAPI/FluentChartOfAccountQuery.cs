using ERP_Core2.AbstractFactory;
using ERP_Core2.ChartOfAccountsDomain;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentChartOfAccountQuery : AbstractErrorHandling, IChartOfAccountQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentChartOfAccountQuery() { }
        public FluentChartOfAccountQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public List<ChartOfAccountView> GetChartOfAccountViewsByIds(long[] acctIds)
        {
            return _unitOfWork.chartOfAccountRepository.GetChartOfAccountsByIds(acctIds);
        }
        public List<ChartOfAccountView> GetChartOfAccountViewsByAccount(string companyNumber, string busUnit, string objectNumber, string subsidiary)
        {
            return _unitOfWork.chartOfAccountRepository.GetChartOfAccountViewsByAccount(companyNumber, busUnit, objectNumber, subsidiary);
        }
    }
}
