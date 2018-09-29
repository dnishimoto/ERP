using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;

namespace ERP_Core2.AccountsReceivableDomain
{

    public class AccountsReceivableModule : AbstractModule
    {

        public FluentCustomerCashPayment CustomerCashPayment = new FluentCustomerCashPayment();

    }
}
