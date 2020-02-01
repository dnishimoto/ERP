using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.NextNumberDomain
{
  public interface IFluentNextNumberQuery
  {
     Task<NextNumber> MapToEntity(NextNumberView inputObject);
     Task<IList<NextNumber>> MapToEntity(IList<NextNumberView> inputObjects);
     Task<NextNumberView> MapToView(NextNumber inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<NextNumber> GetEntityById(long ? nextNumberId);
	 Task<NextNumber> GetEntityByNumber(long nextNumberNumber);
	 Task<NextNumberView> GetViewById(long ? nextNumberId);
	 Task<NextNumberView> GetViewByNumber(long nextNumberNumber);
  }
}
