using lssWebApi2.AbstractFactory;
using lssWebApi2.ContractItemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ContractDomain;

namespace lssWebApi2.ContractItemDomain
{
    public class ContractItemModule : AbstractModule
    {
        public FluentContractItem ContractItem = new FluentContractItem();
        public FluentContract Contract = new FluentContract();
    }
}
