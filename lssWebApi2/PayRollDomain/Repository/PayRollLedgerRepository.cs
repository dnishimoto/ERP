

using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ERP_Core2.PayRollDomain
{
    public class PayRollLedgerView
    {
        public long PayRollLedgerId { get; set; }
        public long EmployeeId { get; set; }
        public long PayRollTransactionCode { get; set; }
        public string PayRollType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime PayPeriodBegin { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public int PayRollGroupCode { get; set; }
        public string ReversingEntry { get; set; }
        public string UpdateEntry { get; set; }
        public long PayRollLedgerNumber { get; set; }
        public long PaySequence { get; set; }
        public string TransactionType { get; set; }

    }
    public class PayRollLedgerRepository : Repository<PayRollLedger>, IPayRollLedgerRepository
    {
        ListensoftwaredbContext _dbContext;
        public PayRollLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<PayRollLedger> GetEntityById(long payRollLedgerId)
        {
            return await _dbContext.FindAsync<PayRollLedger>(payRollLedgerId);
        }
        public async Task<PayRollLedger> GetEntityByNumber(long payRollLedgerNumber)
        {
            var query = await (from detail in _dbContext.PayRollLedger
                               where detail.PayRollLedgerNumber == payRollLedgerNumber
                               select detail).FirstOrDefaultAsync<PayRollLedger>();
            return query;
        }

        public async Task<List<PayRollLedger>> GetEntitiesByPaySequence(Expression<Func<PayRollLedger, bool>> predicate, string include)
        {
            try
            {
                var resultList = base.GetObjectsQueryable(predicate, include);

                List<PayRollLedger> list = new List<PayRollLedger>();
                await resultList.ForEachAsync(e => list.Add(e));
                //foreach (var item in resultList)
                //{
                //    list.Add(item);
                //}
                await Task.Yield();
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

    }
}
