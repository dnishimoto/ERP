   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.UDCDomain
{
    public class UdcView
    {

        public long XrefId { get; set; }
        public string ProductCode { get; set; }
        public string KeyCode { get; set; }
        public string Value { get; set; }
        public long UdcNumber { get; set; }

    }
 public class UdcRepository: Repository<Udc>, IUdcRepository
    {
        ListensoftwaredbContext _dbContext;
        public UdcRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IQueryable<Udc>> GetUDCValuesByProductCode(string productCode)
        {
            try
            {
                var list = await (from detail in _dbContext.Udc
                                  where detail.ProductCode==productCode
                                  select detail).ToListAsync<Udc>();

                return list.AsQueryable<Udc>();
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }

        public async Task<Udc>GetEntityById(long ? udcId)
        {
			try{
            return await _dbContext.FindAsync<Udc>(udcId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Udc> GetEntityByNumber(long udcNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Udc
                               where detail.UdcNumber == udcNumber
                               select detail).FirstOrDefaultAsync<Udc>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
	

		
  }
}
