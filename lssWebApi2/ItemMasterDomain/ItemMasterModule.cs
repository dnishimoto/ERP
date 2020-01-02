using lssWebApi2.AbstractFactory;
using lssWebApi2.ItemMasterDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.ItemMasterDomain
{
    public class ItemMasterModule : AbstractModule
    {
        public FluentItemMaster ItemMaster = new FluentItemMaster();
    }
}
