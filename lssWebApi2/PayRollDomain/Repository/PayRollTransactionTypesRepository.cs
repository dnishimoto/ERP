   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ERP_Core2.PayRollDomain
{
    public class PayRollTransactionTypesView
    {
        public long PayRollTransactionTypesId { get; set; }
        public int PayRollTranactionCode { get; set; }
        public string Description { get; set; }
        public long PayRollTransactionTypesNumber { get; set; }
    }
    public class PayRollTransactionTypesRepository: Repository<PayRollTransactionTypes>, IPayRollTransactionTypesRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollTransactionTypesRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollTransactionTypes>GetEntityById(long payRollTransactionTypesId)
        {
            return await _dbContext.FindAsync<PayRollTransactionTypes>(payRollTransactionTypesId);
        }
         public async Task<PayRollTransactionTypes> GetEntityByNumber(long payRollTransactionTypesNumber)
        {
            var query = await (from detail in _dbContext.PayRollTransactionTypes
                               where detail.PayRollTransactionTypesNumber == payRollTransactionTypesNumber
                               select detail).FirstOrDefaultAsync<PayRollTransactionTypes>();
            return query;
        }

  }
}
