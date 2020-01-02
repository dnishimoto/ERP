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
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.AddressBookDomain
{

    public class UnitLocationAddress
    {

        private readonly ITestOutputHelper output;

        public UnitLocationAddress(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            LocationAddressModule LocationAddressMod = new LocationAddressModule();
            AddressBook addressBook = await LocationAddressMod.AddressBook.Query().GetEntityById(1);
            Udc udc = await LocationAddressMod.Udc.Query().GetEntityById(60);

           LocationAddressView view = new LocationAddressView()
            {
                  AddressLine1="123 abc",
                  City="Boise",
                  Zipcode="83709",
                  TypeXrefId=udc.XrefId,
                  Type=udc.Value,
                  AddressId=addressBook.AddressId,
                  Name=addressBook.Name,
                  State="ID",
                  Country="USA"


            };
            NextNumber nnNextNumber = await LocationAddressMod.LocationAddress.Query().GetNextNumber();

            view.LocationAddressNumber = nnNextNumber.NextNumberValue;

            LocationAddress locationAddress = await LocationAddressMod.LocationAddress.Query().MapToEntity(view);

            LocationAddressMod.LocationAddress.AddLocationAddress(locationAddress).Apply();

            LocationAddress newLocationAddress = await LocationAddressMod.LocationAddress.Query().GetEntityByNumber(view.LocationAddressNumber);

            Assert.NotNull(newLocationAddress);

            newLocationAddress.Type = "Residence Update";

            LocationAddressMod.LocationAddress.UpdateLocationAddress(newLocationAddress).Apply();

            LocationAddressView updateView = await LocationAddressMod.LocationAddress.Query().GetViewById(newLocationAddress.LocationAddressId);

            Assert.Same(updateView.Type, "Residence Update");
              LocationAddressMod.LocationAddress.DeleteLocationAddress(newLocationAddress).Apply();
            LocationAddress lookupLocationAddress= await LocationAddressMod.LocationAddress.Query().GetEntityById(view.LocationAddressId);

            Assert.Null(lookupLocationAddress);
        }
       
      

    }
}
