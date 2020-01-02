using lssWebApi2.AbstractFactory;
using lssWebApi2.Interfaces;
using lssWebApi2.AccountsReceivableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.CustomerLedgerDomain;

namespace lssWebApi2.FluentAPI
{
    public class FluentCustomerCashPayment : AbstractErrorHandling
    {
           public FluentGeneralLedger GeneralLedger= new FluentGeneralLedger();
           public FluentCustomerLedger CustomerLedger= new FluentCustomerLedger() ;
           public FluentAccountReceivable AccountsReceivable = new FluentAccountReceivable() ;
    }
}
