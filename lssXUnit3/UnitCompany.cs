using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.CompanyDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.CompanyDomain
{

    public class UnitCompany
    {

        private readonly ITestOutputHelper output;

        public UnitCompany(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            CompanyModule CompanyMod = new CompanyModule();

            CompanyView view = new CompanyView()
            {
                CompanyName = "test company",
                CompanyCode = "99",
                CompanyStreet = "99 street",
                CompanyCity = "99 city",
                CompanyState = "id",
                CompanyZipcode = "83709",
                TaxCode1 = "tax1",
                TaxCode2 = "tax2"

            };
            NextNumber nnNextNumber = await CompanyMod.Company.Query().GetNextNumber();

            view.CompanyNumber = nnNextNumber.NextNumberValue;

            Company company = await CompanyMod.Company.Query().MapToEntity(view);

            CompanyMod.Company.AddCompany(company).Apply();

            Company newCompany = await CompanyMod.Company.Query().GetEntityByNumber(view.CompanyNumber);

            Assert.NotNull(newCompany);

            newCompany.CompanyName = "test company (update)";

            CompanyMod.Company.UpdateCompany(newCompany).Apply();

            CompanyView updateView = await CompanyMod.Company.Query().GetViewById(newCompany.CompanyId);

            Assert.Same(updateView.CompanyName ,"test company (update)");
            CompanyMod.Company.DeleteCompany(newCompany).Apply();
            Company lookupCompany = await CompanyMod.Company.Query().GetEntityById(view.CompanyId);

            Assert.Null(lookupCompany);
        }



    }
}
