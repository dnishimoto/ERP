using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ContractDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.ContractDomain
{

    public class UnitContract
    {

        private readonly ITestOutputHelper output;

        public UnitContract(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AddressBook addressBook = null;
            ContractModule ContractMod = new ContractModule();
            Customer customer = await ContractMod.Customer.Query().GetEntityById(2);
            Udc udc = await ContractMod.Udc.Query().GetEntityById(64);
            if (customer != null) addressBook=await ContractMod.AddressBook.Query().GetEntityById(customer.AddressId);

           ContractView view = new ContractView()
            {
                    CustomerId=customer.CustomerId,
                    CustomerName=addressBook.Name,
                    ServiceTypeXrefId=udc.XrefId,
                    ServiceType=udc.Value,
                    StartDate=DateTime.Parse("11/13/2019"),
                    EndDate=DateTime.Parse("11/13/2019"),
                    Cost=250.1M,
                    RemainingBalance=111.11M,
                    Title="Bob Test"

            };
            NextNumber nnNextNumber = await ContractMod.Contract.Query().GetNextNumber();

            view.ContractNumber = nnNextNumber.NextNumberValue;

            Contract contract = await ContractMod.Contract.Query().MapToEntity(view);

            ContractMod.Contract.AddContract(contract).Apply();

            Contract newContract = await ContractMod.Contract.Query().GetEntityByNumber(view.ContractNumber);

            Assert.NotNull(newContract);

            newContract.Title = "Bob Test Update";

            ContractMod.Contract.UpdateContract(newContract).Apply();

            ContractView updateView = await ContractMod.Contract.Query().GetViewById(newContract.ContractId);

            Assert.Same(updateView.Title, "Bob Test Update");
              ContractMod.Contract.DeleteContract(newContract).Apply();
            Contract lookupContract= await ContractMod.Contract.Query().GetEntityById(view.ContractId);

            Assert.Null(lookupContract);
        }
       
      

    }
}
