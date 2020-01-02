

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollGroupView
    {
        public long PayRollGroupId { get; set; }
        public int PayRollGroupCode { get; set; }
        public string Description { get; set; }
        public long PayRollGroupNumber { get; set; }

    }
    public class PayRollGroupRepository : Repository<PayRollGroup>, IPayRollGroupRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollGroupRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<PayRollGroup> GetEntityById(long payRollGroupId)
        {
            return await _dbContext.FindAsync<PayRollGroup>(payRollGroupId);
        }
        public async Task<PayRollGroup> GetEntityByNumber(long payRollGroupNumber)
        {
            var query = await (from detail in _dbContext.PayRollGroup
                               where detail.PayRollGroupNumber == payRollGroupNumber
                               select detail).FirstOrDefaultAsync<PayRollGroup>();
            return query;
        }
        public async Task<PayRollGroup> GetEntityByCode(int code)
        {
            var query = await (from detail in _dbContext.PayRollGroup
                               where detail.PayRollGroupCode == code
                               select detail).FirstOrDefaultAsync<PayRollGroup>();
            return query;

        }

    }
    
}
