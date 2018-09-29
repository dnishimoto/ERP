using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.InvoiceDomain
{


    public class InvoiceModule : AbstractModule
    {
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentAccountsReceivable AccountsReceivable = new FluentAccountsReceivable();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
    }
}
