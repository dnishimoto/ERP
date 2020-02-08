using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableDomain
{
    public class FluentAccountReceivableFeeQuery : MapperAbstract<AccountReceivableFee, AccountReceivableFeeView>, IFluentAccountReceivableFeeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAccountReceivableFeeQuery() { }
        public FluentAccountReceivableFeeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<AccountReceivableFee> MapToEntity(AccountReceivableFeeView inputObject)
        {
            AccountReceivableFee outObject = mapper.Map<AccountReceivableFee>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<AccountReceivableFee>> MapToEntity(IList<AccountReceivableFeeView> inputObjects)
        {
            List<AccountReceivableFee> list = new List<AccountReceivableFee>();
            foreach (var item in inputObjects)
            {
                AccountReceivableFee outObject = mapper.Map<AccountReceivableFee>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<AccountReceivableFeeView> MapToView(AccountReceivableFee inputObject)
        {

            AccountReceivableFeeView outObject = mapper.Map<AccountReceivableFeeView>(inputObject);

            AddressBook addressBook = null;
            Task<Customer> customerTask = _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<AccountReceivable> accountReceivableTask = _unitOfWork.accountReceivableRepository.GetEntityById(inputObject.AccountReceivableId);
            Task.WaitAll(customerTask, accountReceivableTask);

            if (customerTask.Result != null) addressBook = await _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result.AddressId);

            outObject.CustomerName = addressBook?.Name;

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfAccountReceivableFee.AccountReceivableFeeNumber.ToString());
        }
        public override async Task<AccountReceivableFeeView> GetViewById(long? accountReceivableFeeId)
        {
            AccountReceivableFee detailItem = await _unitOfWork.accountReceivableFeeRepository.GetEntityById(accountReceivableFeeId);

            return await MapToView(detailItem);
        }
        public async Task<AccountReceivableFeeView> GetViewByNumber(long accountReceivableFeeNumber)
        {
            AccountReceivableFee detailItem = await _unitOfWork.accountReceivableFeeRepository.GetEntityByNumber(accountReceivableFeeNumber);

            return await MapToView(detailItem);
        }

        public override async Task<AccountReceivableFee> GetEntityById(long? accountReceivableFeeId)
        {
            return await _unitOfWork.accountReceivableFeeRepository.GetEntityById(accountReceivableFeeId);

        }
        public async Task<AccountReceivableFee> GetEntityByNumber(long accountReceivableFeeNumber)
        {
            return await _unitOfWork.accountReceivableFeeRepository.GetEntityByNumber(accountReceivableFeeNumber);
        }
    }
}
