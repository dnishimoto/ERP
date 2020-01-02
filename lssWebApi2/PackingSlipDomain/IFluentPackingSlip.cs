

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.PackingSlipDomain;

namespace lssWebApi2.PackingSlipDomain
{ 

public interface IFluentPackingSlip
    {
        IFluentPackingSlipQuery Query();
        IFluentPackingSlip Apply();
        IFluentPackingSlip AddPackingSlip(PackingSlip packingSlip);
        IFluentPackingSlip UpdatePackingSlip(PackingSlip packingSlip);
        IFluentPackingSlip DeletePackingSlip(PackingSlip packingSlip);
     	IFluentPackingSlip UpdatePackingSlips(List<PackingSlip> newObjects);
        IFluentPackingSlip AddPackingSlips(List<PackingSlip> newObjects);
        IFluentPackingSlip DeletePackingSlips(List<PackingSlip> deleteObjects);
        IFluentPackingSlip CreatePackingSlipByView(PackingSlipView view);
    }
}
