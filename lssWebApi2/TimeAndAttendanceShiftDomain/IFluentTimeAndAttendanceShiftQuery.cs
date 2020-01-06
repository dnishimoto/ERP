using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using lssWebApi2.AbstractFactory;
using System.Linq.Expressions;
using System;

public interface IFluentTimeAndAttendanceShiftQuery
{
    Task<String> BuildLongDate(DateTime? date, String stringHours);
    Task<TimeAndAttendanceShift> MapToEntity(TimeAndAttendanceShiftView inputObject);
    Task<IList<TimeAndAttendanceShift>> MapToEntity(IList<TimeAndAttendanceShiftView> inputObjects);
    Task<TimeAndAttendanceShiftView> MapToView(TimeAndAttendanceShift inputObject);
    Task<NextNumber> GetNextNumber();
    Task<TimeAndAttendanceShift> GetEntityById(long? timeAndAttendanceShiftId);
    Task<TimeAndAttendanceShift> GetEntityByNumber(long timeAndAttendanceShiftNumber);
    Task<TimeAndAttendanceShiftView> GetViewById(long? timeAndAttendanceShiftId);
    Task<TimeAndAttendanceShiftView> GetViewByNumber(long timeAndAttendanceShiftNumber);
    Task<PageListViewContainer<TimeAndAttendanceShiftView>> GetViewsByPage(Expression<Func<TimeAndAttendanceShift, bool>> predicate, Expression<Func<TimeAndAttendanceShift, object>> order, int pageSize, int pageNumber);
}
