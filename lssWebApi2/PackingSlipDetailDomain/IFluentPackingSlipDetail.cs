

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.PackingSlipDomain;

namespace lssWebApi2.PackingSlipDetailDomain
{ 

public interface IFluentPackingSlipDetail
    {
        IFluentPackingSlipDetailQuery Query();
        IFluentPackingSlipDetail Apply();
        IFluentPackingSlipDetail AddPackingSlipDetail(PackingSlipDetail packingSlipDetail);
        IFluentPackingSlipDetail UpdatePackingSlipDetail(PackingSlipDetail packingSlipDetail);
        IFluentPackingSlipDetail DeletePackingSlipDetail(PackingSlipDetail packingSlipDetail);
     	IFluentPackingSlipDetail UpdatePackingSlipDetails(IList<PackingSlipDetail> newObjects);
        IFluentPackingSlipDetail AddPackingSlipDetails(List<PackingSlipDetail> newObjects);
        IFluentPackingSlipDetail DeletePackingSlipDetails(List<PackingSlipDetail> deleteObjects);
        IFluentPackingSlipDetail CreatePackingSlipDetailsByView(PackingSlipView view);
    }
}
