using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.ChartOfAccountsDomain.Repository;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{
    public class ChartOfAccountView
    {
        public long AccountId { get; set; }
        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string Subsidiary { get; set; }
        public string SubSub { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public string GenCode { get; set; }
        public string SubCode { get; set; }
        public string ObjectNumber { get; set; }
        public string SupCode { get; set; }
        public string ThirdAccount { get; set; }
        public string CategoryCode1 { get; set; }
        public string CategoryCode2 { get; set; }
        public string CategoryCode3 { get; set; }
        public string PostEditCode { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int Level { get; set; }
    }
    public class ChartOfAccountRepository : Repository<ChartOfAccount>, IChartOfAccountRepository
    {
        private ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public ChartOfAccountRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        
        public async Task<ChartOfAccount> GetEntityById(long ? accountId)
        {
            try
            {
                return await _dbContext.FindAsync<ChartOfAccount>(accountId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ChartOfAccount> GetEntityByNumber(long chartOfAccountNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.ChartOfAccount
                                   where detail.ChartOfAccountNumber == chartOfAccountNumber
                                   select detail).FirstOrDefaultAsync<ChartOfAccount>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ChartOfAccount> GetChartofAccount(string companyCode, string busUnit, string objectNumber, string subsidiary)
        {
            try
            {
                 ChartOfAccount chartOfAcct = await (from e in _dbContext.ChartOfAccount
                                                    where e.CompanyCode == companyCode
                                                    && e.BusUnit == busUnit
                                                    && e.ObjectNumber == objectNumber
                                                    && (e.Subsidiary ?? "") == subsidiary
                                                    select e).FirstOrDefaultAsync<ChartOfAccount>();

                return chartOfAcct;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<IList<ChartOfAccount>> GetEntitiesByAccount(string companyCode, string busUnit, string objectNumber, string subsidiary)
        {
            try
            {

                IQueryable<ChartOfAccount> query = _dbContext.ChartOfAccount;

                if (companyCode != "")
                {
                    query = query.Where(e => e.CompanyCode == companyCode);
                }
                if (busUnit != "")
                {
                    query = query.Where(e => e.BusUnit == busUnit);
                }
                if (objectNumber != "")
                {
                    query = query.Where(e => e.ObjectNumber == objectNumber);
                }
                if (subsidiary != "")
                {
                    query = query.Where(e => e.Subsidiary == subsidiary);
                }
                List<ChartOfAccount> list = new List<ChartOfAccount>();
                foreach (var item in query)
                {
                    list.Add(item);

                }
                await Task.Yield();
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
      
        public async Task<IList<ChartOfAccount>> GetEntitiesByIds(long[] accountIds)
        {
            try
            {
                IList<ChartOfAccount> list = await (from coa in _dbContext.ChartOfAccount
                                                 where accountIds.Contains(coa.AccountId)
                                           select coa
                                                                     ).ToListAsync<ChartOfAccount>();

                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
     
    }
}
