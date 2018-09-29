using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.FluentAPI;
using ERP_Core2.Interfaces;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.CustomerDomain
{


    public class CustomerModule : AbstractModule
    {

        public FluentCustomer Customer = new FluentCustomer();

      }
}
