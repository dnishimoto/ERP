using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public class FluentPayRollTotalsQuery:IFluentPayRollTotalsQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollTotalsQuery() { }
        public FluentPayRollTotalsQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollTotals> MapToEntity(PayRollTotalsView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTotals outObject = mapper.Map<PayRollTotals>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<PayRollTotals>> MapToEntity(List<PayRollTotalsView> inputObjects)
        {
            List<PayRollTotals> list = new List<PayRollTotals>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollTotals outObject = mapper.Map<PayRollTotals>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollTotalsView> MapToView(PayRollTotals inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTotalsView outObject = mapper.Map<PayRollTotalsView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollTotalsRepository.GetNextNumber('PayRollTotalsNumber');
        }
 public async Task<PayRollTotalsView> GetViewById(long payRollTotalsId)
        {
            PayRollTotals detailItem = await _unitOfWork.payRollTotalsRepository.GetEntityById(payRollTotalsId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollTotalsView> GetViewByNumber(long payRollTotalsNumber)
        {
            PayRollTotals detailItem = await _unitOfWork.payRollTotalsRepository.GetEntityByNumber(payRollTotalsNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollTotals> GetEntityById(long payRollTotalsId)
        {
            return await _unitOfWork.payRollTotalsRepository.GetEntityById(payRollTotalsId);

        }
 public async Task<PayRollTotals> GetEntityByNumber(long payRollTotalsNumber)
        {
            return await _unitOfWork.payRollTotalsRepository.GetEntityByNumber(payRollTotalsNumber);
        }
}
}
