   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{
    public class ServiceInformationInvoiceView
    {
        public long ServiceInformationInvoiceId { get; set; }
        public long ? InvoiceId { get; set; }
        public long ? ServiceId { get; set; }
        public long ServiceInformationInvoiceNumber { get; set; }
    }
    public class ServiceInformationInvoiceRepository: Repository<ServiceInformationInvoice>, IServiceInformationInvoiceRepository
    {
        ListensoftwaredbContext _dbContext;
        public ServiceInformationInvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IList<ServiceInformationInvoice>> GetEntitiesByServiceId(long? serviceId)
        {
            var query = await (from detail in _dbContext.ServiceInformationInvoice
                               where detail.ServiceId==serviceId
                               select detail).ToListAsync<ServiceInformationInvoice>();

            return query;

        }


  public async Task<ServiceInformationInvoice>GetEntityById(long ? serviceInformationInvoiceId)
        {
			try{
            return await _dbContext.FindAsync<ServiceInformationInvoice>(serviceInformationInvoiceId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<ServiceInformationInvoice> GetEntityByNumber(long serviceInformationInvoiceNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.ServiceInformationInvoice
                               where detail.ServiceInformationInvoiceNumber == serviceInformationInvoiceNumber
                               select detail).FirstOrDefaultAsync<ServiceInformationInvoice>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<ServiceInformationInvoice> FindEntityByExpression(Expression<Func<ServiceInformationInvoice, bool>> predicate)
        {
            IQueryable<ServiceInformationInvoice> result = _dbContext.Set<ServiceInformationInvoice>().Where(predicate);

            return await result.FirstOrDefaultAsync<ServiceInformationInvoice>();
        }
		  public async Task<IList<ServiceInformationInvoice>> FindEntitiesByExpression(Expression<Func<ServiceInformationInvoice, bool>> predicate)
        {
            IQueryable<ServiceInformationInvoice> result = _dbContext.Set<ServiceInformationInvoice>().Where(predicate);

            return await result.ToListAsync<ServiceInformationInvoice>();
        }
		
  }
}
