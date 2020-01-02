   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.EquipmentDomain
{
    public  class EquipmentView
    {
        public long EquipmentId { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Vin { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? CurrentAppraisalPrice { get; set; }
        public decimal? SalesPrice { get; set; }
        public string Description { get; set; }
        public string SaleOption { get; set; }
        public int? YearPurchased { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public long EquipmentNumber { get; set; }
    }
    public class EquipmentRepository: Repository<Equipment>, IEquipmentRepository
    {
        ListensoftwaredbContext _dbContext;
        public EquipmentRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<Equipment>GetEntityById(long ? equipmentId)
        {
			try{
            return await _dbContext.FindAsync<Equipment>(equipmentId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Equipment> GetEntityByNumber(long equipmentNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Equipment
                               where detail.EquipmentNumber == equipmentNumber
                               select detail).FirstOrDefaultAsync<Equipment>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<Equipment> FindEntityByExpression(Expression<Func<Equipment, bool>> predicate)
        {
            IQueryable<Equipment> result = _dbContext.Set<Equipment>().Where(predicate);

            return await result.FirstOrDefaultAsync<Equipment>();
        }
		  public async Task<IList<Equipment>> FindEntitiesByExpression(Expression<Func<Equipment, bool>> predicate)
        {
            IQueryable<Equipment> result = _dbContext.Set<Equipment>().Where(predicate);

            return await result.ToListAsync<Equipment>();
        }
		
  }
}
