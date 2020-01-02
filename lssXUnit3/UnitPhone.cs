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

namespace lssWebApi2.AddressBookDomain
{

    public class UnitPhone
    {

        private readonly ITestOutputHelper output;

        public UnitPhone(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            PhoneModule PhoneMod = new PhoneModule();
            AddressBook addressBook = await PhoneMod.AddressBook.Query().GetEntityById(1);

           PhoneEntityView view = new PhoneEntityView()
            {
                    PhoneNumber="208-606-6785",
                    PhoneType="Mobile",
                    AddressId=addressBook.AddressId,
                    Name=addressBook.Name

            };
            NextNumber nnNextNumber = await PhoneMod.Phone.Query().GetNextNumber();

            view.PhoneEntityNumber = nnNextNumber.NextNumberValue;

            PhoneEntity phones = await PhoneMod.Phone.Query().MapToEntity(view);

            PhoneMod.Phone.AddPhones(phones).Apply();

            PhoneEntity newPhones = await PhoneMod.Phone.Query().GetEntityByNumber(view.PhoneEntityNumber);

            Assert.NotNull(newPhones);

            newPhones.PhoneType = "Mobile U";

            PhoneMod.Phone.UpdatePhones(newPhones).Apply();

            PhoneEntityView updateView = await PhoneMod.Phone.Query().GetViewById(newPhones.PhoneId);

            Assert.Same(updateView.PhoneType, "Mobile U");
              PhoneMod.Phone.DeletePhones(newPhones).Apply();
            PhoneEntity lookupPhones= await PhoneMod.Phone.Query().GetEntityById(view.PhoneId);

            Assert.Null(lookupPhones);
        }
       
      

    }
}
