using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EntityFramework
{
    public class TimeAndAttendanceSetup
    {
        public long TimeAndAttendanceSetupId { get; set; }
        public string TimeZone { get; set; }
	    public bool DaylightSavings { get; set; }
    }
}
