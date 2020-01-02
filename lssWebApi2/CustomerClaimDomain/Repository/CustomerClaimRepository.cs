   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.CustomerClaimDomain
{
    public class CustomerClaimView
    {
        public long ClaimId { get; set; }
        public long ClassificationXrefId { get; set; }
        public long CustomerId { get; set; }
        public string Configuration { get; set; }
        public string Note { get; set; }
        public long EmployeeId { get; set; }
        public long GroupIdXrefId { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ContractId { get; set; }
        public long CustomerClaimNumber { get; set; }

        public string Classification { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string GroupId { get; set; }

    }
    public class CustomerClaimRepository: Repository<CustomerClaim>, ICustomerClaimRepository
    {
        ListensoftwaredbContext _dbContext;
        public CustomerClaimRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IList<CustomerClaim>> GetEntitiesByCustomerId(long customerId)
        {
            IList<CustomerClaim> list = await (from customer in _dbContext.Customer
                                               join customerClaim in _dbContext.CustomerClaim
                                                  on customer.CustomerId equals customerClaim.CustomerId
                                               where customer.CustomerId==customerId
                                               select customerClaim
                                         ).ToListAsync<CustomerClaim>();
            return list;
        }
  public async Task<CustomerClaim>GetEntityById(long ? customerClaimId)
        {
			try{
            return await _dbContext.FindAsync<CustomerClaim>(customerClaimId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<CustomerClaim> GetEntityByNumber(long customerClaimNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.CustomerClaim
                               where detail.CustomerClaimNumber == customerClaimNumber
                               select detail).FirstOrDefaultAsync<CustomerClaim>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		

		
  }
}
