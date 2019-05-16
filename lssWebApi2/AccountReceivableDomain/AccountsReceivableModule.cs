using ERP_Core2.AbstractFactory;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.FluentAPI;
using ERP_Core2.GeneralLedgerDomain;
using System;

namespace ERP_Core2.AccountsReceivableDomain
{

    public enum PaymentStatus
    {

    }
    public class AccountsReceivableModule : AbstractModule
    {

        public FluentAccountsReceivable AccountsReceivable = new FluentAccountsReceivable();
        public FluentCustomerCashPayment CustomerCashPayment = new FluentCustomerCashPayment();

        public bool CreateCustomerCashPayment(GeneralLedgerView ledgerView)
        {
            try
            {
                CustomerCashPayment
                          .GeneralLedger
                          .CreateGeneralLedger(ledgerView)
                          .Apply();

                CustomerCashPayment
                            .CustomerLedger
                            .CreateCustomerLedger(ledgerView)
                            .Apply();

                CustomerCashPayment
                            .AccountsReceivable
                            .UpdateAccountReceivable(ledgerView)
                            .Apply();

                CustomerCashPayment
                            .GeneralLedger
                            .UpdateAccountBalances(ledgerView);

                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCustomerCashPayment", ex); }
        }
        public bool CreateCustomerLedger(GeneralLedgerView ledgerView)
        {
            try
            {
                CustomerCashPayment
                        .GeneralLedger.CreateGeneralLedger(ledgerView).Apply();

                GeneralLedgerView glView = CustomerCashPayment
                        .GeneralLedger
                        .Query()
                        .GetLedgerViewByExpression(e => e.AccountId == ledgerView.AccountId && e.Amount == ledgerView.Amount && e.Gldate == ledgerView.GLDate && e.DocNumber == ledgerView.DocNumber && e.CheckNumber == ledgerView.CheckNumber);

                ledgerView.GeneralLedgerId = glView.GeneralLedgerId;

                CustomerCashPayment
                     .CustomerLedger
                     .CreateCustomerLedger(ledgerView)
                     .Apply();

                CustomerCashPayment
                    .AccountsReceivable
                           .UpdateAccountReceivable(ledgerView)
                             .Apply();
                CustomerCashPayment
                          .GeneralLedger
                              .UpdateAccountBalances(ledgerView);
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCustomerLedger", ex); }
        }
    }
}
