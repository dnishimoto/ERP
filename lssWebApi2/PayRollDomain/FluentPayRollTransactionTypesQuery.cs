using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public class FluentPayRollTransactionTypesQuery:IFluentPayRollTransactionTypesQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollTransactionTypesQuery() { }
        public FluentPayRollTransactionTypesQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollTransactionTypes> MapToEntity(PayRollTransactionTypesView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTransactionTypes outObject = mapper.Map<PayRollTransactionTypes>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<IList<PayRollTransactionTypes>> MapToEntity(IList<PayRollTransactionTypesView> inputObjects)
        {
            IList<PayRollTransactionTypes> list = new List<PayRollTransactionTypes>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollTransactionTypes outObject = mapper.Map<PayRollTransactionTypes>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollTransactionTypesView> MapToView(PayRollTransactionTypes inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTransactionTypesView outObject = mapper.Map<PayRollTransactionTypesView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollTransactionTypesRepository.GetNextNumber(TypeOfPayRoll.PayRollTransactionTypesNumber.ToString());
        }
 public async Task<PayRollTransactionTypesView> GetViewById(long payRollTransactionTypesId)
        {
            PayRollTransactionTypes detailItem = await _unitOfWork.payRollTransactionTypesRepository.GetEntityById(payRollTransactionTypesId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollTransactionTypesView> GetViewByNumber(long payRollTransactionTypesNumber)
        {
            PayRollTransactionTypes detailItem = await _unitOfWork.payRollTransactionTypesRepository.GetEntityByNumber(payRollTransactionTypesNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollTransactionTypes> GetEntityById(long payRollTransactionTypesId)
        {
            return await _unitOfWork.payRollTransactionTypesRepository.GetEntityById(payRollTransactionTypesId);

        }
 public async Task<PayRollTransactionTypes> GetEntityByNumber(long payRollTransactionTypesNumber)
        {
            return await _unitOfWork.payRollTransactionTypesRepository.GetEntityByNumber(payRollTransactionTypesNumber);
        }
}
}
