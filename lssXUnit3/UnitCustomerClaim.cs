using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.CustomerClaimDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.CustomerClaimDomain
{

    public class UnitCustomerClaim
    {

        private readonly ITestOutputHelper output;

        public UnitCustomerClaim(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            AddressBook customerAddressBook = null;
            AddressBook employeeAddressBook = null;

            CustomerClaimModule CustomerClaimMod = new CustomerClaimModule();
            Udc udc = await CustomerClaimMod.Udc.Query().GetEntityById(39);
            Customer customer = await CustomerClaimMod.Customer.Query().GetEntityById(2);
            if (customer != null) customerAddressBook = await CustomerClaimMod.AddressBook.Query().GetEntityById(customer.AddressId);
            Employee employee = await CustomerClaimMod.Employee.Query().GetEntityById(3);
            if (employee != null) employeeAddressBook = await CustomerClaimMod.AddressBook.Query().GetEntityById(employee.AddressId);
            Udc groupUdc = await CustomerClaimMod.Udc.Query().GetEntityById(37);
            CustomerClaimView view = new CustomerClaimView()
            {
                ClassificationXrefId = udc.XrefId,
                Classification = udc.Value,
                CustomerId = customer.CustomerId,
                CustomerName = customerAddressBook?.Name,
                Configuration = "Javascript Dashboard",
                Note = "larger images",
                EmployeeId = employee?.EmployeeId ?? 0,
                EmployeeName=employeeAddressBook?.Name,
                  GroupIdXrefId=groupUdc.XrefId,
                  GroupId=groupUdc.Value,
                  CreatedDate=DateTime.Parse("11/16/2019")
            };
            NextNumber nnNextNumber = await CustomerClaimMod.CustomerClaim.Query().GetNextNumber();

            view.CustomerClaimNumber = nnNextNumber.NextNumberValue;

            CustomerClaim customerClaim = await CustomerClaimMod.CustomerClaim.Query().MapToEntity(view);

            CustomerClaimMod.CustomerClaim.AddCustomerClaim(customerClaim).Apply();

            CustomerClaim newCustomerClaim = await CustomerClaimMod.CustomerClaim.Query().GetEntityByNumber(view.CustomerClaimNumber);

            Assert.NotNull(newCustomerClaim);

            newCustomerClaim.Note = "larger image update";

            CustomerClaimMod.CustomerClaim.UpdateCustomerClaim(newCustomerClaim).Apply();

            CustomerClaimView updateView = await CustomerClaimMod.CustomerClaim.Query().GetViewById(newCustomerClaim.ClaimId);

            Assert.Same(updateView.Note, "large image update");
              CustomerClaimMod.CustomerClaim.DeleteCustomerClaim(newCustomerClaim).Apply();
            CustomerClaim lookupCustomerClaim= await CustomerClaimMod.CustomerClaim.Query().GetEntityById(view.ClaimId);

            Assert.Null(lookupCustomerClaim);
        }
       
      

    }
}
