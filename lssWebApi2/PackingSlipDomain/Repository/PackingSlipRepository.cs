   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.PackingSlipDetailDomain;

namespace lssWebApi2.PackingSlipDomain
{
    public class PackingSlipView
    {

        public long PackingSlipId { get; set; }
        public long SupplierId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string SlipDocument { get; set; }
        public string PONumber { get; set; }
        public string Remark { get; set; }
        public string SlipType { get; set; }
        public decimal? Amount { get; set; }
        public long PackingSlipNumber { get; set; }
        public IList<PackingSlipDetailView> PackingSlipDetailViews { get; set; }
    }
    public class PackingSlipRepository: Repository<PackingSlip>, IPackingSlipRepository
    {
        ListensoftwaredbContext _dbContext;
        public PackingSlipRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<PackingSlip> GetEntityBySlipDocument(string slipDocument)
        {
            try
            {
                PackingSlip item= await (from detail in _dbContext.PackingSlip
                                         where detail.SlipDocument == slipDocument
                                         select detail).FirstOrDefaultAsync<PackingSlip>();
                return item;
                    
             
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
   
        public async Task<IList<PackingSlip>> GetEntityByPONumber(string PONumber)
        {
            try
            {
                List<PackingSlip> list = await (
                    from detail in _dbContext.PackingSlip
                    where detail.Ponumber == PONumber
                    select detail
                    ).ToListAsync<PackingSlip>();
        
                return list;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }



        public async Task<PackingSlip>GetEntityById(long ? packingSlipId)
        {
			try{
            return await _dbContext.FindAsync<PackingSlip>(packingSlipId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<PackingSlip> GetEntityByNumber(long packingSlipNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.PackingSlip
                               where detail.PackingSlipNumber == packingSlipNumber
                               select detail).FirstOrDefaultAsync<PackingSlip>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
	
		
  }
}
