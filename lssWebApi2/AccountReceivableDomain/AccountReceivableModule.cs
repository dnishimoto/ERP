using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.FluentAPI;
using lssWebApi2.GeneralLedgerDomain;
using System;
using System.Threading.Tasks;

namespace lssWebApi2.AccountsReceivableDomain
{

    public enum PaymentStatus
    {

    }
    public class AccountReceivableModule : AbstractModule
    {

        public FluentAccountReceivable AccountsReceivable = new FluentAccountReceivable();
        public FluentCustomerCashPayment CustomerCashPayment = new FluentCustomerCashPayment();
        public FluentAccountReceivableFee AccountReceivableFee = new FluentAccountReceivableFee();
        public FluentCustomerLedger CustomerLedger = new FluentCustomerLedger();

        public bool CreateCustomerCashPayment(GeneralLedgerView ledgerView)
        {
            try
            {
                CustomerCashPayment
                          .GeneralLedger
                          .CreateGeneralLedgerByView(ledgerView)
                          .Apply();

                CustomerCashPayment
                            .CustomerLedger
                            .Apply();
                CustomerLedger
                            .CreateEntityByGeneralLedgerView(ledgerView)
                            .Apply();

                CustomerCashPayment
                            .AccountsReceivable
                            .Apply();

                AccountsReceivable
                            .UpdateAccountReceivableByGeneralLedgerView(ledgerView)
                            .Apply();
                            

                CustomerCashPayment
                            .GeneralLedger
                            .UpdateAccountBalances(ledgerView);

                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCustomerCashPayment", ex); }
        }
        public async Task<bool> CreateCustomerLedger(GeneralLedgerView ledgerView)
        {
            try
            {
                CustomerCashPayment
                        .GeneralLedger.CreateGeneralLedgerByView(ledgerView).Apply();

                GeneralLedgerView glView = await CustomerCashPayment
                        .GeneralLedger
                        .Query()
                        .GetLedgerViewByExpression(e => e.AccountId == ledgerView.AccountId && e.Amount == ledgerView.Amount && e.Gldate == ledgerView.GLDate && e.DocNumber == ledgerView.DocNumber && e.CheckNumber == ledgerView.CheckNumber);

                ledgerView.GeneralLedgerId = glView.GeneralLedgerId;

                CustomerCashPayment
                     .CustomerLedger
                     .Apply();
                CustomerLedger
                     .CreateEntityByGeneralLedgerView(ledgerView)
                     .Apply();

                CustomerCashPayment
                    .AccountsReceivable
                           .UpdateAccountReceivableByGeneralLedgerView(ledgerView)
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
