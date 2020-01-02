using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;
using lssWebApi2.CustomerDomain;
using System.Threading.Tasks;

namespace lssWebApi2.LocationAddressDomain
{

    public class FluentLocationAddress : IFluentLocationAddress
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentLocationAddress() { }
        public IFluentLocationAddressQuery Query()
        {
            return new FluentLocationAddressQuery(unitOfWork) as IFluentLocationAddressQuery;
        }
        public IFluentLocationAddress Apply()
        {
            try
            {
                if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentLocationAddress;
            }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentLocationAddress CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress)

        {

            locationAddress.AddressId = addressId;



            Task<IList<LocationAddress>> locationAddressListTask = Task.Run(async () => await unitOfWork.locationAddressRepository.GetEntitiesByAddressId(addressId));
            Task.WaitAll(locationAddressListTask);

            if (locationAddressListTask.Result == null)

            {
                AddLocationAddress(locationAddress);
                return this as IFluentLocationAddress;
             }

            processStatus=CreateProcessStatus.AlreadyExists;
            return this as IFluentLocationAddress;
        }
        public IFluentLocationAddress CreateLocationUsingCustomer(CustomerView customerView)
        {
            try
            {
                Task<LocationAddress> locationAddressTask = Task.Run(async () => await unitOfWork.locationAddressRepository.GetEntityByCustomer(customerView));
                Task.WaitAll(locationAddressTask);

                if (locationAddressTask.Result != null)
                {
                    AddLocationAddress(locationAddressTask.Result);
                    processStatus = CreateProcessStatus.Insert;
                    return this as IFluentLocationAddress;
                }

                processStatus = CreateProcessStatus.AlreadyExists;
                return this as IFluentLocationAddress;
            }
            catch (Exception ex) { throw new Exception("CreateLocationUsingCustomer", ex); }

        }
        public IFluentLocationAddress AddLocationAddresses(List<LocationAddress> newObjects)
        {
            unitOfWork.locationAddressRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentLocationAddress;
        }
        public IFluentLocationAddress UpdateLocationAddresses(List<LocationAddress> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.locationAddressRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentLocationAddress;
        }
        public IFluentLocationAddress AddLocationAddress(LocationAddress newObject)
        {
            unitOfWork.locationAddressRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentLocationAddress;
        }
        public IFluentLocationAddress UpdateLocationAddress(LocationAddress updateObject)
        {
            unitOfWork.locationAddressRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentLocationAddress;

        }
        public IFluentLocationAddress DeleteLocationAddress(LocationAddress deleteObject)
        {
            unitOfWork.locationAddressRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentLocationAddress;
        }
        public IFluentLocationAddress DeleteLocationAddresses(List<LocationAddress> deleteObjects)
        {
            unitOfWork.locationAddressRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentLocationAddress;
        }
    }
}
