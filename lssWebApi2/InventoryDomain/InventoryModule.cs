using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class InventoryModule
    {
        private UnitOfWork unitOfWork;
        public FluentInventory Inventory;
        public FluentPackingSlip PackingSlip;
        public FluentPackingSlipDetail PackingSlipDetail;
        public FluentItemMaster ItemMaster;
        public FluentChartOfAccount ChartOfAccount;

        public InventoryModule()
        {
            unitOfWork = new UnitOfWork();
            Inventory = new FluentInventory(unitOfWork);
            PackingSlip = new FluentPackingSlip(unitOfWork);
            PackingSlipDetail = new FluentPackingSlipDetail(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);

        }

    }
}
