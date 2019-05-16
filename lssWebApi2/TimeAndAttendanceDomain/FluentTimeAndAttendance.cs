using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendance : AbstractModule, IFluentTimeAndAttendance
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

        public IFluentTimeAndAttendance Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendance;
        }
        public string FormatPunchDateTime(DateTime? punchinDate)
        {
            return unitOfWork.timeAndAttendanceRepository.GetPunchDateTime(punchinDate);
        }
        
        public IFluentTimeAndAttendance AddPunchIn(TimeAndAttendancePunchIn taPunchin)
        {

            try
            {
                Task<CreateProcessStatus> statusTask = Task.Run(async () => await unitOfWork.timeAndAttendanceRepository.AddPunchin(taPunchin));
                Task.WaitAll(statusTask);


                processStatus = statusTask.Result;
                return this as IFluentTimeAndAttendance;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public IFluentTimeAndAttendance UpdatePunchIn(TimeAndAttendancePunchIn taPunchin,int mealDeduction,int manualElapsedHours=0, int manualElapsedMinutes=0)
        {
            try
            {
                Task<CreateProcessStatus> statusTask = Task.Run(async()=>await unitOfWork.timeAndAttendanceRepository.UpdatePunchin(taPunchin,mealDeduction,manualElapsedHours,manualElapsedMinutes));
                Task.WaitAll(statusTask);
                processStatus = statusTask.Result;
                return this as IFluentTimeAndAttendance;

            }
            catch (Exception ex)
            {
                processStatus = CreateProcessStatus.Failed;
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IFluentTimeAndAttendance DeletePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                CreateProcessStatus statusResult = unitOfWork.timeAndAttendanceRepository.DeletePunchin(taPunchin);
                processStatus = statusResult;
                return this as IFluentTimeAndAttendance;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
