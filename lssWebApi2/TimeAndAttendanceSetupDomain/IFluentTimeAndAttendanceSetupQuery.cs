using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.TimeAndAttendanceSetupDomain;


public interface IFluentTimeAndAttendanceSetupQuery
{
    Task<TimeAndAttendanceSetup> MapToEntity(TimeAndAttendanceSetupView inputObject);
    Task<List<TimeAndAttendanceSetup>> MapToEntity(List<TimeAndAttendanceSetupView> inputObjects);

    Task<TimeAndAttendanceSetupView> MapToView(TimeAndAttendanceSetup inputObject);
    Task<NextNumber> GetNextNumber();
    Task<TimeAndAttendanceSetup> GetEntityById(long? timeAndAttendanceSetupId);
    Task<TimeAndAttendanceSetup> GetEntityByNumber(long timeAndAttendanceSetupNumber);
    Task<TimeAndAttendanceSetupView> GetViewById(long? timeAndAttendanceSetupId);
    Task<TimeAndAttendanceSetupView> GetViewByNumber(long timeAndAttendanceSetupNumber);
}
