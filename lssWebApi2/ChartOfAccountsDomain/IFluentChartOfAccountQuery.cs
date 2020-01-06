using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{
    public interface IFluentChartOfAccountQuery
    {
        Task<IList<ChartOfAccountView>> GetViewsByIds(long[] accountIds);
        Task<IList<ChartOfAccountView>> GetViewsByAccount(string company, string busUnit, string objectNumber, string subsidiary);
        Task<ChartOfAccount> GetEntity(string company, string busUnit, string objectNumber, string subsidiary);
        Task<Company> GetCompany();
        Task<ChartOfAccount> GetEntityById(long ? accountId);
        Task<ChartOfAccountView> GetViewById(long? accountId);
        Task<ChartOfAccount> GetEntityByNumber(long accountNumber);
        Task<ChartOfAccount> MapToEntity(ChartOfAccountView inputObject);
        Task<IList<ChartOfAccount>> MapToEntity(IList<ChartOfAccountView> inputObjects);
        Task<ChartOfAccountView> MapToView(ChartOfAccount inputObject);
      
    }
}
