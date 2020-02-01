
using lssWebApi2.AddressBookDomain;
using lssWebApi2.InventoryDomain;
using lssWebApi2.Services;
using lssWebApi2.TaxRatesByCodeDomain;
using lssWebApi2.CommentDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.CommentDomain
{

    public class UnitTaxesByCode
    {

        private readonly ITestOutputHelper output;

        public UnitTaxesByCode(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDeleteComment()
        {
           TaxRatesByCodeModule TaxRatesByCodeMod = new TaxRatesByCodeModule();

            TaxRatesByCodeView view = new TaxRatesByCodeView()
            {
                TaxCode= "StateTaxUT",
                TaxRate =4.85M,
                State="UT"
            };
            NextNumber nnNextNumber = await TaxRatesByCodeMod.TaxRatesByCode.Query().GetNextNumber();

            view.TaxRatesByCodeNumber = nnNextNumber.NextNumberValue;

            TaxRatesByCode taxRatesByCode = await TaxRatesByCodeMod.TaxRatesByCode.Query().MapToEntity(view);

            TaxRatesByCodeMod.TaxRatesByCode.AddTaxRatesByCode(taxRatesByCode).Apply();

            TaxRatesByCode newTaxRatesByCode = await TaxRatesByCodeMod.TaxRatesByCode.Query().GetEntityByNumber(view.TaxRatesByCodeNumber);

            Assert.NotNull(newTaxRatesByCode);

            newTaxRatesByCode.TaxRate = 4.86M;

            TaxRatesByCodeMod.TaxRatesByCode.UpdateTaxRatesByCode(newTaxRatesByCode).Apply();

            TaxRatesByCodeView updateView = await TaxRatesByCodeMod.TaxRatesByCode.Query().GetViewById(newTaxRatesByCode.TaxRatesByCodeId);

            string taxRatesByCodeString = updateView.TaxRate.ToString();
            Assert.NotSame(taxRatesByCodeString, "4.86");

            TaxRatesByCodeView lookupByCode = await TaxRatesByCodeMod.TaxRatesByCode.Query().GetViewByTaxCode(TypeofTaxRatesByCode.StateTaxUT.ToString());

            Assert.NotNull(lookupByCode);

            TaxRatesByCodeMod.TaxRatesByCode.DeleteTaxRatesByCode(newTaxRatesByCode).Apply();
            TaxRatesByCode lookupTaxRatesByCode = await TaxRatesByCodeMod.TaxRatesByCode.Query().GetEntityById(view.TaxRatesByCodeId);

            Assert.Null(lookupTaxRatesByCode);
        }
        [Fact]
        public async Task TestCommentView()
        {
            CommentModule invMod = new CommentModule();

            long CommentId = 21;
            CommentView view = await invMod.Comment.Query().GetViewById(CommentId);

            Assert.NotNull(view);

        }
      

    }
}
