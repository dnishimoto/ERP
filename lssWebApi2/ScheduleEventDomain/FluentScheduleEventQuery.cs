using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ScheduleEventDomain
{
    public class FluentScheduleEventQuery : MapperAbstract<ScheduleEvent, ScheduleEventView>, IFluentScheduleEventQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentScheduleEventQuery() { }
        public FluentScheduleEventQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<ScheduleEvent> MapToEntity(ScheduleEventView inputObject)
        {

            ScheduleEvent outObject = mapper.Map<ScheduleEvent>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<ScheduleEvent>> MapToEntity(IList<ScheduleEventView> inputObjects)
        {
            IList<ScheduleEvent> list = new List<ScheduleEvent>();

            foreach (var item in inputObjects)
            {
                ScheduleEvent outObject = mapper.Map<ScheduleEvent>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<ScheduleEventView> MapToView(ScheduleEvent inputObject)
        {

            ScheduleEventView outObject = mapper.Map<ScheduleEventView>(inputObject);

            Task<Employee> employeeTask = _unitOfWork.employeeRepository.GetEntityById(inputObject.EmployeeId);
            Task<Customer> customerTask = _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<ServiceInformation> serviceInformationTask = _unitOfWork.serviceInformationRepository.GetEntityById(inputObject.ServiceId);

            Task.WaitAll(employeeTask, customerTask, serviceInformationTask);

            Task<AddressBook> addressBookEmployeeTask = _unitOfWork.addressBookRepository.GetEntityById(employeeTask.Result.AddressId);
            Task<AddressBook> addressBookCustomerTask = _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result.AddressId);
            Task.WaitAll(addressBookEmployeeTask, addressBookCustomerTask);

            outObject.EmployeeName = addressBookEmployeeTask.Result.Name;
            outObject.ServiceDescription = serviceInformationTask.Result.ServiceDescription;
            outObject.CustomerName = addressBookCustomerTask.Result.Name;

            await Task.Yield();
            return outObject;
        }

        public async Task<IQueryable<ScheduleEvent>> GetViewsByEmployeeId(long? employeeId)
        {
            try
            {
                IQueryable<ScheduleEvent> query = await _unitOfWork.scheduleEventRepository.GetEntitiesByEmployeeId(employeeId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetScheduleEventsByEmployeeId", ex);
            }
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfScheduleEvent.ScheduleEventNumber.ToString());
        }
        public override async Task<ScheduleEventView> GetViewById(long? scheduleEventId)
        {
            ScheduleEvent detailItem = await _unitOfWork.scheduleEventRepository.GetEntityById(scheduleEventId);

            return await MapToView(detailItem);
        }
        public async Task<ScheduleEventView> GetViewByNumber(long scheduleEventNumber)
        {
            ScheduleEvent detailItem = await _unitOfWork.scheduleEventRepository.GetEntityByNumber(scheduleEventNumber);

            return await MapToView(detailItem);
        }

        public override async Task<ScheduleEvent> GetEntityById(long? scheduleEventId)
        {
            return await _unitOfWork.scheduleEventRepository.GetEntityById(scheduleEventId);

        }
        public async Task<ScheduleEvent> GetEntityByNumber(long scheduleEventNumber)
        {
            return await _unitOfWork.scheduleEventRepository.GetEntityByNumber(scheduleEventNumber);
        }
        public async Task<IList<ScheduleEventView>> GetViewsByServiceId(long? serviceId)
        {
            IList<ScheduleEvent> list = await _unitOfWork.scheduleEventRepository.GetEntitiesByServiceId(serviceId);
            IList<ScheduleEventView> views = new List<ScheduleEventView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }


        public async Task<IList<ScheduleEventView>> GetViewsByCustomerId(long? customerId, long? serviceId)
        {
            IList<ScheduleEvent> list = await _unitOfWork.scheduleEventRepository.GetEntitiesByCustomerId(customerId);
            if (serviceId > 0)
            {
                list = list.Where(e => e.ServiceId == serviceId).ToList<ScheduleEvent>();
            }
            IList<ScheduleEventView> views = new List<ScheduleEventView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }


    }
}
