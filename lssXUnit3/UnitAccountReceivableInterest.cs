using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountReceivableInterestDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.AccountReceivableInterestDomain
{

    public class UnitAccountReceivableInterest
    {

        private readonly ITestOutputHelper output;

        public UnitAccountReceivableInterest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AddressBook addressBook = null;
            AccountReceivableInterestModule AccountReceivableInterestMod = new AccountReceivableInterestModule();
            Customer customer = await AccountReceivableInterestMod.Customer.Query().GetEntityById(3);
            AccountReceivable accountReceivable = await AccountReceivableInterestMod.AccountReceivable.Query().GetEntityById(12);
            if (customer != null) addressBook = await AccountReceivableInterestMod.AddressBook.Query().GetEntityById(customer.AddressId);

           AccountReceivableInterestView view = new AccountReceivableInterestView()
            {
                   Amount=1M,
                   InterestRate=.035M,
                   InterestFromDate=DateTime.Parse("11/1/2019"),
                   InterestToDate=DateTime.Parse("12/7/2019"),
                   DocNumber=12,
                   PaymentTerms="Monthly",
                   PaymentDueDate=DateTime.Parse("12/31/2019"),
                   CustomerId=customer.CustomerId,
                   CustomerName=addressBook?.Name,
                   AccountReceivableId=accountReceivable.AccountReceivableId,
                   AcctRecDocType=accountReceivable.AcctRecDocType

            };
            NextNumber nnNextNumber = await AccountReceivableInterestMod.AccountReceivableInterest.Query().GetNextNumber();

            view.AccountReceivableInterestNumber = nnNextNumber.NextNumberValue;

            AccountReceivableInterest accountReceivableInterest = await AccountReceivableInterestMod.AccountReceivableInterest.Query().MapToEntity(view);

            AccountReceivableInterestMod.AccountReceivableInterest.AddAccountReceivableInterest(accountReceivableInterest).Apply();

            AccountReceivableInterest newAccountReceivableInterest = await AccountReceivableInterestMod.AccountReceivableInterest.Query().GetEntityByNumber(view.AccountReceivableInterestNumber);

            Assert.NotNull(newAccountReceivableInterest);

            newAccountReceivableInterest.PaymentTerms = "Weekly";

            AccountReceivableInterestMod.AccountReceivableInterest.UpdateAccountReceivableInterest(newAccountReceivableInterest).Apply();

            AccountReceivableInterestView updateView = await AccountReceivableInterestMod.AccountReceivableInterest.Query().GetViewById(newAccountReceivableInterest.AcctRecInterestId);

            Assert.Same(updateView.PaymentTerms, "Weekly");
              AccountReceivableInterestMod.AccountReceivableInterest.DeleteAccountReceivableInterest(newAccountReceivableInterest).Apply();
            AccountReceivableInterest lookupAccountReceivableInterest= await AccountReceivableInterestMod.AccountReceivableInterest.Query().GetEntityById(view.AcctRecInterestId);

            Assert.Null(lookupAccountReceivableInterest);
        }
       
      

    }
}
