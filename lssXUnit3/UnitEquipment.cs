using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.EquipmentDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.EquipmentDomain
{

    public class UnitEquipment
    {

        private readonly ITestOutputHelper output;

        public UnitEquipment(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            EquipmentModule EquipmentMod = new EquipmentModule();

            EquipmentView view = new EquipmentView()
            {
                Model = "F-150",
                Make = "Ford",
                Vin = "V123",
                PurchasePrice = 28155.0M,
                CurrentAppraisalPrice = 19152.54M,
                SalesPrice = null,
                Description = "3.3L ti vct",
                SaleOption = null,
                YearPurchased = 2019,
                LocationCity = "Boise",
                LocationState = null,
                Category1 = null,
                Category2 = null,
                Category3 = null

            };
            NextNumber nnNextNumber = await EquipmentMod.Equipment.Query().GetNextNumber();

            view.EquipmentNumber = nnNextNumber.NextNumberValue;

            Equipment equipment = await EquipmentMod.Equipment.Query().MapToEntity(view);

            EquipmentMod.Equipment.AddEquipment(equipment).Apply();

            Equipment newEquipment = await EquipmentMod.Equipment.Query().GetEntityByNumber(view.EquipmentNumber);

            Assert.NotNull(newEquipment);

            newEquipment.Category1="Light Truck Update";

            EquipmentMod.Equipment.UpdateEquipment(newEquipment).Apply();

            EquipmentView updateView = await EquipmentMod.Equipment.Query().GetViewById(newEquipment.EquipmentId);

            Assert.Same(updateView.Category1, "Light Truck Update");
            EquipmentMod.Equipment.DeleteEquipment(newEquipment).Apply();
            Equipment lookupEquipment = await EquipmentMod.Equipment.Query().GetEntityById(view.EquipmentId);

            Assert.Null(lookupEquipment);
        }



    }
}
