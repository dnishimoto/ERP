using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public class FluentPayRollTransactionControlQuery:IFluentPayRollTransactionControlQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollTransactionControlQuery() { }
        public FluentPayRollTransactionControlQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollTransactionControl> MapToEntity(PayRollTransactionControlView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTransactionControl outObject = mapper.Map<PayRollTransactionControl>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<IList<PayRollTransactionControl>> MapToEntity(IList<PayRollTransactionControlView> inputObjects)
        {
            IList<PayRollTransactionControl> list = new List<PayRollTransactionControl>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollTransactionControl outObject = mapper.Map<PayRollTransactionControl>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollTransactionControlView> MapToView(PayRollTransactionControl inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTransactionControlView outObject = mapper.Map<PayRollTransactionControlView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollTransactionControlRepository.GetNextNumber(TypeOfPayRoll.PayRollTransactionControlNumber.ToString());
        }
 public async Task<PayRollTransactionControlView> GetViewById(long payRollTransactionControlId)
        {
            PayRollTransactionControl detailItem = await _unitOfWork.payRollTransactionControlRepository.GetEntityById(payRollTransactionControlId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollTransactionControlView> GetViewByNumber(long payRollTransactionControlNumber)
        {
            PayRollTransactionControl detailItem = await _unitOfWork.payRollTransactionControlRepository.GetEntityByNumber(payRollTransactionControlNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollTransactionControl> GetEntityById(long payRollTransactionControlId)
        {
            return await _unitOfWork.payRollTransactionControlRepository.GetEntityById(payRollTransactionControlId);

        }
 public async Task<PayRollTransactionControl> GetEntityByNumber(long payRollTransactionControlNumber)
        {
            return await _unitOfWork.payRollTransactionControlRepository.GetEntityByNumber(payRollTransactionControlNumber);
        }
}
}
