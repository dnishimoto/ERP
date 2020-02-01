using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountPayableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.AccountPayableDomain
{

    public class UnitAccountPayable
    {

        private readonly ITestOutputHelper output;

        public UnitAccountPayable(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AccountPayableModule AccountPayableMod = new AccountPayableModule();
            Supplier supplier = await AccountPayableMod.Supplier.Query().GetEntityById(3);
            ChartOfAccount chartOfAccount = await AccountPayableMod.ChartOfAccount.Query().GetEntityById(17);

            AccountPayableView view = new AccountPayableView()
            {
                DocNumber = 17,
                GrossAmount = 299.99M,
                DiscountAmount = null,
                Remark = null,
                Gldate = DateTime.Now,
                SupplierId = supplier.SupplierId,
                ContractId = null,
                PoquoteId = null,
                Description = "back to school",
                PurchaseOrderId = null,
                Tax = 16.08M,
                AccountId = chartOfAccount.AccountId,
                DocType = "STD",
                PaymentTerms = "Net 30",
                DiscountPercent = 0,
                AmountOpen = 0M,
                OrderNumber = "PO-2",
                AmountPaid = 299.99M
            };
            NextNumber nnNextNumber = await AccountPayableMod.AccountPayable.Query().GetNextNumber();

            view.AccountPayableNumber = nnNextNumber.NextNumberValue;

            AccountPayable accountPayable = await AccountPayableMod.AccountPayable.Query().MapToEntity(view);

            AccountPayableMod.AccountPayable.AddAccountPayable(accountPayable).Apply();

            AccountPayable newAccountPayable = await AccountPayableMod.AccountPayable.Query().GetEntityByNumber(view.AccountPayableNumber);

            Assert.NotNull(newAccountPayable);

            newAccountPayable.PaymentTerms = "Net 30 Update";

            AccountPayableMod.AccountPayable.UpdateAccountPayable(newAccountPayable).Apply();

            AccountPayableView updateView = await AccountPayableMod.AccountPayable.Query().GetViewById(newAccountPayable.AcctPayId);

            Assert.Same(updateView.PaymentTerms, "Net 30 Update");
            AccountPayableMod.AccountPayable.DeleteAccountPayable(newAccountPayable).Apply();
            AccountPayable lookupAccountPayable = await AccountPayableMod.AccountPayable.Query().GetEntityById(view.AcctPayId);

            Assert.Null(lookupAccountPayable);
        }



    }
}
