using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.Interfaces;
using MillenniumERP.CustomerLedgerDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace MillenniumERP.AccountsReceivableDomain
{
    public interface IAccountsReceivableModule
    {
        //IAccountsReceivableModule CreateGeneralLedger(GeneralLedgerView ledgerView);
        IGeneralLedger GeneralLedger();
        ICustomerLedger CustomerLedger();
        IAccountsReceivable AccountsReceivable();
     }
 

  
    public class FluentCustomerCashPayment: AbstractErrorHandling, IAccountsReceivableModule
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
    public class AccountsReceivableModule : AbstractModule
    {

        public FluentCustomerCashPayment CustomerCashPayment = new FluentCustomerCashPayment();

    }
}
