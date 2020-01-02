using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public class FluentPayRollTransactionsByEmployeeQuery:IFluentPayRollTransactionsByEmployeeQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPayRollTransactionsByEmployeeQuery() { }
        public FluentPayRollTransactionsByEmployeeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public async Task<PayRollTransactionsByEmployee> MapToEntity(PayRollTransactionsByEmployeeView inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTransactionsByEmployee outObject = mapper.Map<PayRollTransactionsByEmployee>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public async Task<List<PayRollTransactionsByEmployee>> MapToEntity(List<PayRollTransactionsByEmployeeView> inputObjects)
        {
            List<PayRollTransactionsByEmployee> list = new List<PayRollTransactionsByEmployee>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                PayRollTransactionsByEmployee outObject = mapper.Map<PayRollTransactionsByEmployee>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public async Task<PayRollTransactionsByEmployeeView> MapToView(PayRollTransactionsByEmployee inputObject)
        {
            Mapper mapper = new Mapper();
            PayRollTransactionsByEmployeeView outObject = mapper.Map<PayRollTransactionsByEmployeeView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.payRollTransactionsByEmployeeRepository.GetNextNumber(TypeOfPayRoll.PayRollTransactionsByEmployeeNumber.ToString());
        }
 public async Task<PayRollTransactionsByEmployeeView> GetViewById(long payRollTransactionsByEmployeeId)
        {
            PayRollTransactionsByEmployee detailItem = await _unitOfWork.payRollTransactionsByEmployeeRepository.GetEntityById(payRollTransactionsByEmployeeId);

            return await MapToView(detailItem);
        }
 public async Task<PayRollTransactionsByEmployeeView> GetViewByNumber(long payRollTransactionsByEmployeeNumber)
        {
            PayRollTransactionsByEmployee detailItem = await _unitOfWork.payRollTransactionsByEmployeeRepository.GetEntityByNumber(payRollTransactionsByEmployeeNumber);

            return await MapToView(detailItem);
        }

public async Task<PayRollTransactionsByEmployee> GetEntityById(long payRollTransactionsByEmployeeId)
        {
            return await _unitOfWork.payRollTransactionsByEmployeeRepository.GetEntityById(payRollTransactionsByEmployeeId);

        }
 public async Task<PayRollTransactionsByEmployee> GetEntityByNumber(long payRollTransactionsByEmployeeNumber)
        {
            return await _unitOfWork.payRollTransactionsByEmployeeRepository.GetEntityByNumber(payRollTransactionsByEmployeeNumber);
        }
        public async Task<List<PayRollTransactionsByEmployeeView>> GetTransactionsByEmployeeViews(long employeeId)
        {
            var query =  _unitOfWork.payRollTransactionsByEmployeeRepository.GetEntitiesByExpression(e => e.Employee == employeeId);
            List<PayRollTransactionsByEmployeeView> views = new List<PayRollTransactionsByEmployeeView>();
            foreach (var item in query) {
                views.Add(await MapToView(item));
            }
            return views;
        }
        public async Task<PayRollTransactionsByEmployee> GetEntityByEmployeeAndTransactionCodeAndType(long employee, int payRollTransactionCode, string transactionType)
        {
            return await _unitOfWork.payRollTransactionsByEmployeeRepository.GetObjectAsyncByPredicate(e => e.Employee == employee && e.PayRollTransactionCode == payRollTransactionCode && e.TransactionType == transactionType);
        }
}
}
