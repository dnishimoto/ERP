
using ERP_Core2.AddressBookDomain;
using ERP_Core2.InventoryDomain;
using ERP_Core2.Services;
using ERP_Core2.TaxRatesByCodeDomain;
using lssWebApi2.CommentDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PayRollDomain;
using lssWebApi2.TaxRatesByCodeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.PayRollDomain
{

    public class UnitPayRollGroup
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollGroup(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDeletePayRoll()
        {
            PayRollGroupModule PayRollGroupMod = new PayRollGroupModule();

            PayRollGroupView view = new PayRollGroupView()
            {
                    Description ="PayRollGroup Test",
                    PayRollGroupCode=99

            };
            NextNumber nnNextNumber = await PayRollGroupMod.PayRollGroup.Query().GetNextNumber();

            view.PayRollGroupNumber = nnNextNumber.NextNumberValue;

            PayRollGroup payRollGroup = await PayRollGroupMod.PayRollGroup.Query().MapToEntity(view);

            PayRollGroupMod.PayRollGroup.AddPayRollGroup(payRollGroup).Apply();

            PayRollGroup newPayRollGroup = await PayRollGroupMod.PayRollGroup.Query().GetEntityByNumber(view.PayRollGroupNumber);

            Assert.NotNull(newPayRollGroup);

            newPayRollGroup.Description = "PayRollGroup Test Update";

            PayRollGroupMod.PayRollGroup.UpdatePayRollGroup(newPayRollGroup).Apply();

            PayRollGroupView updateView = await PayRollGroupMod.PayRollGroup.Query().GetViewById(newPayRollGroup.PayRollGroupId);

            Assert.Same(updateView.Description, "PayRollGroup Test Update");
            int code = 99;
           

            PayRollGroupView lookupByCode = await PayRollGroupMod.PayRollGroup.Query().GetViewByCode(code);

            Assert.NotNull(lookupByCode);

            PayRollGroupMod.PayRollGroup.DeletePayRollGroup(newPayRollGroup).Apply();
            PayRollGroup lookupPayRollGroup= await PayRollGroupMod.PayRollGroup.Query().GetEntityById(view.PayRollGroupId);

            Assert.Null(lookupPayRollGroup);
        }
       
      

    }
}
