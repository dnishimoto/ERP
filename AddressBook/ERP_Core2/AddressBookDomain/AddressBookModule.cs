using ERP_Core2.AbstractFactory;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using System.Linq.Expressions;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.AddressBookDomain
{
   
    class AddressBookModule : AbstractModule
    {

        public FluentAddressBook AddressBook = new FluentAddressBook();
   
    }
}
