

using lssWebApi2.CustomerDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.LocationAddressDomain
{ 

public interface IFluentLocationAddress
    {
        IFluentLocationAddressQuery Query();
        IFluentLocationAddress Apply();
        IFluentLocationAddress AddLocationAddress(LocationAddress locationAddress);
        IFluentLocationAddress UpdateLocationAddress(LocationAddress locationAddress);
        IFluentLocationAddress DeleteLocationAddress(LocationAddress locationAddress);
     	IFluentLocationAddress UpdateLocationAddresses(List<LocationAddress> newObjects);
        IFluentLocationAddress AddLocationAddresses(List<LocationAddress> newObjects);
        IFluentLocationAddress DeleteLocationAddresses(List<LocationAddress> deleteObjects);
        IFluentLocationAddress CreateLocationUsingCustomer(CustomerView customerView);
        IFluentLocationAddress CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress);
    }
}
