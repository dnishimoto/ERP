using lssWebApi2.AbstractFactory;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using System;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.TimeAndAttendanceScheduleDomain;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public class FluentTimeAndAttendanceScheduledToWork : AbstractModule, IFluentTimeAndAttendanceScheduledToWork
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        FluentTimeAndAttendanceScheduledToWorkQuery _query;
        public FluentTimeAndAttendanceScheduledToWork() { }

        public IFluentTimeAndAttendanceScheduledToWorkQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentTimeAndAttendanceScheduledToWorkQuery(unitOfWork);
            }
            return _query as IFluentTimeAndAttendanceScheduledToWorkQuery;
        }

        public IFluentTimeAndAttendanceScheduledToWork Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IFluentTimeAndAttendanceScheduledToWork AddTimeAndAttendanceScheduledToWorks(List<TimeAndAttendanceScheduledToWork> newObjects)
        {
            unitOfWork.timeAndAttendanceScheduledToWorkRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IFluentTimeAndAttendanceScheduledToWork UpdateTimeAndAttendanceScheduledToWorks(IList<TimeAndAttendanceScheduledToWork> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.timeAndAttendanceScheduledToWorkRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IFluentTimeAndAttendanceScheduledToWork AddTimeAndAttendanceScheduledToWork(TimeAndAttendanceScheduledToWork newObject)
        {
            unitOfWork.timeAndAttendanceScheduledToWorkRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IFluentTimeAndAttendanceScheduledToWork UpdateTimeAndAttendanceScheduledToWork(TimeAndAttendanceScheduledToWork updateObject)
        {
            unitOfWork.timeAndAttendanceScheduledToWorkRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceScheduledToWork;

        }
        public IFluentTimeAndAttendanceScheduledToWork DeleteTimeAndAttendanceScheduledToWork(TimeAndAttendanceScheduledToWork deleteObject)
        {
            unitOfWork.timeAndAttendanceScheduledToWorkRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IFluentTimeAndAttendanceScheduledToWork DeleteTimeAndAttendanceScheduledToWorks(List<TimeAndAttendanceScheduledToWork> deleteObjects)
        {
            unitOfWork.timeAndAttendanceScheduledToWorkRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews, string payCode)
        {
    
            IList<TimeAndAttendanceScheduledToWork> retList = new List<TimeAndAttendanceScheduledToWork>();

            DateTime startDay = scheduleView.StartDate ?? DateTime.Now;
            DateTime endDay = scheduleView.EndDate ?? DateTime.Now;

            TimeSpan difference = endDay - startDay;

            foreach (var employeeItem in employeeViews)
            {
                for (int i = 0; i <= difference.Days; i++)
                {
                    bool monday = scheduleView.Monday??false;
                    bool tuesday = scheduleView.Tuesday ?? false;
                    bool wednesday = scheduleView.Wednesday ?? false;
                    bool thursday = scheduleView.Thursday ?? false;
                    bool friday = scheduleView.Friday ?? false;
                    bool saturday = scheduleView.Saturday ?? false;
                    bool sunday = scheduleView.Sunday ?? false;


                    DateTime scheduleStartDate = scheduleView.StartDate ?? DateTime.Now;
                    DateTime currentDate = scheduleStartDate.AddDays(i);
                    int ?durationHours = scheduleView.DurationHours??0;
                    int ?durationMinutes = scheduleView.DurationMinutes??0;

                    string shiftStartTime = scheduleView.ShiftStartTime ;
                    string shiftEndTime = scheduleView.ShiftEndTime;


                    DateTime startDate = unitOfWork.timeAndAttendanceRepository.BuildShortDate(currentDate, shiftStartTime);
                    DateTime endDate = unitOfWork.timeAndAttendanceRepository.AddTimeShortDate(startDate, durationHours??0, durationMinutes??0);

                    string startDateTime = unitOfWork.timeAndAttendanceRepository.BuildLongDate(currentDate, shiftStartTime);
                    string endDateTime = unitOfWork.timeAndAttendanceRepository.BuildLongDate(endDate);

                    TimeAndAttendanceScheduleDayView currentDayView = new TimeAndAttendanceScheduleDayView();
                    currentDayView.StartDate = currentDate;
                    currentDayView.StartDateTime = startDateTime;
                    currentDayView.EndDate = endDate;
                    currentDayView.EndDateTime = endDateTime;


                    if (
                        (currentDate.DayOfWeek == DayOfWeek.Monday && monday)
                        || (currentDate.DayOfWeek == DayOfWeek.Tuesday && tuesday)
                        || (currentDate.DayOfWeek == DayOfWeek.Wednesday && wednesday)
                        || (currentDate.DayOfWeek == DayOfWeek.Thursday && thursday)
                        || (currentDate.DayOfWeek == DayOfWeek.Friday && friday)
                        || (currentDate.DayOfWeek == DayOfWeek.Saturday && saturday)
                        || (currentDate.DayOfWeek == DayOfWeek.Sunday && sunday)
                        )
                    {

                        TimeAndAttendanceScheduledToWork scheduledToWork = unitOfWork.timeAndAttendanceScheduledToWorkRepository.BuildScheduledToWork(scheduleView, currentDayView, employeeItem,payCode);
                        retList.Add(scheduledToWork);
                    }
                }
            }

            return retList;
        }
    }
}
