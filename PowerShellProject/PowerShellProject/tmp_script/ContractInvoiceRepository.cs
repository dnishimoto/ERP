   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ContractInvoiceDomain
{
 public class ContractInvoiceRepository: Repository<ContractInvoice>, IContractInvoiceRepository
    {
        ListensoftwaredbContext _dbContext;
        public ContractInvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<ContractInvoice> GetEntitiesByExpression(Expression<Func<ContractInvoice, bool>> predicate)
         {
            var result =  _dbContext.Set<ContractInvoice>().Where(predicate);

            return result;
        }
 
  public async Task<ContractInvoice>GetEntityById(long ? contractInvoiceId)
        {
			try{
            return await _dbContext.FindAsync<ContractInvoice>(contractInvoiceId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<ContractInvoice> GetEntityByNumber(long contractInvoiceNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.ContractInvoice
                               where detail.ContractInvoiceNumber == contractInvoiceNumber
                               select detail).FirstOrDefaultAsync<ContractInvoice>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<ContractInvoice> FindEntityByExpression(Expression<Func<ContractInvoice, bool>> predicate)
        {
            IQueryable<ContractInvoice> result = _dbContext.Set<ContractInvoice>().Where(predicate);

            return await result.FirstOrDefaultAsync<ContractInvoice>();
        }
		  public async Task<IList<ContractInvoice>> FindEntitiesByExpression(Expression<Func<ContractInvoice, bool>> predicate)
        {
            IQueryable<ContractInvoice> result = _dbContext.Set<ContractInvoice>().Where(predicate);

            return await result.ToListAsync<ContractInvoice>();
        }
		
  }
}
