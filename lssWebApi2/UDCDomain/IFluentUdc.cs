

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.UDCDomain
{ 

public interface IFluentUdc
    {
        IFluentUdcQuery Query();
        IFluentUdc Apply();
        IFluentUdc AddUdc(Udc udc);
        IFluentUdc UpdateUdc(Udc udc);
        IFluentUdc DeleteUdc(Udc udc);
     	IFluentUdc UpdateUdcs(List<Udc> newObjects);
        IFluentUdc AddUdcs(List<Udc> newObjects);
        IFluentUdc DeleteUdcs(List<Udc> deleteObjects);
    }
}
