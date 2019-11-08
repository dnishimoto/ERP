   

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{
    public class  PayRollDeductionLiabilitiesView
    {
        public long PayRollDeductionLiabilitiesId { get; set; }
        public int DeductionLiabilitiesCode { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percentage { get; set; }
        public string Description { get; set; }
        public string DeductionLiabilitiesType { get; set; }
        public decimal? LimitAmount { get; set; }
        public long PayRollDeductionLiabilitiesNumber { get; set; }

    }
    public class PayRollDeductionLiabilitiesRepository: Repository<PayRollDeductionLiabilities>, IPayRollDeductionLiabilitiesRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollDeductionLiabilitiesRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<PayRollDeductionLiabilities>GetEntityById(long payRollDeductionLiabilitiesId)
        {
            return await _dbContext.FindAsync<PayRollDeductionLiabilities>(payRollDeductionLiabilitiesId);
        }
         public async Task<PayRollDeductionLiabilities> GetEntityByNumber(long payRollDeductionLiabilitiesNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.PayRollDeductionLiabilities
                               where detail.PayRollDeductionLiabilitiesNumber == payRollDeductionLiabilitiesNumber
                               select detail).FirstOrDefaultAsync<PayRollDeductionLiabilities>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		 public async Task<List<PayRollDeductionLiabilities>> GetObjectsQueryable(Expression<Func<PayRollDeductionLiabilities, bool>> predicate,string include)
       {
            try
            {
                var resultList = base.GetObjectsQueryable(predicate, include);

                List <PayRollDeductionLiabilities> list = new List<PayRollDeductionLiabilities>();
                foreach (var item in resultList)
                {
                    list.Add(item);
                }
                await Task.Yield();
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<PayRollDeductionLiabilities> GetEntityByDeductionLiabiltiesCode(int deductionLiabilitiesCode, string deductionLiabilitiesType)

        {
            try
            {
                var query = await (from detail in _dbContext.PayRollDeductionLiabilities
                                   where detail.DeductionLiabilitiesCode == deductionLiabilitiesCode
                                   && detail.DeductionLiabilitiesType == deductionLiabilitiesType
                                   select detail).FirstOrDefaultAsync<PayRollDeductionLiabilities>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }


        }



    }
}
