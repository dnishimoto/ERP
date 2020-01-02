using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public class FluentPayRollDeductionLiabilitiesQuery:IFluentPayRollDeductionLiabilitiesQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollDeductionLiabilitiesQuery() { }
        public FluentPayRollDeductionLiabilitiesQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollDeductionLiabilities> MapToEntity(PayRollDeductionLiabilitiesView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollDeductionLiabilities outObject = mapper.Map<PayRollDeductionLiabilities>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<PayRollDeductionLiabilities>> MapToEntity(List<PayRollDeductionLiabilitiesView> inputObjects)
        {
            List<PayRollDeductionLiabilities> list = new List<PayRollDeductionLiabilities>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollDeductionLiabilities outObject = mapper.Map<PayRollDeductionLiabilities>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollDeductionLiabilitiesView> MapToView(PayRollDeductionLiabilities inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollDeductionLiabilitiesView outObject = mapper.Map<PayRollDeductionLiabilitiesView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollDeductionLiabilitiesRepository.GetNextNumber(TypeOfPayRoll.PayRollDeductionLiabilitiesNumber.ToString());
        }
 public async Task<PayRollDeductionLiabilitiesView> GetViewById(long payRollDeductionLiabilitiesId)
        {
            PayRollDeductionLiabilities detailItem = await _unitOfWork.payRollDeductionLiabilitiesRepository.GetEntityById(payRollDeductionLiabilitiesId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollDeductionLiabilitiesView> GetViewByNumber(long payRollDeductionLiabilitiesNumber)
        {
            PayRollDeductionLiabilities detailItem = await _unitOfWork.payRollDeductionLiabilitiesRepository.GetEntityByNumber(payRollDeductionLiabilitiesNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollDeductionLiabilities> GetEntityById(long payRollDeductionLiabilitiesId)
        {
            return await _unitOfWork.payRollDeductionLiabilitiesRepository.GetEntityById(payRollDeductionLiabilitiesId);

        }
 public async Task<PayRollDeductionLiabilities> GetEntityByNumber(long payRollDeductionLiabilitiesNumber)
        {
            return await _unitOfWork.payRollDeductionLiabilitiesRepository.GetEntityByNumber(payRollDeductionLiabilitiesNumber);
        }
        public async Task<PayRollDeductionLiabilitiesView> GetViewByDeductionLiabilitiesCode(int deductionLiabilitiesCode, string deductionLiabilitiesType)
        {

            return await MapToView(await _unitOfWork.payRollDeductionLiabilitiesRepository.GetEntityByDeductionLiabiltiesCode(deductionLiabilitiesCode, deductionLiabilitiesType));
        }
    }
}
