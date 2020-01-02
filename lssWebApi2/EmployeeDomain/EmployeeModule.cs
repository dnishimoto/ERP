using lssWebApi2.AbstractFactory;
using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EmployeeDomain
{
    public class EmployeeModule : AbstractModule
    {
        public FluentEmployee Employee = new FluentEmployee();
    }
}
