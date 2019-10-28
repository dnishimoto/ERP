   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ERP_Core2.PayRollDomain
{
    public class PayRollPaySequenceView
    {
        public long PayRollPaySequenceId { get; set; }
        public long PaySequence { get; set; }
        public DateTime PayRollBeginDate { get; set; }
        public DateTime PayRollEndDate { get; set; }
        public string Frequency { get; set; }
        public int PayRollGroupCode { get; set; }
        public long PayRollPaySequenceNumber { get; set; }

    }
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
  public async Task<PayRollPaySequence> GetEntityByNumber(long payRollPaySequenceNumber)
        {
            var query = await (from detail in _dbContext.PayRollPaySequence
                               where detail.PayRollPaySequenceNumber == payRollPaySequenceNumber
                               select detail).FirstOrDefaultAsync<PayRollPaySequence>();
            return query;
        }
        public long GetMaxSequenceNumber()
        {
            var query= (from u in _dbContext.PayRollPaySequence
             orderby u.PaySequence descending
             select u.PaySequence).Take(1);

            return query.FirstOrDefault();
            
        }
  }
}
