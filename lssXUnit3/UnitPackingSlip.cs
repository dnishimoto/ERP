using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PackingSlipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.PackingSlipDomain
{

    public class UnitPackingSlip
    {

        private readonly ITestOutputHelper output;

        public UnitPackingSlip(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PackingSlipModule PackingSlipMod = new PackingSlipModule();
            Supplier supplier = await PackingSlipMod.Supplier.Query().GetEntityById(3);

            PackingSlipView view = new PackingSlipView()
            {
                SupplierId = supplier.SupplierId,
                ReceivedDate = DateTime.Parse("11/29/2019"),
                SlipDocument = "Slip-02",
                PONumber = "PO-2",
                Remark = "Remark comment",
                SlipType = "INBOUND",
                Amount = 267.9m
            };
            NextNumber nnNextNumber = await PackingSlipMod.PackingSlip.Query().GetNextNumber();

            view.PackingSlipNumber = nnNextNumber.NextNumberValue;

            PackingSlip packingSlip = await PackingSlipMod.PackingSlip.Query().MapToEntity(view);

            PackingSlipMod.PackingSlip.AddPackingSlip(packingSlip).Apply();

            PackingSlip newPackingSlip = await PackingSlipMod.PackingSlip.Query().GetEntityByNumber(view.PackingSlipNumber);

            Assert.NotNull(newPackingSlip);

            newPackingSlip.SlipDocument = "Slip-03 Update";

            PackingSlipMod.PackingSlip.UpdatePackingSlip(newPackingSlip).Apply();

            PackingSlipView updateView = await PackingSlipMod.PackingSlip.Query().GetViewById(newPackingSlip.PackingSlipId);

            Assert.Same(updateView.SlipDocument, "Slip-03 Update");
            PackingSlipMod.PackingSlip.DeletePackingSlip(newPackingSlip).Apply();
            PackingSlip lookupPackingSlip = await PackingSlipMod.PackingSlip.Query().GetEntityById(view.PackingSlipId);

            Assert.Null(lookupPackingSlip);
        }



    }
}
