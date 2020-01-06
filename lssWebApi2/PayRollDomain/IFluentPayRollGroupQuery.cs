using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public interface IFluentPayRollGroupQuery
    {
        Task<PayRollGroup> MapToEntity(PayRollGroupView inputObject);
        Task<IList<PayRollGroup>> MapToEntity(IList<PayRollGroupView> inputObjects);
        Task<PayRollGroupView> MapToView(PayRollGroup inputObject);
        Task<NextNumber> GetNextNumber();
        Task<PayRollGroupView> GetViewById(long payRollGroupId);
        Task<PayRollGroup> GetEntityById(long payRollGroupId);
        Task<PayRollGroupView> GetViewByNumber(long payRollGroupNumber);
        Task<PayRollGroup> GetEntityByNumber(long payRollGroupNumber);
        Task<PayRollGroupView> GetViewByCode(int code);
    }
}
