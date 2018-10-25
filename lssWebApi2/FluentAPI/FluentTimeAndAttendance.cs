using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendance : AbstractModule, ITimeAndAttendance
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public FluentTimeAndAttendance()
        {

        }

        public IFluentTimeAndAttendanceQuery Query()
        {
            return new FluentTimeAndAttendanceQuery(unitOfWork) as IFluentTimeAndAttendanceQuery;
        }

        public ITimeAndAttendance Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as ITimeAndAttendance;
        }
        public string FormatPunchDateTime(DateTime? punchinDate)
        {
            return unitOfWork.timeAndAttendanceRepository.GetPunchDateTime(punchinDate);
        }
        public ITimeAndAttendance AddPunchIn(TimeAndAttendancePunchIn taPunchin)
        {

            try
            {
                Task<CreateProcessStatus> statusTask = Task.Run(async () => await unitOfWork.timeAndAttendanceRepository.AddPunchin(taPunchin));
                Task.WaitAll(statusTask);


                processStatus = statusTask.Result;
                return this as ITimeAndAttendance;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public ITimeAndAttendance UpdatePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                unitOfWork.timeAndAttendanceRepository.UpdateObject(taPunchin);
                processStatus = CreateProcessStatus.Update;
                return this as ITimeAndAttendance;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public ITimeAndAttendance DeletePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                CreateProcessStatus statusResult = unitOfWork.timeAndAttendanceRepository.DeletePunchin(taPunchin);
                processStatus = statusResult;
                return this as ITimeAndAttendance;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
