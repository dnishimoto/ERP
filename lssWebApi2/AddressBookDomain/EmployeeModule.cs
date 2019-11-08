using ERP_Core2.AbstractFactory;
using ERP_Core2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain
{
    public class EmployeeModule : AbstractModule
    {
        public FluentEmployee Employee = new FluentEmployee();
    }
}
