using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.Services;

namespace lssWebApi2.PackingSlipDetailDomain
{
    public class PackingSlipDetailModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPackingSlipDetail PackingSlipDetail;
        public FluentPackingSlip PackingSlip;
        public FluentItemMaster ItemMaster;
        public PackingSlipDetailModule()
        {
            unitOfWork = new UnitOfWork();
            PackingSlipDetail = new FluentPackingSlipDetail(unitOfWork);
            PackingSlip = new FluentPackingSlip(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
        }

    }
}
