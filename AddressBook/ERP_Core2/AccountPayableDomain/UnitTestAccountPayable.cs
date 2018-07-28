using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ERP_Core2.EntityFramework;
using Xunit.Abstractions;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using MillenniumERP.CustomerDomain;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using MillenniumERP.PurchaseOrderDomain;

namespace ERP_Core2.AccountPayableDomain
{
    
       public class UnitTestAccountPayable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly ITestOutputHelper output;

        public UnitTestAccountPayable(ITestOutputHelper output)
        {
            this.output = output;

        }
        [Fact]
        public void TestMatchReceiptToPurchaseOrder()
        {
        }
        [Fact]
        public void TestReceiveOpenPurchaseOrderDetail()
        {
        }
        [Fact]
        public void TestCreateAccountModel()
        {
            bool status = false;
            UnitOfWork unitOfWork = new UnitOfWork();

            AddressBook addressBook = new AddressBook();
            addressBook.CompanyName = "Sample Company Part Ltd";
            addressBook.Name = "";
            addressBook.FirstName = "";
            addressBook.LastName = "";

            LocationAddress locationAddress = new LocationAddress();
            locationAddress.Address_Line_1 = "204 Collins Street";
            locationAddress.City = "Melbourne";
            locationAddress.Zipcode = "3000";
            locationAddress.Country = "Australia";

            Email email = new Email();
            email.Email1 = "SampleCompany@Party.com";
            email.LoginEmail = true;
            email.Password = "123";

            unitOfWork.supplierRepository.CreateSupplierByAddressBook(addressBook,locationAddress,email);

            PurchaseOrderView purchaseOrderView = new PurchaseOrderView();


            
          
            Assert.True(true);
        }
     
    }
}
