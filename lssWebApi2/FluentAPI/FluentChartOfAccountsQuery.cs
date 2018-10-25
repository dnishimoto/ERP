using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentChartOfAccountsQuery : AbstractErrorHandling, IChartOfAccountsQuery
    {
        UnitOfWork _unitOfWork = null;

        public FluentChartOfAccountsQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public ChartOfAccts GetChartofAccount(string company, string busUnit, string objectNumber, string subsidiary)
        {
            Task<ChartOfAccts> coaTask = Task.Run(async () => await _unitOfWork.chartOfAccountRepository.GetChartofAccount(company, busUnit, objectNumber, subsidiary));
            Task.WaitAll(coaTask);
            return coaTask.Result;
        }

    }
}
