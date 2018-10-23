using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.ScheduleEventsDomain
{
    public class ScheduleEventModule : AbstractModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async Task<IQueryable<ScheduleEvent>> GetScheduleEventsByEmployeeId(long employeeId)
        {
            try
            {
                IQueryable<ScheduleEvent> query = await unitOfWork.scheduleEventRepository.GetScheduleEventsByEmployeeId(employeeId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

    }
}
