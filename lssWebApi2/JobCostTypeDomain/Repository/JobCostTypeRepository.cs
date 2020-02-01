   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.JobCostTypeDomain
{
    public partial class JobCostTypeView
    {
        public long JobCostTypeId { get; set; }
        public string CostCode { get; set; }
        public string Description { get; set; }
        public string Account { get; set; }
        public long JobCostTypeNumber { get; set; }
    }

    public class JobCostTypeRepository: Repository<JobCostType>, IJobCostTypeRepository
    {
        ListensoftwaredbContext _dbContext;
        public JobCostTypeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<JobCostType> GetEntitiesByExpression(Expression<Func<JobCostType, bool>> predicate)
         {
            var result =  _dbContext.Set<JobCostType>().Where(predicate);

            return result;
        }
 
  public async Task<JobCostType>GetEntityById(long ? jobCostTypeId)
        {
			try{
            return await _dbContext.FindAsync<JobCostType>(jobCostTypeId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<JobCostType> GetEntityByNumber(long jobCostTypeNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.JobCostType
                               where detail.JobCostTypeNumber == jobCostTypeNumber
                               select detail).FirstOrDefaultAsync<JobCostType>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<JobCostType> FindEntityByExpression(Expression<Func<JobCostType, bool>> predicate)
        {
            IQueryable<JobCostType> result = _dbContext.Set<JobCostType>().Where(predicate);

            return await result.FirstOrDefaultAsync<JobCostType>();
        }
		  public async Task<IList<JobCostType>> FindEntitiesByExpression(Expression<Func<JobCostType, bool>> predicate)
        {
            IQueryable<JobCostType> result = _dbContext.Set<JobCostType>().Where(predicate);

            return await result.ToListAsync<JobCostType>();
        }
		
  }
}
