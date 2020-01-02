using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.PackingSlipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class InventoryModule
    {
        public FluentInventory Inventory = new FluentInventory();
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentPackingSlipDetail PackingSlipDetail = new FluentPackingSlipDetail();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();

        
    }
}
