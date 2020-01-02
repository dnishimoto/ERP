using lssWebApi2.AbstractFactory;
using lssWebApi2.CompanyDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.CompanyDomain
{
    public class CompanyModule : AbstractModule
    {
        public FluentCompany Company = new FluentCompany();
    }
}
