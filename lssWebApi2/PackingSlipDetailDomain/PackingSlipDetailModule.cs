using lssWebApi2.AbstractFactory;
using lssWebApi2.PackingSlipDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.ItemMasterDomain;

namespace lssWebApi2.PackingSlipDetailDomain
{
    public class PackingSlipDetailModule : AbstractModule
    {
        public FluentPackingSlipDetail PackingSlipDetail = new FluentPackingSlipDetail();
        public FluentPackingSlip PackingSlip = new FluentPackingSlip();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
    }
}
