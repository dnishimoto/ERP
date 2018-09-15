using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.Interfaces;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.AccountsReceivableDomain
{
 
    public class AccountsReceivableModule : AbstractModule
    {

        public FluentCustomerCashPayment CustomerCashPayment = new FluentCustomerCashPayment();

    }
}
