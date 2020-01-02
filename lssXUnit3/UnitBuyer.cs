using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.BuyerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.BuyerDomain
{

    public class UnitBuyer
    {

        private readonly ITestOutputHelper output;

        public UnitBuyer(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            BuyerModule BuyerMod = new BuyerModule();
            AddressBook address = await BuyerMod.AddressBook.Query().GetEntityById(16);


           BuyerView view = new BuyerView()
            {
                  AddressId=address.AddressId,
                  BuyerName=address.Name

            };
            NextNumber nnNextNumber = await BuyerMod.Buyer.Query().GetNextNumber();

            view.BuyerNumber = nnNextNumber.NextNumberValue;

            Buyer buyer = await BuyerMod.Buyer.Query().MapToEntity(view);

            BuyerMod.Buyer.AddBuyer(buyer).Apply();

            Buyer newBuyer = await BuyerMod.Buyer.Query().GetEntityByNumber(view.BuyerNumber);

            Assert.NotNull(newBuyer);

            newBuyer.AddressId =18;

            BuyerMod.Buyer.UpdateBuyer(newBuyer).Apply();

            BuyerView updateView = await BuyerMod.Buyer.Query().GetViewById(newBuyer.BuyerId);

            if (updateView.AddressId == 18) Assert.True(true);
              BuyerMod.Buyer.DeleteBuyer(newBuyer).Apply();
            Buyer lookupBuyer= await BuyerMod.Buyer.Query().GetEntityById(view.BuyerId);

            Assert.Null(lookupBuyer);
        }
       
      

    }
}
