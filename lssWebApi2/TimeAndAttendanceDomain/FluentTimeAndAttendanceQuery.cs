using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;
using lssWebApi2.MapperAbstract;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public class FluentTimeAndAttendanceQuery : MapperAbstract<TimeAndAttendancePunchIn, TimeAndAttendancePunchInView>, IFluentTimeAndAttendanceQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentTimeAndAttendanceQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PageListViewContainer<TimeAndAttendancePunchInView>>  GetViewsByPage(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate, Expression<Func<TimeAndAttendancePunchIn, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.timeAndAttendanceRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<TimeAndAttendancePunchIn> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer < TimeAndAttendancePunchInView > container = new PageListViewContainer<TimeAndAttendancePunchInView> ();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                TimeAndAttendancePunchInView view =await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }
        public async Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long ? employeeId, int hours, int minutes, int mealDurationInMinutes, DateTime workDay, string account)
        {
            return await _unitOfWork.timeAndAttendanceRepository.BuildByTimeDuration(employeeId, hours, minutes, mealDurationInMinutes, workDay, account);
        }
        public async Task<TimeAndAttendanceTimeView> GetUTCAdjustedTime()
        {
            return await _unitOfWork.timeAndAttendanceRepository.GetUTCAdjustedTime();
        }
        
        public async Task<TimeAndAttendancePunchInView> GetPunchOpenView(long ? employeeId)
        {
            return await MapToView(await _unitOfWork.timeAndAttendanceRepository.GetPunchOpen(employeeId));
        }
        public async Task<TimeAndAttendancePunchIn> GetPunchOpen(long ? employeeId)
        {
            return await _unitOfWork.timeAndAttendanceRepository.GetPunchOpen(employeeId);
        }
        
        public async Task<TimeAndAttendancePunchIn> IsPunchOpen(long ? employeeId, DateTime asOfDate)
        {
            return await _unitOfWork.timeAndAttendanceRepository.IsPunchOpen(employeeId,asOfDate);
        }
        public async Task<TimeAndAttendancePunchIn> BuildPunchin(long ? employeeId,string account,DateTime punchDate)
        {

            return await _unitOfWork.timeAndAttendanceRepository.BuildPunchin(employeeId,account,punchDate);
        }
        public override async Task<TimeAndAttendancePunchIn> MapToEntity(TimeAndAttendancePunchInView inputObject)
        {

            TimeAndAttendancePunchIn outObject = mapper.Map<TimeAndAttendancePunchIn>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<TimeAndAttendancePunchIn>> MapToEntity(List<TimeAndAttendancePunchInView> inputObjects)
        {
            List<TimeAndAttendancePunchIn> list = new List<TimeAndAttendancePunchIn>();

            foreach (var item in inputObjects)
            {
                TimeAndAttendancePunchIn outObject = mapper.Map<TimeAndAttendancePunchIn>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<TimeAndAttendancePunchInView> MapToView(TimeAndAttendancePunchIn inputObject)
        {

            TimeAndAttendancePunchInView outObject = mapper.Map<TimeAndAttendancePunchInView>(inputObject);

            Task<Employee> employeeTask =  _unitOfWork.employeeRepository.GetEntityById(inputObject.EmployeeId);
            Task<Udc> udcTypeOfPayTask =  _unitOfWork.udcRepository.GetEntityById(inputObject.TypeOfTimeUdcXrefId);
            Task<Supervisor> supervisorTask =  _unitOfWork.supervisorRepository.GetEntityById(inputObject.SupervisorId);
            Task.WaitAll(employeeTask, udcTypeOfPayTask, supervisorTask);

            Task<AddressBook> addressBookEmployeeTask =  _unitOfWork.addressBookRepository.GetEntityById(employeeTask.Result.AddressId);
              Task<AddressBook> addressBookSupervisorTask = _unitOfWork.addressBookRepository.GetEntityById(supervisorTask.Result.AddressId);
            Task.WaitAll(addressBookEmployeeTask, addressBookSupervisorTask);

            outObject.EmployeeName = addressBookEmployeeTask.Result.Name;
            outObject.SupervisorName = addressBookSupervisorTask.Result.Name;
            outObject.TypeOfTime = udcTypeOfPayTask.Result.Value;

            await Task.Yield();

            return outObject;
        }

      
        public TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate)
        {
            var query = _unitOfWork.timeAndAttendanceRepository.GetEntitiesByExpression(predicate) as IQueryable<TimeAndAttendancePunchIn>;
            TimeAndAttendancePunchIn retItem = null;
            foreach (var item in query)
            {
                retItem = item;
                break;
            }
            return retItem;

        }
        public override async Task<TimeAndAttendancePunchInView> GetViewById(long ? timePunchinId)
        {
            TimeAndAttendancePunchInView view = await MapToView(await _unitOfWork.timeAndAttendanceRepository.GetEntityById(timePunchinId));
            
            return view;
        }
        public override async Task<TimeAndAttendancePunchIn> GetEntityById(long ? timePunchinId)
        {

            TimeAndAttendancePunchIn taPunchinTask = await _unitOfWork.timeAndAttendanceRepository.GetEntityById(timePunchinId);

            return taPunchinTask;
        }

        public async Task<IList<TimeAndAttendanceView>> GetViewsByDate(DateTime startDate, DateTime endDate)
        {
            IList<TimeAndAttendanceView> taPunchin =  await _unitOfWork.timeAndAttendanceRepository.GetViewsByDate(startDate, endDate);
            
            return taPunchin;
        }
        public async Task<IList<TimeAndAttendanceView>> GetViewsByIdAndDate(long ? employeeId, DateTime startDate, DateTime endDate)
        {
            IList<TimeAndAttendanceView> taPunchin =  await _unitOfWork.timeAndAttendanceRepository.GetViewsByIdAndDate(employeeId, startDate, endDate);
           
         
            return taPunchin;
        }
        public async Task<IList<TimeAndAttendancePunchInView>> GetEntitiesByEmployeeId(long ? employeeId)
        {
            IList<TimeAndAttendancePunchIn> list = await _unitOfWork.timeAndAttendanceRepository.GetEntitiesByEmployeeId(employeeId);

            IList<TimeAndAttendancePunchInView> views = new List<TimeAndAttendancePunchInView>();

            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }

            return views;
        }
    }
}
