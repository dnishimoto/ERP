using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;
using System.Linq.Expressions;
using X.PagedList;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{
public class FluentTimeAndAttendanceSetupQuery:MapperAbstract<TimeAndAttendanceSetup,TimeAndAttendanceSetupView>,IFluentTimeAndAttendanceSetupQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentTimeAndAttendanceSetupQuery() { }
        public FluentTimeAndAttendanceSetupQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<TimeAndAttendanceSetup> MapToEntity(TimeAndAttendanceSetupView inputObject)
        {
            TimeAndAttendanceSetup outObject = mapper.Map<TimeAndAttendanceSetup>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<TimeAndAttendanceSetup>> MapToEntity(IList<TimeAndAttendanceSetupView> inputObjects)
        {
            IList<TimeAndAttendanceSetup> list = new List<TimeAndAttendanceSetup>();
            foreach (var item in inputObjects)
            {
                TimeAndAttendanceSetup outObject = mapper.Map<TimeAndAttendanceSetup>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

		public async Task<PageListViewContainer<TimeAndAttendanceSetupView>> GetViewsByPage(Expression<Func<TimeAndAttendanceSetup, bool>> predicate, Expression<Func<TimeAndAttendanceSetup, object>> order, int pageSize, int pageNumber)
        {

            var query = _unitOfWork.timeAndAttendanceSetupRepository.GetEntitiesByExpression(predicate);
            query = query.OrderByDescending(order).Select(e => e);

            IPagedList<TimeAndAttendanceSetup> list = await query.ToPagedListAsync(pageNumber, pageSize);

            PageListViewContainer<TimeAndAttendanceSetupView> container = new PageListViewContainer<TimeAndAttendanceSetupView>();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                TimeAndAttendanceSetupView view = await MapToView(item);
                container.Items.Add(view);
            }
            //await Task.Yield();
            return container;

        }

 public override async Task<TimeAndAttendanceSetupView> MapToView(TimeAndAttendanceSetup inputObject)
        {
            TimeAndAttendanceSetupView outObject = mapper.Map<TimeAndAttendanceSetupView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.timeAndAttendanceSetupRepository.GetNextNumber(TypeOfTimeAndAttendanceSetup.TimeAndAttendanceSetupNumber.ToString());
        }
 public override async Task<TimeAndAttendanceSetupView> GetViewById(long ? timeAndAttendanceSetupId)
        {
            TimeAndAttendanceSetup detailItem = await _unitOfWork.timeAndAttendanceSetupRepository.GetEntityById(timeAndAttendanceSetupId);

            return await MapToView(detailItem);
        }
 public async Task<TimeAndAttendanceSetupView> GetViewByNumber(long timeAndAttendanceSetupNumber)
        {
            TimeAndAttendanceSetup detailItem = await _unitOfWork.timeAndAttendanceSetupRepository.GetEntityByNumber(timeAndAttendanceSetupNumber);

            return await MapToView(detailItem);
        }

public override async Task<TimeAndAttendanceSetup> GetEntityById(long ? timeAndAttendanceSetupId)
        {
            return await _unitOfWork.timeAndAttendanceSetupRepository.GetEntityById(timeAndAttendanceSetupId);

        }
 public async Task<TimeAndAttendanceSetup> GetEntityByNumber(long timeAndAttendanceSetupNumber)
        {
            return await _unitOfWork.timeAndAttendanceSetupRepository.GetEntityByNumber(timeAndAttendanceSetupNumber);
        }
}
}
