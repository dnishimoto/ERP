using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.TimeAndAttendanceDomain;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using System;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendanceScheduledToWork : AbstractModule, IFluentTimeAndAttendanceScheduledToWork
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        public FluentTimeAndAttendanceScheduledToWork() { }
        public IFluentTimeAndAttendanceScheduledToWork Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendanceScheduledToWork;
        }
        public IFluentTimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async () => await unitOfWork.timeAndAttendanceScheduledToWorkRepository.AddScheduledToWorkItems(items));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
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
                    int durationHours = scheduleView.DurationHours;
                    int durationMinutes = scheduleView.DurationMinutes;

                    int shiftStartTime = scheduleView.ShiftStartTime ?? 0;
                    int shiftEndTime = scheduleView.ShiftEndTime ?? 0;


                    DateTime startDate = unitOfWork.timeAndAttendanceRepository.BuildShortDate(currentDate, shiftStartTime);
                    DateTime endDate = unitOfWork.timeAndAttendanceRepository.AddTimeShortDate(startDate, durationHours, durationMinutes);

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
