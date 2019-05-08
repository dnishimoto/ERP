
using ERP_Core2.AddressBookDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.InventoryDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.InventoryDomain
{

    public class UnitTestInventory
    {

        private readonly ITestOutputHelper output;

        public UnitTestInventory(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDeleteInventory()
        {
            InventoryModule invMod = new InventoryModule();
            InventoryView view = new InventoryView()
            {
                  ItemId=9,
                    Description="Highlighter - 3 Color",
                    Remarks ="Testing inventory add",
                    UnitOfMeasure ="Sets",
                    Quantity =10,
                    ExtendedPrice =100,
                    DistributionAccountId =5,
                    Branch="700"
            };
            NextNumber nnInventory = await invMod.Inventory.Query().GetInventoryNextNumber();

            view.InventoryNumber = nnInventory.NextNumberValue;

            Inventory inventory = await invMod.Inventory.Query().MapToInventoryEntity(view);

            invMod.Inventory.AddInventory(inventory).Apply();

            Inventory newInventory = await invMod.Inventory.Query().GetInventoryByNumber(view.InventoryNumber);

            Assert.NotNull(newInventory);

            newInventory.Description = "Testing inventory update";

            //TODO add packing slip

            invMod.Inventory.UpdateInventory(newInventory).Apply();

            InventoryView updateView = await invMod.Inventory.Query().GetInventoryViewbyId(newInventory.InventoryId);

            Assert.Same(updateView.Description, "Testing inventory update");

            invMod.Inventory.DeleteInventory(newInventory).Apply();
            Inventory lookupInventory = await invMod.Inventory.Query().GetInventoryById(view.InventoryId);

            Assert.Null(lookupInventory);
        }
        [Fact]
        public async Task TestInventoryView()
        {
            InventoryModule invMod = new InventoryModule();

            long inventoryId = 21;
            InventoryView view = await invMod.Inventory.Query().GetInventoryViewbyId(inventoryId);

            Assert.NotNull(view);

        }
      

    }
}
