using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public class FluentPayRollLedgerQuery:IFluentPayRollLedgerQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollLedgerQuery() { }
        public FluentPayRollLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollLedger> MapToEntity(PayRollLedgerView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollLedger outObject = mapper.Map<PayRollLedger>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<PayRollLedger>> MapToEntity(List<PayRollLedgerView> inputObjects)
        {
            List<PayRollLedger> list = new List<PayRollLedger>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollLedger outObject = mapper.Map<PayRollLedger>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollLedgerView> MapToView(PayRollLedger inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollLedgerView outObject = mapper.Map<PayRollLedgerView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollLedgerRepository.GetNextNumber(TypeOfPayRoll.PayRollLedgerNumber.ToString());
        }
 public async Task<PayRollLedgerView> GetViewById(long payRollLedgerId)
        {
            PayRollLedger detailItem = await _unitOfWork.payRollLedgerRepository.GetEntityById(payRollLedgerId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollLedgerView> GetViewByNumber(long payRollLedgerNumber)
        {
            PayRollLedger detailItem = await _unitOfWork.payRollLedgerRepository.GetEntityByNumber(payRollLedgerNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollLedger> GetEntityById(long payRollLedgerId)
        {
            return await _unitOfWork.payRollLedgerRepository.GetEntityById(payRollLedgerId);

        }
 public async Task<PayRollLedger> GetEntityByNumber(long payRollLedgerNumber)
        {
            return await _unitOfWork.payRollLedgerRepository.GetEntityByNumber(payRollLedgerNumber);
        }

        public async Task<List<PayRollLedger>> GetEntitiesByPaySequence(long employee, long paySequence)
        {
            List<PayRollLedger> list = await _unitOfWork.payRollLedgerRepository.GetEntitiesByPaySequence(e => e.EmployeeId == employee && e.PaySequence == paySequence,"");

            return list;
        }
}
}
