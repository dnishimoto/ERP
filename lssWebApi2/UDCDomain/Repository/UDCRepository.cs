using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.UDCDomain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ScheduleEventsDomain
{

    public class UDCRepository : Repository<Udc>, IUDCRepository
    {
        ListensoftwaredbContext _dbContext;
        public UDCRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IQueryable<Udc>> GetUDCValuesByProductCode(string productCode)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.ProductCode == productCode, "").ToListAsync();

                return list.AsQueryable<Udc>();
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
           
        }
    }
}
