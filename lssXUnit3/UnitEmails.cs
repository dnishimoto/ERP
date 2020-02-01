using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using lssWebApi2.EmailDomain;

namespace lssWebApi2.AddressBookDomain
{

    public class UnitEmails
    {

        private readonly ITestOutputHelper output;

        public UnitEmails(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            EmailModule EmailMod = new EmailModule();
            AddressBook addressBook = await EmailMod.AddressBook.Query().GetEntityById(1);

            EmailEntityView view = new EmailEntityView()
            {
                AddressId = addressBook.AddressId,
                Name=addressBook.Name,
                Email = "abc@123.com",
                Password = "123",
               LoginEmail =false
            };
            NextNumber nnNextNumber = await EmailMod.Email.Query().GetNextNumber();

            view.EmailEntityNumber = nnNextNumber.NextNumberValue;

            EmailEntity email = await EmailMod.Email.Query().MapToEntity(view);

            EmailMod.Email.AddEmail(email).Apply();

            EmailEntity newEmails = await EmailMod.Email.Query().GetEntityByNumber(view.EmailEntityNumber);

            Assert.NotNull(newEmails);

            newEmails.Email = "abc@123.com Update";

            EmailMod.Email.UpdateEmail(newEmails).Apply();

            EmailEntityView updateView = await EmailMod.Email.Query().GetViewById(newEmails.EmailId);

            Assert.Same(updateView.Email , "abc@123.com Update");
            EmailMod.Email.DeleteEmail(newEmails).Apply();
            EmailEntity lookupEmails = await EmailMod.Email.Query().GetEntityById(view.EmailId);

            Assert.Null(lookupEmails);
        }



    }
}
