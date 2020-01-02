   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{
    public class TimeAndAttendanceSetupView
    {
        public long TimeAndAttendanceSetupId { get; set; }
        public string TimeZone { get; set; }
        public bool? DaylightSavings { get; set; }
        public int Offset { get; set; }
        public long TimeAndAttendanceSetupNumber { get; set; }

    }
    public class TimeAndAttendanceSetupRepository: Repository<TimeAndAttendanceSetup>, ITimeAndAttendanceSetupRepository
    {
        ListensoftwaredbContext _dbContext;
        public TimeAndAttendanceSetupRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<TimeAndAttendanceSetup> GetEntitiesByExpression(Expression<Func<TimeAndAttendanceSetup, bool>> predicate)
         {
            var result =  _dbContext.Set<TimeAndAttendanceSetup>().Where(predicate);

            return result;
        }
 
  public async Task<TimeAndAttendanceSetup>GetEntityById(long ? timeAndAttendanceSetupId)
        {
			try{
            return await _dbContext.FindAsync<TimeAndAttendanceSetup>(timeAndAttendanceSetupId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<TimeAndAttendanceSetup> GetEntityByNumber(long timeAndAttendanceSetupNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.TimeAndAttendanceSetup
                               where detail.TimeAndAttendanceSetupNumber == timeAndAttendanceSetupNumber
                               select detail).FirstOrDefaultAsync<TimeAndAttendanceSetup>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<TimeAndAttendanceSetup> FindEntityByExpression(Expression<Func<TimeAndAttendanceSetup, bool>> predicate)
        {
            IQueryable<TimeAndAttendanceSetup> result = _dbContext.Set<TimeAndAttendanceSetup>().Where(predicate);

            return await result.FirstOrDefaultAsync<TimeAndAttendanceSetup>();
        }
		  public async Task<IList<TimeAndAttendanceSetup>> FindEntitiesByExpression(Expression<Func<TimeAndAttendanceSetup, bool>> predicate)
        {
            IQueryable<TimeAndAttendanceSetup> result = _dbContext.Set<TimeAndAttendanceSetup>().Where(predicate);

            return await result.ToListAsync<TimeAndAttendanceSetup>();
        }
		
  }
}
