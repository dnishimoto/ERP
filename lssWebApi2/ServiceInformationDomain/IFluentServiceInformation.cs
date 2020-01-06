

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ServiceInformationDomain;

namespace lssWebApi2.ServiceInformationDomain
{ 

public interface IFluentServiceInformation
    {
        IFluentServiceInformationQuery Query();
        IFluentServiceInformation Apply();
        IFluentServiceInformation AddServiceInformation(ServiceInformation serviceInformation);
        IFluentServiceInformation UpdateServiceInformation(ServiceInformation serviceInformation);
        IFluentServiceInformation DeleteServiceInformation(ServiceInformation serviceInformation);
     	IFluentServiceInformation UpdateServiceInformations(IList<ServiceInformation> newObjects);
        IFluentServiceInformation AddServiceInformations(List<ServiceInformation> newObjects);
        IFluentServiceInformation DeleteServiceInformations(List<ServiceInformation> deleteObjects);
    }
}
