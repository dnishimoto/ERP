   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ERP_Core2.PayRollDomain
{
 public class PayRollTransactionControlRepository: Repository<PayRollTransactionControl>, IPayRollTransactionControlRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollTransactionControlRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollTransactionControl>GetEntityById(long payRollTransactionControlId)
        {
            return await _dbContext.FindAsync<PayRollTransactionControl>(payRollTransactionControlId);
        }
         public async Task<PayRollTransactionControl> GetEntityByNumber(long payRollTransactionControlNumber)
        {
            var query = await (from detail in _dbContext.PayRollTransactionControl
                               where detail.PayRollTransactionControlNumber == payRollTransactionControlNumber
                               select detail).FirstOrDefaultAsync<PayRollTransactionControl>();
            return query;
        }

  }
}
