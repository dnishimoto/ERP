   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollEarningsView
    {
        public long PayRollEarningsId { get; set; }
        public int EarningCode { get; set; }
        public string Description { get; set; }
        public string EarningType { get; set; }
        public long PayRollEarningsNumber { get; set; }

    }
    public class PayRollEarningsRepository: Repository<PayRollEarnings>, IPayRollEarningsRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollEarningsRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollEarnings>GetEntityById(long payRollEarningsId)
        {
            return await _dbContext.FindAsync<PayRollEarnings>(payRollEarningsId);
        }
         public async Task<PayRollEarnings> GetEntityByNumber(long payRollEarningsNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.PayRollEarnings
                               where detail.PayRollEarningsNumber == payRollEarningsNumber
                               select detail).FirstOrDefaultAsync<PayRollEarnings>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
        public async Task<PayRollEarnings> GetEntityByEarningCode(int earningCode,string earningType)
        {
            try
            {
                var query = await (from detail in _dbContext.PayRollEarnings
                                   where detail.EarningCode == earningCode
                                   && detail.EarningType==earningType
                                   select detail).FirstOrDefaultAsync<PayRollEarnings>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }



  }
}
