using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.TimeAndAttendanceDomain.Repository;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using MillenniumERP.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.TimeAndAttendanceDomain
{
 
  
    
    public class TimeAndAttendanceModule 
    {
        public FluentTimeAndAttendance TimeAndAttendance = new FluentTimeAndAttendance();
        public FluentTimeAndAttendanceSchedule TimeAndAttendanceSchedule = new FluentTimeAndAttendanceSchedule();
        public FluentTimeAndAttendanceScheduledToWork TimeAndAttendanceScheduleToWork =new FluentTimeAndAttendanceScheduledToWork();
    }
}
