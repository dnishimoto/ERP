using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.AccountPayableDetailDomain
{
  public interface IFluentAccountPayableDetailQuery
  {
     Task<AccountPayableDetail> MapToEntity(AccountPayableDetailView inputObject);
     Task<IList<AccountPayableDetail>> MapToEntity(IList<AccountPayableDetailView> inputObjects);
     Task<AccountPayableDetailView> MapToView(AccountPayableDetail inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<AccountPayableDetail> GetEntityById(long ? accountPyableDetailId);
	 Task<AccountPayableDetail> GetEntityByNumber(long accountPyableDetailNumber);
	 Task<AccountPayableDetailView> GetViewById(long ? accountPyableDetailId);
	 Task<AccountPayableDetailView> GetViewByNumber(long accountPyableDetailNumber);
  }
}
