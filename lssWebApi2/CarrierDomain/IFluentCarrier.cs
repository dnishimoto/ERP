

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.CarrierDomain
{ 

public interface IFluentCarrier
    {
        IFluentCarrierQuery Query();
        IFluentCarrier Apply();
        IFluentCarrier AddCarrier(Carrier carrier);
        IFluentCarrier UpdateCarrier(Carrier carrier);
        IFluentCarrier DeleteCarrier(Carrier carrier);
     	IFluentCarrier UpdateCarriers(IList<Carrier> newObjects);
        IFluentCarrier AddCarriers(List<Carrier> newObjects);
        IFluentCarrier DeleteCarriers(List<Carrier> deleteObjects);
    }
}
