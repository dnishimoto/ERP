using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public class FluentPayRollEarningsQuery:IFluentPayRollEarningsQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollEarningsQuery() { }
        public FluentPayRollEarningsQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollEarnings> MapToEntity(PayRollEarningsView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollEarnings outObject = mapper.Map<PayRollEarnings>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<PayRollEarnings>> MapToEntity(List<PayRollEarningsView> inputObjects)
        {
            List<PayRollEarnings> list = new List<PayRollEarnings>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollEarnings outObject = mapper.Map<PayRollEarnings>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollEarningsView> MapToView(PayRollEarnings inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollEarningsView outObject = mapper.Map<PayRollEarningsView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollEarningsRepository.GetNextNumber(TypeOfPayRoll.PayRollEarningsNumber.ToString());
        }
 public async Task<PayRollEarningsView> GetViewById(long payRollEarningsId)
        {
            PayRollEarnings detailItem = await _unitOfWork.payRollEarningsRepository.GetEntityById(payRollEarningsId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollEarningsView> GetViewByNumber(long payRollEarningsNumber)
        {
            PayRollEarnings detailItem = await _unitOfWork.payRollEarningsRepository.GetEntityByNumber(payRollEarningsNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollEarnings> GetEntityById(long payRollEarningsId)
        {
            return await _unitOfWork.payRollEarningsRepository.GetEntityById(payRollEarningsId);

        }
 public async Task<PayRollEarnings> GetEntityByNumber(long payRollEarningsNumber)
        {
            return await _unitOfWork.payRollEarningsRepository.GetEntityByNumber(payRollEarningsNumber);
        }
        public async Task<PayRollEarningsView> GetViewByEarningCode(int earningCode,string earningType)
        {
            return await MapToView(await _unitOfWork.payRollEarningsRepository.GetEntityByEarningCode(earningCode,earningType));
        }
}
}
