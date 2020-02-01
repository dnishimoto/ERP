using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;
using lssWebApi2.Interfaces;

using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.CustomerDomain
{

public class FluentCustomer :IFluentCustomer
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentCustomer(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentCustomerQuery Query()
        {
            return new FluentCustomerQuery(unitOfWork) as IFluentCustomerQuery;
        }
        public IFluentCustomer CreateCustomerByView(CustomerView view)
        {
            Task<Customer> customerTask = Task.Run(async()=>await MapToEntity(view));
            Task.WaitAll(customerTask);
            AddCustomer(customerTask.Result);
            return this as IFluentCustomer;
        }
        private async Task<Customer> MapToEntity(CustomerView inputObject)
        {
            Mapper mapper = new Mapper();
            Customer outObject = mapper.Map<Customer>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public IFluentCustomer Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentCustomer;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentCustomer AddCustomers(List<Customer> newObjects)
        {
            unitOfWork.customerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCustomer;
        }
        public IFluentCustomer UpdateCustomers(IList<Customer> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.customerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCustomer;
        }
        public IFluentCustomer AddCustomer(Customer newObject) {
            unitOfWork.customerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCustomer;
        }
        public IFluentCustomer UpdateCustomer(Customer updateObject) {
            unitOfWork.customerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCustomer;

        }
        public IFluentCustomer DeleteCustomer(Customer deleteObject) {
            unitOfWork.customerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCustomer;
        }
   	public IFluentCustomer DeleteCustomers(List<Customer> deleteObjects)
        {
            unitOfWork.customerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCustomer;
        }
    }
}
