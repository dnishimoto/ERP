

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ScheduleEventDomain
{
    public class ScheduleEventView
    {

        public long ScheduleEventId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? EventDateTime { get; set; }
        public long ServiceId { get; set; }
        public long? DurationMinutes { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long ScheduleEventNumber { get; set; }


        //Service Information
        public string ServiceDescription { get; set; }

        /*
        public decimal? Price { get; set; }
        public string AddOns { get; set; }
        public long? ServiceTypeXRefId { get; set; }
        public string ServiceType { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? SquareFeetOfStructure { get; set; }
        public string LocationDescription { get; set; }
        public string LocationGPS { get; set; }
        public string Comments { get; set; }
        public LocationAddressView LocationAddressView { get; set; }
        public ContractView ContractView { get; set; }
        */

    }

    public class ScheduleEventRepository : Repository<ScheduleEvent>, IScheduleEventRepository
    {
        ListensoftwaredbContext _dbContext;
        public ScheduleEventRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IList<ScheduleEvent>> GetEntitiesByServiceId(long? serviceId)
        {

            try
            {

                List<ScheduleEvent> scheduleEventList = await (from detail in   _dbContext.ScheduleEvent
                                                                 where detail.ServiceId == serviceId
                                                               select detail).ToListAsync<ScheduleEvent>();


                return scheduleEventList;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<IList<ScheduleEvent>> GetEntitiesByCustomerId(long? customerId)
        {
            try
            {
      
                List<ScheduleEvent> scheduleEventList = await (from detail in _dbContext.Customer
                                                               join scheduleEvent in
                                                              _dbContext.ScheduleEvent
                                                           on detail.CustomerId equals customerId

                                                               where detail.CustomerId == customerId
                                                               select scheduleEvent).ToListAsync<ScheduleEvent>();


                return scheduleEventList;
            
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IQueryable<ScheduleEvent>> GetEntitiesByEmployeeId(long ? employeeId)
        {
            try
            {
                var list = (from detail in _dbContext.ScheduleEvent
                                  where detail.EmployeeId == employeeId
                                  select detail).AsQueryable<ScheduleEvent>();

                await Task.Yield();

                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }

        public async Task<ScheduleEvent> GetEntityById(long ? scheduleEventId)
        {
            try
            {
                return await _dbContext.FindAsync<ScheduleEvent>(scheduleEventId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ScheduleEvent> GetEntityByNumber(long scheduleEventNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.ScheduleEvent
                                   where detail.ScheduleEventNumber == scheduleEventNumber
                                   select detail).FirstOrDefaultAsync<ScheduleEvent>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
       

    }
}
