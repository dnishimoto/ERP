using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.CustomerLedgerDomain;

using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.Services;
using lssWebApi2.UDCDomain;
using System;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableDomain
{

    public enum PaymentStatus
    {

    }
    public class AccountReceivableModule : AbstractModule
    {
        private UnitOfWork unitOfWork;

        public FluentAccountReceivable AccountReceivable;
        public FluentAccountReceivableFee AccountReceivableFee;
        public FluentCustomerLedger CustomerLedger;
        public FluentCustomer Customer;
        public FluentAddressBook AddressBook;
        public FluentChartOfAccount ChartOfAccount;
        public FluentPurchaseOrder PurchaseOrder;
        public FluentPurchaseOrderDetail PurchaseOrderDetail;
        public FluentGeneralLedger GeneralLedger;
        public FluentUdc Udc;

        public AccountReceivableModule()
        {
            unitOfWork = new UnitOfWork();
            AccountReceivable = new FluentAccountReceivable(unitOfWork);
            AccountReceivableFee = new FluentAccountReceivableFee(unitOfWork);
            CustomerLedger = new FluentCustomerLedger(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            PurchaseOrder = new FluentPurchaseOrder(unitOfWork);
            PurchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
        }



        public async Task<bool> CreateCustomerCashPayment(GeneralLedgerView ledgerView)
        {
            try
            {
                GeneralLedger
                          .CreateGeneralLedgerByView(ledgerView)
                          .Apply();

                CustomerLedger
                            .Apply();

                await CustomerLedger
                            .CreateEntityByGeneralLedgerView(ledgerView);
                CustomerLedger.Apply();

                AccountReceivable
                            .UpdateAccountReceivableByGeneralLedgerView(ledgerView)
                            .Apply();


                GeneralLedger
                            .UpdateAccountBalances(ledgerView);

                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCustomerCashPayment", ex); }
        }
        public async Task<bool> CreateCustomerLedger(GeneralLedgerView ledgerView)
        {
            try
            {
                GeneralLedger.CreateGeneralLedgerByView(ledgerView).Apply();

                GeneralLedgerView glView = await GeneralLedger
                        .Query()
                        .GetLedgerViewByExpression(e => e.AccountId == ledgerView.AccountId && e.Amount == ledgerView.Amount && e.Gldate == ledgerView.GLDate && e.DocNumber == ledgerView.DocNumber && e.CheckNumber == ledgerView.CheckNumber);

                ledgerView.GeneralLedgerId = glView.GeneralLedgerId;

                CustomerLedger
                     .Apply();
                await CustomerLedger
                     .CreateEntityByGeneralLedgerView(ledgerView);
                CustomerLedger.Apply();

                AccountReceivable
                           .UpdateAccountReceivableByGeneralLedgerView(ledgerView)
                             .Apply();
                GeneralLedger
                              .UpdateAccountBalances(ledgerView);
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCustomerLedger", ex); }
        }
    }
}
