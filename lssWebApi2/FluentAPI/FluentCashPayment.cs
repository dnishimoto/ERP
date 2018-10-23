using ERP_Core2.AbstractFactory;
using ERP_Core2.Interfaces;
using ERP_Core2.AccountsReceivableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentCustomerCashPayment : AbstractErrorHandling
    {
           public FluentGeneralLedger GeneralLedger= new FluentGeneralLedger();
           public FluentCustomerLedger CustomerLedger= new FluentCustomerLedger() ;
           public FluentAccountReceivable AccountsReceivable = new FluentAccountReceivable() ;
    }
}
