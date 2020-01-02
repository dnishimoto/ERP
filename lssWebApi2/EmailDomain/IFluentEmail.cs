

using lssWebApi2.CustomerDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.AddressBookDomain
{ 

public interface IFluentEmail
    {
        IFluentEmailQuery Query();
        IFluentEmail Apply();
        IFluentEmail AddEmail(EmailEntity email);
        IFluentEmail UpdateEmail(EmailEntity email);
        IFluentEmail DeleteEmail(EmailEntity email);
     	IFluentEmail UpdateEmailsByList(List<EmailEntity> newObjects);
        IFluentEmail AddEmailsByList(List<EmailEntity> newObjects);
        IFluentEmail DeleteEmailsByList(List<EmailEntity> deleteObjects);
        IFluentEmail CreateCustomerEmail(CustomerView customerView);
        IFluentEmail CreateEmailByAddressId(long ?addressId, EmailEntity email);
        IFluentEmail CreateSupplierEmail(long ? addressId, EmailEntity email);
    }
}
