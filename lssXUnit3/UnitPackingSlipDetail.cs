using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PackingSlipDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.PackingSlipDetailDomain
{

    public class UnitPackingSlipDetail
    {

        private readonly ITestOutputHelper output;

        public UnitPackingSlipDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PackingSlipDetailModule PackingSlipDetailMod = new PackingSlipDetailModule();

            PackingSlip packingSlip = await PackingSlipDetailMod.PackingSlip.Query().GetEntityById(9);
            ItemMaster itemMaster = await PackingSlipDetailMod.ItemMaster.Query().GetEntityById(5);

            PackingSlipDetailView view = new PackingSlipDetailView()
            {
                PackingSlipId = packingSlip.PackingSlipId,
                ItemId = itemMaster.ItemId,
                ItemCode=itemMaster.ItemCode,
                ItemDescription=itemMaster.Description,
                Branch=itemMaster.Branch,
                Quantity = 5,
                UnitPrice = 10M,
                ExtendedCost = 50M,
                UnitOfMeasure = "Each",
                Description = "Packing Slip Description"

            };
            NextNumber nnNextNumber = await PackingSlipDetailMod.PackingSlipDetail.Query().GetNextNumber();

            view.PackingSlipDetailNumber = nnNextNumber.NextNumberValue;

            PackingSlipDetail packingSlipDetail = await PackingSlipDetailMod.PackingSlipDetail.Query().MapToEntity(view);

            PackingSlipDetailMod.PackingSlipDetail.AddPackingSlipDetail(packingSlipDetail).Apply();

            PackingSlipDetail newPackingSlipDetial = await PackingSlipDetailMod.PackingSlipDetail.Query().GetEntityByNumber(view.PackingSlipDetailNumber);

            Assert.NotNull(newPackingSlipDetial);

            newPackingSlipDetial.Description = "Packing Slip Description Update";

            PackingSlipDetailMod.PackingSlipDetail.UpdatePackingSlipDetail(newPackingSlipDetial).Apply();

            PackingSlipDetailView updateView = await PackingSlipDetailMod.PackingSlipDetail.Query().GetViewById(newPackingSlipDetial.PackingSlipDetailId);

            Assert.Same(updateView.Description, "Packing Slip Description Update");
            PackingSlipDetailMod.PackingSlipDetail.DeletePackingSlipDetail(newPackingSlipDetial).Apply();
            PackingSlipDetail lookupPackingSlipDetial = await PackingSlipDetailMod.PackingSlipDetail.Query().GetEntityById(view.PackingSlipDetailId);

            Assert.Null(lookupPackingSlipDetial);
        }



    }
}
