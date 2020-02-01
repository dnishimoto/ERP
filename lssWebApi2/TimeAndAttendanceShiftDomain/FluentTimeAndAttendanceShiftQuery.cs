using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;
using lssWebApi2.AbstractFactory;
using System.Linq.Expressions;
using X.PagedList;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{
    public class FluentTimeAndAttendanceShiftQuery : MapperAbstract<TimeAndAttendanceShift, TimeAndAttendanceShiftView>, IFluentTimeAndAttendanceShiftQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentTimeAndAttendanceShiftQuery() { }
        public FluentTimeAndAttendanceShiftQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<String> BuildLongDate(DateTime? date, String stringHours)
        {
            string retVal = _unitOfWork.timeAndAttendanceShiftRepository.BuildLongDate(date, stringHours);
            await Task.Yield();
            return retVal;
        }
        public override async Task<TimeAndAttendanceShift> MapToEntity(TimeAndAttendanceShiftView inputObject)
        {
            TimeAndAttendanceShift outObject = mapper.Map<TimeAndAttendanceShift>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<TimeAndAttendanceShift>> MapToEntity(IList<TimeAndAttendanceShiftView> inputObjects)
        {
            IList<TimeAndAttendanceShift> list = new List<TimeAndAttendanceShift>();
            foreach (var item in inputObjects)
            {
                TimeAndAttendanceShift outObject = mapper.Map<TimeAndAttendanceShift>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<PageListViewContainer<TimeAndAttendanceShiftView>> GetViewsByPage(Expression<Func<TimeAndAttendanceShift, bool>> predicate, Expression<Func<TimeAndAttendanceShift, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.timeAndAttendanceShiftRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<TimeAndAttendanceShift> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<TimeAndAttendanceShiftView> container = new PageListViewContainer<TimeAndAttendanceShiftView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                TimeAndAttendanceShiftView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

        public override async Task<TimeAndAttendanceShiftView> MapToView(TimeAndAttendanceShift inputObject)
        {
            TimeAndAttendanceShiftView outObject = mapper.Map<TimeAndAttendanceShiftView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        

        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfTimeAndAttendanceShift.TimeAndAttendanceShiftNumber.ToString());
        }
        public override async Task<TimeAndAttendanceShiftView> GetViewById(long? timeAndAttendanceShiftId)
        {
            TimeAndAttendanceShift detailItem = await _unitOfWork.timeAndAttendanceShiftRepository.GetEntityById(timeAndAttendanceShiftId);

            return await MapToView(detailItem);
        }
        public async Task<TimeAndAttendanceShiftView> GetViewByNumber(long timeAndAttendanceShiftNumber)
        {
            TimeAndAttendanceShift detailItem = await _unitOfWork.timeAndAttendanceShiftRepository.GetEntityByNumber(timeAndAttendanceShiftNumber);

            return await MapToView(detailItem);
        }

        public override async Task<TimeAndAttendanceShift> GetEntityById(long? timeAndAttendanceShiftId)
        {
            return await _unitOfWork.timeAndAttendanceShiftRepository.GetEntityById(timeAndAttendanceShiftId);

        }
        public async Task<TimeAndAttendanceShift> GetEntityByNumber(long timeAndAttendanceShiftNumber)
        {
            return await _unitOfWork.timeAndAttendanceShiftRepository.GetEntityByNumber(timeAndAttendanceShiftNumber);
        }
    }
}
