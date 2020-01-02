using lssWebApi2.AbstractFactory;
using lssWebApi2.EquipmentDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.EquipmentDomain
{
    public class EquipmentModule : AbstractModule
    {
        public FluentEquipment Equipment = new FluentEquipment();
    }
}
