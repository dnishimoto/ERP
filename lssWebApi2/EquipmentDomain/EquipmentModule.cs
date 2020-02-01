using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EquipmentDomain
{
    public class EquipmentModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentEquipment Equipment;
        public EquipmentModule()
        {
            unitOfWork = new UnitOfWork();
            Equipment = new FluentEquipment(unitOfWork);
        }
    }
}
