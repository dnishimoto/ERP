   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ContractDomain
{
    public class ContractView
    {
        public long ContractId { get; set; }
        public long? CustomerId { get; set; }
        public long? ServiceTypeXrefId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Cost { get; set; }
        public decimal? RemainingBalance { get; set; }
        public string Title { get; set; }
        public long ContractNumber { get; set; }

    
        public string CustomerName { get; set; }
        public string ServiceType { get; set; }
     
    }
    public class ContractRepository: Repository<Contract>, IContractRepository
    {
        ListensoftwaredbContext _dbContext;
        public ContractRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IList<Contract>> GetContractsByCustomerId(long ? customerId)
        {
            try
            {
                IList<Contract> list = await(from detail in _dbContext.Contract
                                  where detail.CustomerId == customerId
                                  select detail).ToListAsync<Contract>();
              
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<Contract>GetEntityById(long ? contractId)
        {
			try{
            return await _dbContext.FindAsync<Contract>(contractId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Contract> GetEntityByNumber(long contractNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Contract
                               where detail.ContractNumber == contractNumber
                               select detail).FirstOrDefaultAsync<Contract>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public  IQueryable<Contract> GetQueryableByCustomerId(long ? customerId)
        {
            try
            { 
            IQueryable<Contract> query = (from detail in _dbContext.Contract
                         where detail.CustomerId == customerId
                         select detail);
            return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }

       

		
  }
}
