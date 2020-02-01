using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.CustomerClaimDomain
{
    public class FluentCustomerClaimQuery : MapperAbstract<CustomerClaim,CustomerClaimView>, IFluentCustomerClaimQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCustomerClaimQuery() { }
        public FluentCustomerClaimQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<CustomerClaim> MapToEntity(CustomerClaimView inputObject)
        {
          
            CustomerClaim outObject = mapper.Map<CustomerClaim>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<CustomerClaim>> MapToEntity(IList<CustomerClaimView> inputObjects)
        {
            IList<CustomerClaim> list = new List<CustomerClaim>();
           
            foreach (var item in inputObjects)
            {
                CustomerClaim outObject = mapper.Map<CustomerClaim>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<IList<CustomerClaimView>> GetCustomerClaimsByCustomerId(long customerId)
        {
            try
            {
                IList<CustomerClaim> resultList = await _unitOfWork.customerClaimRepository.GetEntitiesByCustomerId(customerId);

                IList<CustomerClaimView> list = new List<CustomerClaimView>();
                foreach (var item in resultList)
                {
                    list.Add(await MapToView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception("GetCustomerClaimsByCustomerId", ex); }
        }

        public override async Task<CustomerClaimView> MapToView(CustomerClaim inputObject)
        {
          
            CustomerClaimView outObject = mapper.Map<CustomerClaimView>(inputObject);


            AddressBook customerAddressBook = null;
            AddressBook employeeAddressBook = null;

            Task<Udc> classificationTask = _unitOfWork.udcRepository.GetEntityById(inputObject.ClassificationXrefId);
            Task<Customer> customerTask = _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<Employee> employeeTask = _unitOfWork.employeeRepository.GetEntityById(inputObject.EmployeeId);
            Task<Udc> groupIdTask = _unitOfWork.udcRepository.GetEntityById(inputObject.GroupIdXrefId);
            Task.WaitAll(classificationTask, customerTask, classificationTask,groupIdTask);

            if(customerTask.Result!=null) customerAddressBook = await _unitOfWork.addressBookRepository.GetEntityByCustomerId(customerTask.Result.AddressId);
            if (employeeTask.Result != null) employeeAddressBook = await _unitOfWork.addressBookRepository.GetEntityById(employeeTask.Result.AddressId);            

            outObject.Classification = classificationTask.Result.Value;
            outObject.CustomerName = customerAddressBook?.Name;
            outObject.EmployeeName = employeeAddressBook?.Name;
            outObject.GroupId = groupIdTask.Result.Value;

            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfCustomerClaim.CustomerClaimNumber.ToString());
        }
        public override async Task<CustomerClaimView> GetViewById(long ? customerClaimId)
        {
            CustomerClaim detailItem = await _unitOfWork.customerClaimRepository.GetEntityById(customerClaimId);

            return await MapToView(detailItem);
        }
        public async Task<CustomerClaimView> GetViewByNumber(long customerClaimNumber)
        {
            CustomerClaim detailItem = await _unitOfWork.customerClaimRepository.GetEntityByNumber(customerClaimNumber);

            return await MapToView(detailItem);
        }

        public override async Task<CustomerClaim> GetEntityById(long ? customerClaimId)
        {
            return await _unitOfWork.customerClaimRepository.GetEntityById(customerClaimId);

        }
        public async Task<CustomerClaim> GetEntityByNumber(long customerClaimNumber)
        {
            return await _unitOfWork.customerClaimRepository.GetEntityByNumber(customerClaimNumber);
        }
    }
}
