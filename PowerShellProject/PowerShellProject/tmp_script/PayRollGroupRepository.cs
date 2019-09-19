   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ERP_Core2.PayRollDomain
{
 public class PayRollGroupRepository: Repository<PayRollGroup>, IPayRollGroupRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollGroupRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollGroup>GetEntityById(long payRollGroupId)
        {
            return await _dbContext.FindAsync<PayRollGroup>(payRollGroupId);
        }

  }
}
