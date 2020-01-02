
using lssWebApi2.AddressBookDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.InventoryDomain
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
            ItemMaster itemMaster = await invMod.ItemMaster.Query().GetEntityById(9);
            ChartOfAccount chartOfAccount = await invMod.ChartOfAccount.Query().GetEntityById(5);

            InventoryView view = new InventoryView()
            {
                  ItemId=itemMaster.ItemId,
                    Description="Highlighter - 3 Color",
                    Remarks ="Testing inventory add",
                    UnitOfMeasure ="Sets",
                    Quantity =10,
                    ExtendedPrice =100,
                    DistributionAccountId =chartOfAccount.AccountId,
                    Branch="700",
                    ItemMasterView=await invMod.ItemMaster.Query().MapToView(itemMaster),
                    DistributionAccountView= await invMod.ChartOfAccount.Query().MapToView(chartOfAccount)


            };
            NextNumber nnInventory = await invMod.Inventory.Query().GetInventoryNextNumber();

            view.InventoryNumber = nnInventory.NextNumberValue;

            Inventory inventory = await invMod.Inventory.Query().MapToEntity(view);

            invMod.Inventory.AddInventory(inventory).Apply();

            Inventory newInventory = await invMod.Inventory.Query().GetInventoryByNumber(view.InventoryNumber);

            Assert.NotNull(newInventory);

            newInventory.Description = "Testing inventory update";

            //TODO add packing slip

            invMod.Inventory.UpdateInventory(newInventory).Apply();

            InventoryView updateView = await invMod.Inventory.Query().GetViewById(newInventory.InventoryId);

            Assert.Same(updateView.Description, "Testing inventory update");

            invMod.Inventory.DeleteInventory(newInventory).Apply();
            Inventory lookupInventory = await invMod.Inventory.Query().GetEntityById(view.InventoryId);

            Assert.Null(lookupInventory);
        }
        [Fact]
        public async Task TestInventoryView()
        {
            InventoryModule invMod = new InventoryModule();

            long inventoryId = 21;
            InventoryView view = await invMod.Inventory.Query().GetViewById(inventoryId);


            Assert.NotNull(view);

        }
      

    }
}
