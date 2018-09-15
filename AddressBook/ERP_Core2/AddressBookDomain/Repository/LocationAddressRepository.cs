using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using ERP_Core2.AccountPayableDomain;

namespace ERP_Core2.AddressBookDomain
{
    public class LocationAddressView
    {
        public LocationAddressView() { }
        public LocationAddressView(LocationAddress locationAddress)
        {
            this.Address_Line1 = locationAddress.Address_Line_1;
            this.Address_Line2 = locationAddress.Address_Line_2;
            this.City = locationAddress.City;
            this.State = locationAddress.State;
            this.Zipcode = locationAddress.Zipcode;
            this.AddressId = locationAddress.AddressId;
            this.Country = locationAddress.Country;
            this.TypeXRefId = locationAddress.TypeXRefId;
            this.Type = locationAddress.UDC.Value;
        }
        public long AddressId { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public long TypeXRefId { get; set; }
    }



    public class LocationAddressRepository : Repository<LocationAddress>
    {
        private ApplicationViewFactory applicationViewFactory;

        Entities _dbContext;
        public LocationAddressRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateLocationUsingCustomer(CustomerView customerView)
        {
            try
            {

                foreach (LocationAddressView item in customerView.LocationAddress)
                {
                    var query = await (from e in _dbContext.LocationAddresses
                                       where e.AddressId == customerView.AddressId
                                       &&
                                       e.Address_Line_1 == item.Address_Line1
                                       &&
                                       e.City == item.City
                                       &&
                                       e.State == item.State
                                       &&
                                       e.Country == item.Country

                                       select e
                                ).FirstOrDefaultAsync<LocationAddress>();

                    if (query == null)
                    {
                        LocationAddress locationAddress = new LocationAddress();
                        UDC typeUDC = await base.GetUdc("LOCATIONADDRESS_TYPE", item.Type);
                        item.TypeXRefId = typeUDC.XRefId;
                        item.AddressId = customerView.AddressId;
                        applicationViewFactory.MapLocationAddressEntity(ref locationAddress, item);
                        AddObject(locationAddress);
                        return CreateProcessStatus.Insert;
                    }
                }
                return CreateProcessStatus.AlreadyExists;
               
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }


    }

}
