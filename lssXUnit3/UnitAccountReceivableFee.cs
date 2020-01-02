using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountReceivableDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.AccountReceivableDomain
{

    public class UnitAccountReceivableFee
    {

        private readonly ITestOutputHelper output;

        public UnitAccountReceivableFee(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AddressBook addressBook = null;
            AccountReceivableFeeModule AccountReceivableFeeMod = new AccountReceivableFeeModule();
            Customer customer = await AccountReceivableFeeMod.Customer.Query().GetEntityById(3);
            if (customer != null)  addressBook= await AccountReceivableFeeMod.AddressBook.Query().GetEntityById(customer.AddressId);
            AccountReceivable accountReceivable = await AccountReceivableFeeMod.AccountReceivable.Query().GetEntityById(12);
            AccountReceivableFeeView view = new AccountReceivableFeeView()
            {
                    FeeAmount=25M,
                    PaymentDueDate=DateTime.Parse("12/7/2019"),
                    CustomerId=customer.CustomerId,
                    CustomerName=addressBook?.Name,
                    DocNumber=12,
                    AcctRecDocType="INV",
                    AcctRecId=accountReceivable.AcctRecId
                  

            };
            NextNumber nnNextNumber = await AccountReceivableFeeMod.AccountReceivableFee.Query().GetNextNumber();

            view.AccountReceivableFeeNumber = nnNextNumber.NextNumberValue;

            AccountReceivableFee accountReceivableFee = await AccountReceivableFeeMod.AccountReceivableFee.Query().MapToEntity(view);

            AccountReceivableFeeMod.AccountReceivableFee.AddAccountReceivableFee(accountReceivableFee).Apply();

            AccountReceivableFee newAccountReceivableFee = await AccountReceivableFeeMod.AccountReceivableFee.Query().GetEntityByNumber(view.AccountReceivableFeeNumber);

            Assert.NotNull(newAccountReceivableFee);

            newAccountReceivableFee.AcctRecDocType = "INV2";

            AccountReceivableFeeMod.AccountReceivableFee.UpdateAccountReceivableFee(newAccountReceivableFee).Apply();

            AccountReceivableFeeView updateView = await AccountReceivableFeeMod.AccountReceivableFee.Query().GetViewById(newAccountReceivableFee.AcctRecFeeId);

            Assert.Same(updateView.AcctRecDocType, "INV2");
              AccountReceivableFeeMod.AccountReceivableFee.DeleteAccountReceivableFee(newAccountReceivableFee).Apply();
            AccountReceivableFee lookupAccountReceivableFee= await AccountReceivableFeeMod.AccountReceivableFee.Query().GetEntityById(view.AcctRecFeeId);

            Assert.Null(lookupAccountReceivableFee);
        }
       
      

    }
}
