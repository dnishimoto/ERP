   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.CompanyDomain
{
    public class CompanyView
    {
      
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipcode { get; set; }
        public string TaxCode1 { get; set; }
        public string TaxCode2 { get; set; }
        public long CompanyNumber { get; set; }

    
    }
    public class CompanyRepository: Repository<Company>, ICompanyRepository
    {
        ListensoftwaredbContext _dbContext;
        public CompanyRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<Company> GetCompany()
        {
            try
            {
           

                Company company = await (from e in _dbContext.Company
                                         where e.CompanyId == 1
                                         select e).FirstOrDefaultAsync<Company>();

                return company;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Company>GetEntityById(long ? companyId)
        {
			try{
            return await _dbContext.FindAsync<Company>(companyId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Company> GetEntityByNumber(long companyNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Company
                               where detail.CompanyNumber == companyNumber
                               select detail).FirstOrDefaultAsync<Company>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		

		
  }
}
