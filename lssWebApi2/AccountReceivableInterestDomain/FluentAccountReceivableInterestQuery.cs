using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableInterestDomain
{
    public class FluentAccountReceivableInterestQuery : MapperAbstract<AccountReceivableInterest, AccountReceivableInterestView>, IFluentAccountReceivableInterestQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAccountReceivableInterestQuery() { }
        public FluentAccountReceivableInterestQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<AccountReceivableInterest> MapToEntity(AccountReceivableInterestView inputObject)
        {
            AccountReceivableInterest outObject = mapper.Map<AccountReceivableInterest>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<AccountReceivableInterest>> MapToEntity(IList<AccountReceivableInterestView> inputObjects)
        {
            IList<AccountReceivableInterest> list = new List<AccountReceivableInterest>();
            foreach (var item in inputObjects)
            {
                AccountReceivableInterest outObject = mapper.Map<AccountReceivableInterest>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<AccountReceivableInterestView> MapToView(AccountReceivableInterest inputObject)
        {
            AccountReceivableInterestView outObject = mapper.Map<AccountReceivableInterestView>(inputObject);
            AddressBook addressBook = null;
            Task<Customer> customerTask = _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<AccountReceivable> accountReceivableTask = _unitOfWork.accountReceivableRepository.GetEntityById(inputObject.AcctRecId);
            Task.WaitAll(customerTask, accountReceivableTask);

            if (customerTask.Result != null) addressBook = await _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result.AddressId);

            outObject.CustomerName = addressBook?.Name;
            return outObject;
        }
        public override async Task<AccountReceivableInterestView> GetViewById(long? accountReceivableInterestId)
        {
            AccountReceivableInterest detailItem = await _unitOfWork.accountReceivableInterestRepository.GetEntityById(accountReceivableInterestId);

            return await MapToView(detailItem);
        }

        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.accountReceivableInterestRepository.GetNextNumber(TypeOfAccountReceivableInterest.AccountReceivableInterestNumber.ToString());
        }

        public async Task<AccountReceivableInterestView> GetViewByNumber(long accountReceivableInterestNumber)
        {
            AccountReceivableInterest detailItem = await _unitOfWork.accountReceivableInterestRepository.GetEntityByNumber(accountReceivableInterestNumber);

            return await MapToView(detailItem);
        }

        public override async Task<AccountReceivableInterest> GetEntityById(long? accountReceivableInterestId)
        {
            return await _unitOfWork.accountReceivableInterestRepository.GetEntityById(accountReceivableInterestId);

        }
        public async Task<AccountReceivableInterest> GetEntityByNumber(long accountReceivableInterestNumber)
        {
            return await _unitOfWork.accountReceivableInterestRepository.GetEntityByNumber(accountReceivableInterestNumber);
        }
    }
}
