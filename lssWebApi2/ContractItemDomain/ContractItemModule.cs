using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ContractDomain;
using lssWebApi2.Services;

namespace lssWebApi2.ContractItemDomain
{
    public class ContractItemModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentContractItem ContractItem;
        public FluentContract Contract;
        public ContractItemModule()
        {
            unitOfWork = new UnitOfWork();
            ContractItem = new FluentContractItem(unitOfWork);
            Contract = new FluentContract(unitOfWork);
        }
    }
}

