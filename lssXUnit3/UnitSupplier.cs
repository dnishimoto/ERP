using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using lssWebApi2.SupplierDomain;


namespace lssWebApi2.AddressBookDomain
{

    public class UnitSupplier
    {

        private readonly ITestOutputHelper output;

        public UnitSupplier(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            SupplierModule SupplierMod = new SupplierModule();

           SupplierView view = new SupplierView()
            {
                    SupplierId=1,
                    AddressId=20,
                    Identification="test"

            };
            NextNumber nnNextNumber = await SupplierMod.Supplier.Query().GetNextNumber();

            view.SupplierNumber = nnNextNumber.NextNumberValue;

            Supplier supplier = await SupplierMod.Supplier.Query().MapToEntity(view);

            SupplierMod.Supplier.AddSupplier(supplier).Apply();

            Supplier newSupplier = await SupplierMod.Supplier.Query().GetEntityByNumber(view.SupplierNumber);

            Assert.NotNull(newSupplier);

            newSupplier.Identification = "test update" ;

            SupplierMod.Supplier.UpdateSupplier(newSupplier).Apply();

            SupplierView updateView = await SupplierMod.Supplier.Query().GetViewById(newSupplier.SupplierId);

            Assert.Same(updateView.Identification, "test update");
              SupplierMod.Supplier.DeleteSupplier(newSupplier).Apply();
            Supplier lookupSupplier= await SupplierMod.Supplier.Query().GetEntityById(view.SupplierId);

            Assert.Null(lookupSupplier);
        }
       
      

    }
}
