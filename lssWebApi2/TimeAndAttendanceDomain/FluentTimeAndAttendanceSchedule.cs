using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.TimeAndAttendanceDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendanceSchedule : AbstractModule, IFluentTimeAndAttendanceSchedule
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        FluentTimeAndAttendanceScheduleQuery _query;
        public FluentTimeAndAttendanceSchedule()
        {

        }
        public IFluentTimeAndAttendanceScheduleQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentTimeAndAttendanceScheduleQuery(unitOfWork);
            }
            return _query as IFluentTimeAndAttendanceScheduleQuery;
        }
        public IFluentTimeAndAttendanceSchedule Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendanceSchedule;
        }
        public IFluentTimeAndAttendanceSchedule AddSchedule(TimeAndAttendanceScheduleView view)
        {
            Task<CreateProcessStatus> statusTask = Task.Run(async () => await unitOfWork.timeAndAttendanceScheduleRepository.AddSchedule(view));
            Task.WaitAll(statusTask);
            processStatus = statusTask.Result;
            return this as IFluentTimeAndAttendanceSchedule;
        }
    }
}
