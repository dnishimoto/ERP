using ERP_Core2.AbstractFactory;
using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.CustomerDomain;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AddressBookDomain
{
    public class LocationAddressView
    {
        public LocationAddressView() { }
        public LocationAddressView(LocationAddress locationAddress)
        {
            this.AddressLine1 = locationAddress.AddressLine1;
            this.AddressLine2 = locationAddress.AddressLine2;
            this.City = locationAddress.City;
            this.State = locationAddress.State;
            this.Zipcode = locationAddress.Zipcode;
            this.AddressId = locationAddress.AddressId;
            this.Country = locationAddress.Country;
            this.TypeXRefId = locationAddress.TypeXrefId;
            this.Type = locationAddress.TypeXref.Value;
        }
        public long AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
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

        ListensoftwareDBContext _dbContext;
        public LocationAddressRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateLocationUsingCustomer(CustomerView customerView)
        {
            try
            {

                foreach (LocationAddressView item in customerView.LocationAddress)
                {
                    var query = await (from e in _dbContext.LocationAddress
                                       where e.AddressId == customerView.AddressId
                                       &&
                                       e.AddressLine1 == item.AddressLine1
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
                        Udc typeUDC = await base.GetUdc("LOCATIONADDRESS_TYPE", item.Type);
                        item.TypeXRefId = typeUDC.XrefId;
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
