   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{
    public class PayRollTransactionsByEmployeeView
    {
        public long PayRollTransactionsByEmployeeId { get; set; }
        public long Employee { get; set; }
        public long PayRollTransactionCode { get; set; }
        public decimal Amount { get; set; }
        public decimal? TaxPercentOfGross { get; set; }
        public decimal? AdditionalAmount { get; set; }
        public int? PayRollGroupCode { get; set; }
        public int? BenefitOption { get; set; }
        public long PayRollTransactionsByEmployeeNumber { get; set; }
        public string PayRollType { get; set; }
        public string TransactionType { get; set; }
        
    }
    public class PayRollTransactionsByEmployeeRepository: Repository<PayRollTransactionsByEmployee>, IPayRollTransactionsByEmployeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollTransactionsByEmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollTransactionsByEmployee>GetEntityById(long payRollTransactionsByEmployeeId)
        {
            return await _dbContext.FindAsync<PayRollTransactionsByEmployee>(payRollTransactionsByEmployeeId);
        }
         public async Task<PayRollTransactionsByEmployee> GetEntityByNumber(long payRollTransactionsByEmployeeNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.PayRollTransactionsByEmployee
                                   where detail.PayRollTransactionsByEmployeeNumber == payRollTransactionsByEmployeeNumber
                                   select detail).FirstOrDefaultAsync<PayRollTransactionsByEmployee>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
       public async Task<List<PayRollTransactionsByEmployee>> GetObjectsQueryable(Expression<Func<PayRollTransactionsByEmployee, bool>> predicate,string include)
       {
            try
            {
                var resultList = base.GetObjectsQueryable(predicate, include);

                List < PayRollTransactionsByEmployee > list = new List<PayRollTransactionsByEmployee>();
                foreach (var item in resultList)
                {
                    list.Add(item);
                }
                await Task.Yield();
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
  }
}
