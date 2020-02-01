using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;

namespace lssWebApi2.CompanyDomain
{
    public class CompanyModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentCompany Company;
        public CompanyModule()
        {
            unitOfWork = new UnitOfWork();
            Company = new FluentCompany(unitOfWork);
        }
    }
}
