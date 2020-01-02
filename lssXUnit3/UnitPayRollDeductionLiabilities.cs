using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.PayRollDomain
{

    public class UnitPayRollDeductionLiabilities
    {

        private readonly ITestOutputHelper output;

        public UnitPayRollDeductionLiabilities(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestGetDeductionLiabilitiesByCode()
        {
            PayRollDeductionLiabilitiesModule PayRollDeductionLiabilitiesMod = new PayRollDeductionLiabilitiesModule();
            int deductionLiabilities = 1;
            string deductionLiabilitiesType = "L";
            PayRollDeductionLiabilitiesView view = await PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.Query().GetViewByDeductionLiabilitiesCode(deductionLiabilities, deductionLiabilitiesType);
            Assert.NotNull(view);
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PayRollDeductionLiabilitiesModule PayRollDeductionLiabilitiesMod = new PayRollDeductionLiabilitiesModule();

            PayRollDeductionLiabilitiesView view = new PayRollDeductionLiabilitiesView()
            {
                DeductionLiabilitiesCode = 3,
                Amount = null,
                Percentage = 1.45M,
                Description = "FICA Medicare",
                DeductionLiabilitiesType = "L",
                LimitAmount = null
            };
            NextNumber nnNextNumber = await PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.Query().GetNextNumber();

            view.PayRollDeductionLiabilitiesNumber = nnNextNumber.NextNumberValue;

            PayRollDeductionLiabilities payRollDeductionLiabilities = await PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.Query().MapToEntity(view);

            PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.AddPayRollDeductionLiabilities(payRollDeductionLiabilities).Apply();

            PayRollDeductionLiabilities newPayRollDeductionLiabilities = await PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.Query().GetEntityByNumber(view.PayRollDeductionLiabilitiesNumber);

            Assert.NotNull(newPayRollDeductionLiabilities);

            newPayRollDeductionLiabilities.Description = "FICA Medicare Update";

            PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.UpdatePayRollDeductionLiabilities(newPayRollDeductionLiabilities).Apply();

            PayRollDeductionLiabilitiesView updateView = await PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.Query().GetViewById(newPayRollDeductionLiabilities.PayRollDeductionLiabilitiesId);

            Assert.Same(updateView.Description, "FICA Medicare Update");
            PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.DeletePayRollDeductionLiabilities(newPayRollDeductionLiabilities).Apply();
            PayRollDeductionLiabilities lookupPayRollDeductionLiabilities = await PayRollDeductionLiabilitiesMod.PayRollDeductionLiabilities.Query().GetEntityById(view.PayRollDeductionLiabilitiesId);

            Assert.Null(lookupPayRollDeductionLiabilities);
        }



    }
}
