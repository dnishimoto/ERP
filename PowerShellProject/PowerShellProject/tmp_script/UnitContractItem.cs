using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ContractItemDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ContractItemDomain
{

    public class UnitContractItem
    {

        private readonly ITestOutputHelper output;

        public UnitContractItem(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            ContractItemModule ContractItemMod = new ContractItemModule();

           ContractItemView view = new ContractItemView()
            {
                    Description = 'ContractItem Test',
                    ContractItemCode=99

            };
            NextNumber nnNextNumber = await ContractItemMod.ContractItem.Query().GetNextNumber();

            view.ContractItemNumber = nnNextNumber.NextNumberValue;

            ContractItem contractItem = await ContractItemMod.ContractItem.Query().MapToEntity(view);

            ContractItemMod.ContractItem.AddContractItem(contractItem).Apply();

            ContractItem newContractItem = await ContractItemMod.ContractItem.Query().GetEntityByNumber(view.ContractItemNumber);

            Assert.NotNull(newContractItem);

            newContractItem.Description = 'ContractItem Test Update';

            ContractItemMod.ContractItem.UpdateContractItem(newContractItem).Apply();

            ContractItemView updateView = await ContractItemMod.ContractItem.Query().GetViewById(newContractItem.ContractItemId);

            Assert.Same(updateView.Description, 'ContractItem Test Update');
              ContractItemMod.ContractItem.DeleteContractItem(newContractItem).Apply();
            ContractItem lookupContractItem= await ContractItemMod.ContractItem.Query().GetEntityById(view.ContractItemId);

            Assert.Null(lookupContractItem);
        }
       
      

    }
}
