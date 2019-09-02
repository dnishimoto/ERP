using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class TimeAndAttendanceSetup
    {
        public long TimeAndAttendanceSetupId { get; set; }
        public string TimeZone { get; set; }
        public bool? DaylightSavings { get; set; }

    }
}