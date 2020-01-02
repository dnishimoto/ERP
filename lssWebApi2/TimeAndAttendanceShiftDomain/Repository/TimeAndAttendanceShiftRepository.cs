   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{
    public class TimeAndAttendanceShiftView
    {
       
        public long ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public string ShiftType { get; set; }
        public int? DurationHours { get; set; }
        public int? DurationMinutes { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }
        public long TimeAndAttendanceShiftNumber { get; set; }
    }
    public class TimeAndAttendanceShiftRepository: Repository<TimeAndAttendanceShift>, ITimeAndAttendanceShiftRepository
    {
        ListensoftwaredbContext _dbContext;
        public TimeAndAttendanceShiftRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<TimeAndAttendanceShift> GetEntitiesByExpression(Expression<Func<TimeAndAttendanceShift, bool>> predicate)
         {
            var result =  _dbContext.Set<TimeAndAttendanceShift>().Where(predicate);

            return result;
        }
 
  public async Task<TimeAndAttendanceShift>GetEntityById(long ? timeAndAttendanceShiftId)
        {
			try{
            return await _dbContext.FindAsync<TimeAndAttendanceShift>(timeAndAttendanceShiftId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<TimeAndAttendanceShift> GetEntityByNumber(long timeAndAttendanceShiftNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.TimeAndAttendanceShift
                               where detail.TimeAndAttendanceShiftNumber == timeAndAttendanceShiftNumber
                               select detail).FirstOrDefaultAsync<TimeAndAttendanceShift>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<TimeAndAttendanceShift> FindEntityByExpression(Expression<Func<TimeAndAttendanceShift, bool>> predicate)
        {
            IQueryable<TimeAndAttendanceShift> result = _dbContext.Set<TimeAndAttendanceShift>().Where(predicate);

            return await result.FirstOrDefaultAsync<TimeAndAttendanceShift>();
        }
		  public async Task<IList<TimeAndAttendanceShift>> FindEntitiesByExpression(Expression<Func<TimeAndAttendanceShift, bool>> predicate)
        {
            IQueryable<TimeAndAttendanceShift> result = _dbContext.Set<TimeAndAttendanceShift>().Where(predicate);

            return await result.ToListAsync<TimeAndAttendanceShift>();
        }
		
  }
}
