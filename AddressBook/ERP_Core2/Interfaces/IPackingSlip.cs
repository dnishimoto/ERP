using MillenniumERP.PackingSlipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IPackingSlip
    {
        IPackingSlip CreatePackingSlip(PackingSlipView packingSlipView);

        IPackingSlip CreatePackingSlipDetails(PackingSlipView packingSlipView);
        IPackingSlip CreateInventoryByPackingSlip(PackingSlipView packingSlipView);
        IPackingSlip Apply();
    }
}
