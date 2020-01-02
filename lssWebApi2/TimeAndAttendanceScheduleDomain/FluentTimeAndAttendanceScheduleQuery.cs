using lssWebApi2.AbstractFactory;
using lssWebApi2.Interfaces;
using lssWebApi2.Services;
using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Linq;
using System.Linq.Expressions;
using lssWebApi2.MapperAbstract;
using System.Threading.Tasks;
using X.PagedList;
using System.Collections.Generic;
using lssWebApi2.Enumerations;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public class FluentTimeAndAttendanceScheduleQuery : MapperAbstract<TimeAndAttendanceSchedule, TimeAndAttendanceScheduleView>, IFluentTimeAndAttendanceScheduleQuery
    {
        UnitOfWork _unitOfWork = null;
        public FluentTimeAndAttendanceScheduleQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<PageListViewContainer<TimeAndAttendanceScheduleView>> GetViewsByPage(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate, Expression<Func<TimeAndAttendanceSchedule, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.timeAndAttendanceScheduleRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<TimeAndAttendanceSchedule> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<TimeAndAttendanceScheduleView> container = new PageListViewContainer<TimeAndAttendanceScheduleView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                TimeAndAttendanceScheduleView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.timeAndAttendanceScheduledToWorkRepository.GetNextNumber(TypeOfTimeAndAttendanceSchedule.TimeAndAttendanceScheduleNumber.ToString());
        }

        public override async Task<TimeAndAttendanceSchedule> MapToEntity(TimeAndAttendanceScheduleView inputObject)
        {

            TimeAndAttendanceSchedule outObject = mapper.Map<TimeAndAttendanceSchedule>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<TimeAndAttendanceSchedule>> MapToEntity(List<TimeAndAttendanceScheduleView> inputObjects)
        {
            List<TimeAndAttendanceSchedule> list = new List<TimeAndAttendanceSchedule>();

            foreach (var item in inputObjects)
            {
                TimeAndAttendanceSchedule outObject = mapper.Map<TimeAndAttendanceSchedule>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<TimeAndAttendanceScheduleView> MapToView(TimeAndAttendanceSchedule inputObject)
        {

            TimeAndAttendanceScheduleView outObject = mapper.Map<TimeAndAttendanceScheduleView>(inputObject);

            TimeAndAttendanceShift shift = await _unitOfWork.timeAndAttendanceShiftRepository.GetEntityById(inputObject.ShiftId);

            outObject.ShiftName = shift.ShiftName;
            outObject.ShiftStartTime = shift.ShiftStartTime;
            outObject.ShiftEndTime = shift.ShiftEndTime;

            await Task.Yield();

            return outObject;
        }
        public async Task<TimeAndAttendanceSchedule> GetEntityByNumber(long scheduleNumber)
        {
            return await _unitOfWork.timeAndAttendanceScheduleRepository.GetEntityByNumber(scheduleNumber);
        }
        public async Task<TimeAndAttendanceScheduleView> GetViewByNumber(long scheduleNumber)
        {
            TimeAndAttendanceSchedule detailItem = await _unitOfWork.timeAndAttendanceScheduleRepository.GetEntityByNumber(scheduleNumber);

            return await MapToView(detailItem);
        }
        public override async Task<TimeAndAttendanceScheduleView> GetViewById(long? timePunchinId)
        {
            TimeAndAttendanceScheduleView view = await MapToView(await _unitOfWork.timeAndAttendanceScheduleRepository.GetEntityById(timePunchinId));

            return view;
        }
        public override async Task<TimeAndAttendanceSchedule> GetEntityById(long? timePunchinId)
        {

            TimeAndAttendanceSchedule detail = await _unitOfWork.timeAndAttendanceScheduleRepository.GetEntityById(timePunchinId);

            return detail;
        }

        public async Task<TimeAndAttendanceScheduleView> GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate)
        {
            try
            {
                var query = _unitOfWork.timeAndAttendanceScheduleRepository.GetEntitiesByExpression(predicate) as IQueryable<TimeAndAttendanceSchedule>;
                TimeAndAttendanceScheduleView retItem = null;
                foreach (var item in query)
                {
                    retItem = await MapToView(item);
                    break;
                }
                return retItem;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

    }
}
