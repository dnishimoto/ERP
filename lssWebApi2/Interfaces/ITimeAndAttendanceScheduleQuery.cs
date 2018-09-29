﻿using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.entityframework;
using System;
using System.Linq.Expressions;

namespace ERP_Core2.Interfaces
{
    public interface ITimeAndAttendanceScheduleQuery
    {
        TimeAndAttendanceScheduleView GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate);
    }
}
