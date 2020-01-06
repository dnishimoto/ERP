using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.AccountPayableDomain
{
    public class FluentAccountPayableQuery : MapperAbstract<AccountPayable, AccountPayableView>, IFluentAccountPayableQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAccountPayableQuery() { }
        public FluentAccountPayableQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<AccountPayable> MapToEntity(AccountPayableView inputObject)
        {

            AccountPayable outObject = mapper.Map<AccountPayable>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<AccountPayable>> MapToEntity(IList<AccountPayableView> inputObjects)
        {
            IList<AccountPayable> list = new List<AccountPayable>();

            foreach (var item in inputObjects)
            {
                AccountPayable outObject = mapper.Map<AccountPayable>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<AccountPayableView> MapToView(AccountPayable inputObject)
        {

            AccountPayableView outObject = mapper.Map<AccountPayableView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<AccountPayable> GetAcctPayByPONumber(string poNumber)
        {
            AccountPayable acctPay = await _unitOfWork.accountPayableRepository.GetAcctPayByPONumber(poNumber);
            return acctPay;
        }

        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.accountPayableRepository.GetNextNumber(TypeOfAccountPayable.AccountPayableNumber.ToString());
        }
        public override async Task<AccountPayableView> GetViewById(long? accountPayableId)
        {
            AccountPayable detailItem = await _unitOfWork.accountPayableRepository.GetEntityById(accountPayableId);

            return await MapToView(detailItem);
        }
        public async Task<AccountPayableView> GetViewByNumber(long accountPayableNumber)
        {
            AccountPayable detailItem = await _unitOfWork.accountPayableRepository.GetEntityByNumber(accountPayableNumber);

            return await MapToView(detailItem);
        }

        public override async Task<AccountPayable> GetEntityById(long? accountPayableId)
        {
            return await _unitOfWork.accountPayableRepository.GetEntityById(accountPayableId);

        }
        public async Task<AccountPayable> GetEntityByNumber(long accountPayableNumber)
        {
            return await _unitOfWork.accountPayableRepository.GetEntityByNumber(accountPayableNumber);
        }
    }
}
