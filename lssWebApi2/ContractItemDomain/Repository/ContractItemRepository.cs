   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ContractItemDomain
{
    public class ContractItemView
    {
        public long ContractItemId { get; set; }
        public long ContractId { get; set; }
        public string Wbs { get; set; }
        public string ItemDescription { get; set; }
        public string UnitOfMeasure { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ExtendedCost { get; set; }
        public decimal? Fees { get; set; }
        public string ContractType { get; set; }
        public string PaymentMethod { get; set; }
        public int? DurationHours { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public long ContractItemNumber { get; set; }

    }
    public class ContractItemRepository: Repository<ContractItem>, IContractItemRepository
    {
        ListensoftwaredbContext _dbContext;
        public ContractItemRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<ContractItem> GetEntitiesByExpression(Expression<Func<ContractItem, bool>> predicate)
         {
            var result =  _dbContext.Set<ContractItem>().Where(predicate);

            return result;
        }
 
  public async Task<ContractItem>GetEntityById(long ? contractItemId)
        {
			try{
            return await _dbContext.FindAsync<ContractItem>(contractItemId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<ContractItem> GetEntityByNumber(long contractItemNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.ContractItem
                               where detail.ContractItemNumber == contractItemNumber
                               select detail).FirstOrDefaultAsync<ContractItem>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<ContractItem> FindEntityByExpression(Expression<Func<ContractItem, bool>> predicate)
        {
            IQueryable<ContractItem> result = _dbContext.Set<ContractItem>().Where(predicate);

            return await result.FirstOrDefaultAsync<ContractItem>();
        }
		  public async Task<IList<ContractItem>> FindEntitiesByExpression(Expression<Func<ContractItem, bool>> predicate)
        {
            IQueryable<ContractItem> result = _dbContext.Set<ContractItem>().Where(predicate);

            return await result.ToListAsync<ContractItem>();
        }
		
  }
}
