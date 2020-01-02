   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.ContractDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.ServiceInformationDomain
{
    public partial class ServiceInformationView
    {
       

        public long ServiceId { get; set; }
        public string ServiceDescription { get; set; }
        public decimal? Price { get; set; }
        public string AddOns { get; set; }
        public long? ServiceTypeXrefId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long LocationId { get; set; }
        public long CustomerId { get; set; }
        public long ContractId { get; set; }
        public int? SquareFeetOfStructure { get; set; }
        public string LocationDescription { get; set; }
        public string LocationGps { get; set; }
        public string Comments { get; set; }
        public bool Status { get; set; }
        public long ServiceInformationNumber { get; set; }

        public string ServiceType { get; set; }
        public  ContractView vwContract { get; set; }
        public  CustomerView  vwCustomer { get; set; }
        public LocationAddressView vwLocationAddress { get; set; }
        public IList<ScheduleEventView> ScheduleEventViews { get; set; }
        public IList<ServiceInformationInvoice> ServiceInformationInvoiceViews { get; set; }

    }
    public class ServiceInformationRepository: Repository<ServiceInformation>, IServiceInformationRepository
    {
        ListensoftwaredbContext _dbContext;
        public ServiceInformationRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<ServiceInformation>GetEntityById(long ? serviceInformationId)
        {
			try{
            return await _dbContext.FindAsync<ServiceInformation>(serviceInformationId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<ServiceInformation> GetEntityByNumber(long serviceInformationNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.ServiceInformation
                               where detail.ServiceInformationNumber == serviceInformationNumber
                               select detail).FirstOrDefaultAsync<ServiceInformation>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<ServiceInformation> FindEntityByExpression(Expression<Func<ServiceInformation, bool>> predicate)
        {
            IQueryable<ServiceInformation> result = _dbContext.Set<ServiceInformation>().Where(predicate);

            return await result.FirstOrDefaultAsync<ServiceInformation>();
        }
		  public async Task<IList<ServiceInformation>> FindEntitiesByExpression(Expression<Func<ServiceInformation, bool>> predicate)
        {
            IQueryable<ServiceInformation> result = _dbContext.Set<ServiceInformation>().Where(predicate);

            return await result.ToListAsync<ServiceInformation>();
        }
		
  }
}
