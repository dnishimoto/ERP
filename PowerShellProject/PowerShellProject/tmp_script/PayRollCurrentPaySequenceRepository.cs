   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ERP_Core2.PayRollDomain
{
 public class PayRollCurrentPaySequenceRepository: Repository<PayRollCurrentPaySequence>, IPayRollCurrentPaySequenceRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollCurrentPaySequenceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollCurrentPaySequence>GetEntityById(long payRollCurrentPaySequenceId)
        {
            return await _dbContext.FindAsync<PayRollCurrentPaySequence>(payRollCurrentPaySequenceId);
        }
         public async Task<PayRollCurrentPaySequence> GetEntityByNumber(long payRollCurrentPaySequenceNumber)
        {
            var query = await (from detail in _dbContext.PayRollCurrentPaySequence
                               where detail.PayRollCurrentPaySequenceNumber == payRollCurrentPaySequenceNumber
                               select detail).FirstOrDefaultAsync<PayRollCurrentPaySequence>();
            return query;
        }

  }
}
