   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ERP_Core2.PayRollDomain
{
 public class PayRollPaySequenceRepository: Repository<PayRollPaySequence>, IPayRollPaySequenceRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollPaySequenceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollPaySequence>GetEntityById(long payRollPaySequenceId)
        {
            return await _dbContext.FindAsync<PayRollPaySequence>(payRollPaySequenceId);
        }
         public async Task<PayRollPaySequence> GetEntityByNumber(longpayRollPaySequenceNumber)
        {
            var query = await (from detail in _dbContext.PayRollPaySequence
                               where detail.PayRollPaySequenceNumber == payRollPaySequenceNumber
                               select detail).FirstOrDefaultAsync<PayRollPaySequence>();
            return query;
        }

  }
}
