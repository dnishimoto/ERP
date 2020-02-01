using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ContractDomain
{
public class FluentContractQuery:MapperAbstract<Contract,ContractView>,IFluentContractQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentContractQuery() { }
        public FluentContractQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Contract> MapToEntity(ContractView inputObject)
        {
        
            Contract outObject = mapper.Map<Contract>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<Contract>> MapToEntity(IList<ContractView> inputObjects)
        {
            IList<Contract> list = new List<Contract>();
         
            foreach (var item in inputObjects)
            {
                Contract outObject = mapper.Map<Contract>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<ContractView> MapToView(Contract inputObject)
        {
            
            ContractView outObject = mapper.Map<ContractView>(inputObject);

            AddressBook addressBook = null;
    
            Task<Customer> customerTask =  _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<Udc> udcTask =  _unitOfWork.udcRepository.GetEntityById(inputObject.ServiceTypeXrefId);
            Task.WaitAll(customerTask, udcTask);

            if (customerTask.Result != null) addressBook = await _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result.AddressId);

            outObject.CustomerName = addressBook.Name;
            outObject.ServiceType = udcTask.Result.Value;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfContract.ContractNumber.ToString());
        }
 public override async Task<ContractView> GetViewById(long ? contractId)
        {
            Contract detailItem = await _unitOfWork.contractRepository.GetEntityById(contractId);

            return await MapToView(detailItem);
        }
 public async Task<ContractView> GetViewByNumber(long contractNumber)
        {
            Contract detailItem = await _unitOfWork.contractRepository.GetEntityByNumber(contractNumber);

            return await MapToView(detailItem);
        }

public override async Task<Contract> GetEntityById(long ? contractId)
        {
            return await _unitOfWork.contractRepository.GetEntityById(contractId);

        }
 public async Task<Contract> GetEntityByNumber(long contractNumber)
        {
            return await _unitOfWork.contractRepository.GetEntityByNumber(contractNumber);
        }
        public async Task<IList<ContractView>> GetContractsByCustomerId(long? customerId, long? contractId)
        {

            IEnumerable<Contract> contractList = null;
            IQueryable<Contract> resultList =  _unitOfWork.contractRepository.GetQueryableByCustomerId(customerId);
            IList<ContractView> list = new List<ContractView>();
            if (contractId != null)
            {
                contractList = resultList.Where(f => f.ContractId == contractId);
            }
            else
            {
                contractList = resultList;
            }
          

            foreach (var item in contractList)

            {

                list.Add(await MapToView(item));

            }

            return list;

        }
}
}
