using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain.Repository
{
    public interface IChartOfAccountRepository
    {
        Task<IList<ChartOfAccount>> GetEntitiesByAccount(string companyNumber, string busUnit, string objectNumber, string subsidiary);
        Task<IList<ChartOfAccount>> GetEntitiesByIds(long[] accountIds);
        Task<ChartOfAccount> GetEntityById(long ? accountId);
        Task<ChartOfAccount> GetEntityByNumber(long chartOfAccountNumber);
        Task<ChartOfAccount> GetChartofAccount(string companyCode, string busUnit, string objectNumber, string subsidiary);


    }
}
