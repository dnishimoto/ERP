   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionControlView
    {
        public long PayRollTransactionControlId { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public string PayRollType { get; set; }
        public decimal? RateAmount { get; set; }
        public string RateType { get; set; }
        public int PayRollTransactionCode { get; set; }
        public decimal? UpperLimit1 { get; set; }
        public decimal? UpperLimit2 { get; set; }
        public long PayRollTransactionControlNumber { get; set; }

    }
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
