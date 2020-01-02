
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace lssWebApi2.TaxRatesByCodeDomain
{
    public class TaxRatesByCodeView
    {
        public long TaxRatesByCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal? TaxRate { get; set; }
        public string State { get; set; }
        public long TaxRatesByCodeNumber { get; set; }

    }

    public class TaxRatesByCodeRepository : Repository<TaxRatesByCode>, ITaxRatesByCodeRepository
    {
        ListensoftwaredbContext _dbContext;
        public TaxRatesByCodeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<TaxRatesByCode> GetEntityById(long ? TaxRatesByCodeId)
        {
            return await _dbContext.FindAsync<TaxRatesByCode>(TaxRatesByCodeId);
        }
        public async Task<TaxRatesByCode> GetEntityByCode(string taxCode)
        {
            var query = await (from detail in _dbContext.TaxRatesByCode
                               where detail.TaxCode == taxCode
                               select detail).FirstOrDefaultAsync<TaxRatesByCode>();
            return query;
        }
        public async Task<TaxRatesByCode> GetEntityByNumber(long TaxRatesByCodeNumber)
        {
            var query = await (from detail in _dbContext.TaxRatesByCode
                               where detail.TaxRatesByCodeNumber == TaxRatesByCodeNumber
                               select detail).FirstOrDefaultAsync<TaxRatesByCode>();
            return query;
        }
    }
}

