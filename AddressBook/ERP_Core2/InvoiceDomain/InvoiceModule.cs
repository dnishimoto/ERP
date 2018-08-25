using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.Interfaces;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.GeneralLedgerDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.InvoiceDomain
{
     

    public class InvoiceModule : AbstractModule, IInvoiceModule
    {
       public IInvoice Invoice()
        {
            return new FluentInvoice() as IInvoice;
        }
        public IInvoiceDetail InvoiceDetail()
        {
            return new FluentInvoiceDetail() as IInvoiceDetail;
        }
        public IAccountsReceivable AccountsReceivable()
        {
            return new FluentAccountsReceivable() as IAccountsReceivable;
        }
        public IGeneralLedger GeneralLedger()
        {
            return new FluentGeneralLedger() as IGeneralLedger;
        }
     

    }
}
