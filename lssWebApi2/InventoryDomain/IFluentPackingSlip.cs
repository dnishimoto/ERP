using ERP_Core2.PackingSlipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentPackingSlip
    {
        IFluentPackingSlip CreatePackingSlip(PackingSlipView packingSlipView);

        IFluentPackingSlip CreatePackingSlipDetails(PackingSlipView packingSlipView);
        IFluentPackingSlip CreateInventoryByPackingSlip(PackingSlipView packingSlipView);
        IFluentPackingSlip Apply();
    }
}
