using lssWebApi2.AbstractFactory;
using lssWebApi2.ContractItemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.ContractItemDomain
{
    public class ContractItemModule : AbstractModule
    {
        public FluentContractItem ContractItem = new FluentContractItem();
    }
}
