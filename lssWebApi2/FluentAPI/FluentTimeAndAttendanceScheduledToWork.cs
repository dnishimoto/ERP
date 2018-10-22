using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.TimeAndAttendanceDomain;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendanceScheduledToWork : AbstractModule, ITimeAndAttendanceScheduledToWork
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        public FluentTimeAndAttendanceScheduledToWork() { }
        public ITimeAndAttendanceScheduledToWork Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as ITimeAndAttendanceScheduledToWork;
        }
        public ITimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async () => await unitOfWork.timeAndAttendanceScheduledToWorkRepository.AddScheduledToWorkItems(items));
            //Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as ITimeAndAttendanceScheduledToWork;
        }
        public IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews)
        {
            IList<TimeAndAttendanceScheduledToWork> retList = new List<TimeAndAttendanceScheduledToWork>();

            foreach (var employeeItem in employeeViews)
            {
                TimeAndAttendanceScheduledToWork scheduledToWork = unitOfWork.timeAndAttendanceScheduledToWorkRepository.BuildScheduledToWork(scheduleView, employeeItem);
                retList.Add(scheduledToWork);
            }

            return retList;
        }
    }
}
