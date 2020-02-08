using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableDetailDomain
{
  public interface IFluentAccountReceivableDetailQuery
  {
     Task<AccountReceivableDetail> MapToEntity(AccountReceivableDetailView inputObject);
     Task<IList<AccountReceivableDetail>> MapToEntity(IList<AccountReceivableDetailView> inputObjects);
     Task<AccountReceivableDetailView> MapToView(AccountReceivableDetail inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<AccountReceivableDetail> GetEntityById(long ? accountReceivableDetailId);
	 Task<AccountReceivableDetail> GetEntityByNumber(long accountReceivableDetailNumber);
	 Task<AccountReceivableDetailView> GetViewById(long ? accountReceivableDetailId);
	 Task<AccountReceivableDetailView> GetViewByNumber(long accountReceivableDetailNumber);
  }
}
