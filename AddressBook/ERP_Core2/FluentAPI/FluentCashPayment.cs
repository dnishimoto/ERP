using ERP_Core2.AbstractFactory;
using ERP_Core2.Interfaces;
using MillenniumERP.AccountsReceivableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentCustomerCashPayment : AbstractErrorHandling, IAccountsReceivableModule
    {


        public IGeneralLedger GeneralLedger()
        {
            return new FluentGeneralLedger() as IGeneralLedger;
        }
        public ICustomerLedger CustomerLedger()
        {
            return new FluentCustomerLedger() as ICustomerLedger;
        }
        public IAccountsReceivable AccountsReceivable()
        {
            return new FluentAccountsReceivable() as IAccountsReceivable;
        }


    }
}
