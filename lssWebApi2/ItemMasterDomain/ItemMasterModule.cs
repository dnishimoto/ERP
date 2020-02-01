using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace lssWebApi2.ItemMasterDomain
{
    public class ItemMasterModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentItemMaster ItemMaster;
        public ItemMasterModule()
        {
            unitOfWork = new UnitOfWork();
            ItemMaster = new FluentItemMaster(unitOfWork);
        }

    }
}
