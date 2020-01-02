   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.PackingSlipDetailDomain
{
    public class PackingSlipDetailView
    {
       
        public long PackingSlipDetailId { get; set; }
        public long PackingSlipId { get; set; }
        public long ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ExtendedCost { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }
        public long PackingSlipDetailNumber { get; set; }

        public string ItemCode { get; set; }
        public string Branch { get; set; }
        public string ItemDescription { get; set; }
    }
    public class PackingSlipDetailRepository: Repository<PackingSlipDetail>, IPackingSlipDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public PackingSlipDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IList<PackingSlipDetail>> GetEntitiesByPackingSlipId(long? packingSlipId)
        {
            IList<PackingSlipDetail> list = await (from detail in _dbContext.PackingSlipDetail
                               where detail.PackingSlipId == packingSlipId
                               select detail).ToListAsync<PackingSlipDetail>();
            return list;

        }
  public async Task<PackingSlipDetail>GetEntityById(long ? packingSlipDetailId)
        {
			try{
            return await _dbContext.FindAsync<PackingSlipDetail>(packingSlipDetailId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<PackingSlipDetail> GetEntityByNumber(long packingSlipDetailNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.PackingSlipDetail
                               where detail.PackingSlipDetailNumber == packingSlipDetailNumber
                               select detail).FirstOrDefaultAsync<PackingSlipDetail>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<IList<PackingSlipDetail>> FindByExpression(Expression<Func<PackingSlipDetail, bool>> predicate)
        {
            try
            {
                IQueryable<PackingSlipDetail> query= _dbContext.Set<PackingSlipDetail>().Where(predicate);
            IList<PackingSlipDetail> list = new List<PackingSlipDetail>();
            foreach (var item in query) { list.Add(item); }
            await Task.Yield();
            return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
  }
}
