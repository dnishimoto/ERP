﻿using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace ERP_Core2.Interfaces
{
    public interface IFluentTimeAndAttendanceQuery
    {
        Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId,string account);
        Task<TimeAndAttendancePunchIn> GetPunchInById(long timePunchinId);
        IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId);
        TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate);
        List<TimeAndAttendanceView> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate);
        List<TimeAndAttendanceView> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate);
        IPagedList<TimeAndAttendancePunchIn> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber);
        Task<bool> IsPunchOpen(long employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> GetPunchOpen(long employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long employeeId, int hours, int minutes, DateTime workDay, string account);
    }
}
