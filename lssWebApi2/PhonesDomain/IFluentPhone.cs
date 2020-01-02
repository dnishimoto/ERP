

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.AddressBookDomain
{ 

public interface IFluentPhone
    {
        IFluentPhoneQuery Query();
        IFluentPhone Apply();
        IFluentPhone AddPhones(PhoneEntity phones);
        IFluentPhone UpdatePhones(PhoneEntity phones);
        IFluentPhone DeletePhones(PhoneEntity phones);
     	IFluentPhone UpdatePhoness(List<PhoneEntity> newObjects);
        IFluentPhone AddPhoness(List<PhoneEntity> newObjects);
        IFluentPhone DeletePhoness(List<PhoneEntity> deleteObjects);
    }
}
