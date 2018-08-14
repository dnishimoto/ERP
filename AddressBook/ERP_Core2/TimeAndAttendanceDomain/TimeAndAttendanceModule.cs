using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.TimeAndAttendanceDomain
{
    class TimeAndAttendanceModule : AbstractModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async Task<long> AddPunchIn(TimeAndAttendancePunchIn taPunchin)
        {

            try
            {
                long timePunchinId = await unitOfWork.TARepository.AddPunchin(taPunchin);
                unitOfWork.CommitChanges();

                return timePunchinId;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<TimeAndAttendancePunchIn> GetPunchInById(long timePunchinId)
        {
            try
            {

                TimeAndAttendancePunchIn taPunchin = await unitOfWork.TARepository.GetObjectAsync(timePunchinId);
                return taPunchin;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId)
        {
            Task<IList<TimeAndAttendancePunchInView>> resultTask = Task.Run<IList<TimeAndAttendancePunchInView>>(async () => await unitOfWork.TARepository.GetTAPunchinByEmployeeId(employeeId));
            return resultTask.Result;
        }
    public bool UpdatePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                unitOfWork.TARepository.UpdateObject(taPunchin);
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public bool DeletePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                bool result = unitOfWork.TARepository.DeletePunchin(taPunchin);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
