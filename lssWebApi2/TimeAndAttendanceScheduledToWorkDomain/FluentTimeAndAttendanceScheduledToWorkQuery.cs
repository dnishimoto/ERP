using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using lssWebApi2.Services;
using lssWebApi2.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public class FluentTimeAndAttendanceScheduledToWorkQuery:MapperAbstract<TimeAndAttendanceScheduledToWork, TimeAndAttendanceScheduledToWorkView>, IFluentTimeAndAttendanceScheduledToWorkQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentTimeAndAttendanceScheduledToWorkQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<TimeAndAttendanceScheduledToWork> MapToEntity(TimeAndAttendanceScheduledToWorkView inputObject)
        {
            mapper.dictSpecialMapping.Clear();

            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c)=>c.StartDateTime), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ShiftStartTime));
            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.EndDateTime), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ShiftEndTime));
            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.StartDate), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ScheduleStartDate));
            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.EndDate), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ScheduleEndDate));

        TimeAndAttendanceScheduledToWork outObject = mapper.Map<TimeAndAttendanceScheduledToWork>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<TimeAndAttendanceScheduledToWork>> MapToEntity(List<TimeAndAttendanceScheduledToWorkView> inputObjects)
        {
            mapper.dictSpecialMapping.Clear();

            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.StartDateTime), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ShiftStartTime));
            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.EndDateTime), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ShiftEndTime));
            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.StartDate), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ScheduleStartDate));
            mapper.dictSpecialMapping.Add(GetMemberName((TimeAndAttendanceScheduledToWork c) => c.EndDate), GetMemberName((TimeAndAttendanceScheduledToWorkView c) => c.ScheduleEndDate));


            List<TimeAndAttendanceScheduledToWork> list = new List<TimeAndAttendanceScheduledToWork>();

            foreach (var item in inputObjects)
            {
                TimeAndAttendanceScheduledToWork outObject = mapper.Map<TimeAndAttendanceScheduledToWork>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public override async Task<TimeAndAttendanceScheduledToWorkView> MapToView(TimeAndAttendanceScheduledToWork inputObject)
        {

          
            TimeAndAttendanceScheduledToWorkView outObject = mapper.Map<TimeAndAttendanceScheduledToWorkView>(inputObject);
            Task<Employee> employeeTask =  _unitOfWork.employeeRepository.GetEntityById(inputObject.EmployeeId);
            Task<TimeAndAttendanceSchedule> scheduleTask =  _unitOfWork.timeAndAttendanceScheduleRepository.GetEntityById(inputObject.ScheduleId);
            Task<Udc> udcJobCodeTask = _unitOfWork.udcRepository.GetEntityById(inputObject?.JobCodeXrefId);
            Task<Udc> udcPayCodeTask = _unitOfWork.udcRepository.GetEntityById(inputObject?.PayCodeXrefId);
            Task<Udc> udcWorkedJobCodeTask= _unitOfWork.udcRepository.GetEntityById(inputObject?.WorkedJobCodeXrefId);
            Task.WaitAll(employeeTask, scheduleTask,udcJobCodeTask,udcPayCodeTask,udcWorkedJobCodeTask);

            Task<AddressBook> addressBookEmployeeTask = _unitOfWork.addressBookRepository.GetEntityById(employeeTask.Result?.AddressId);
            Task<TimeAndAttendanceShift> shiftTask =  _unitOfWork.timeAndAttendanceShiftRepository.GetEntityById(scheduleTask.Result?.ShiftId);

            string shiftStartTime = _unitOfWork.timeAndAttendanceShiftRepository.BuildLongDate(scheduleTask.Result?.StartDate, shiftTask.Result?.ShiftStartTime);
            string shiftEndTime =  _unitOfWork.timeAndAttendanceShiftRepository.BuildLongDate(scheduleTask.Result?.EndDate, shiftTask.Result?.ShiftEndTime);

            outObject.EmployeeName = addressBookEmployeeTask.Result?.Name;
            outObject.ScheduleName = scheduleTask.Result?.ScheduleName;
            outObject.ShiftStartTime = shiftStartTime;
            outObject.ShiftEndTime = shiftEndTime;

            outObject.JobCode = udcJobCodeTask.Result?.KeyCode;
            outObject.PayCode = udcPayCodeTask.Result?.KeyCode;
            outObject.WorkedJobCode = udcWorkedJobCodeTask.Result?.KeyCode;

            outObject.DurationHours = shiftTask.Result?.DurationHours ?? 0;
            outObject.DurationMinutes = shiftTask.Result?.DurationMinutes ?? 0;
            outObject.Monday = shiftTask.Result?.Monday;
            outObject.Tuesday = shiftTask.Result?.Tuesday;
            outObject.Wednesday = shiftTask.Result?.Wednesday;
            outObject.Thursday = shiftTask.Result?.Thursday;
            outObject.Friday = shiftTask.Result?.Friday;
            outObject.Saturday = shiftTask.Result?.Saturday;
            outObject.Sunday = shiftTask.Result?.Sunday;

            await Task.Yield();
            return outObject;

        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.timeAndAttendanceScheduledToWorkRepository.GetNextNumber(TypeOfTimeAndAttendanceScheduledToWork.ScheduledToWorkNumber.ToString());
        }
        public async Task<TimeAndAttendanceScheduledToWorkView> GetViewByNumber(long scheduledToWorkNumber)
        {
            TimeAndAttendanceScheduledToWork detailItem = await _unitOfWork.timeAndAttendanceScheduledToWorkRepository.GetEntityByNumber(scheduledToWorkNumber);

            return await MapToView(detailItem);
        }
        public async Task<TimeAndAttendanceScheduledToWork> GetEntityByNumber(long scheduledToWorkNumber)
        {
            return await _unitOfWork.timeAndAttendanceScheduledToWorkRepository.GetEntityByNumber(scheduledToWorkNumber);

        }

        public override async Task<TimeAndAttendanceScheduledToWorkView> GetViewById(long? scheduledToWorkId)
        {
            TimeAndAttendanceScheduledToWorkView view = await MapToView(await _unitOfWork.timeAndAttendanceScheduledToWorkRepository.GetEntityById(scheduledToWorkId));

            return view;
        }
        public override async Task<TimeAndAttendanceScheduledToWork> GetEntityById(long? scheduledToWorkId)
        {

            TimeAndAttendanceScheduledToWork detail = await _unitOfWork.timeAndAttendanceScheduledToWorkRepository.GetEntityById(scheduledToWorkId);

            return detail;
        }
    }
}
