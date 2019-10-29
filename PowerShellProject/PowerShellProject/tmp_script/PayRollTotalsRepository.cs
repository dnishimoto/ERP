   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ERP_Core2.PayRollDomain
{
 public class PayRollTotalsRepository: Repository<PayRollTotals>, IPayRollTotalsRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollTotalsRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollTotals>GetEntityById(long payRollTotalsId)
        {
            return await _dbContext.FindAsync<PayRollTotals>(payRollTotalsId);
        }
         public async Task<PayRollTotals> GetEntityByNumber(long payRollTotalsNumber)
        {
            var query = await (from detail in _dbContext.PayRollTotals
                               where detail.PayRollTotalsNumber == payRollTotalsNumber
                               select detail).FirstOrDefaultAsync<PayRollTotals>();
            return query;
        }

  }
}
