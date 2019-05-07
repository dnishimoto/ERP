
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

namespace ERP_Core2.ProjectManagementDomain
{

    public class UnitTestInventory
    {

        private readonly ITestOutputHelper output;

        public UnitTestInventory(ITestOutputHelper output)
        {
            this.output = output;
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
