using ERP_Core2.AbstractFactory;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.AddressBookDomain.Repository;

namespace ERP_Core2.AddressBookDomain
{
    public class CarrierView
    {
        public CarrierView() { }
        public CarrierView(Carrier carrier)
        {
            this.CarrierId = carrier.CarrierId;
            this.CarrierName = carrier.Address.CompanyName;
            this.CarrierType = carrier.CarrierTypeXref.Value.ToString();
        }


        public long? CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierType { get; set; }
    }

        public class CarrierRepository : Repository<Carrier>, ICarrierRepository
    {
            private ApplicationViewFactory applicationViewFactory;

            ListensoftwaredbContext _dbContext;
            public CarrierRepository(DbContext db) : base(db)
            {
                _dbContext = (ListensoftwaredbContext)db;
                applicationViewFactory = new ApplicationViewFactory();
            }
        public async Task<CarrierView> GetCarrierViewByCarrierId(long carrierId)
        {
            Carrier carrier = await GetObjectAsync(carrierId);
            return applicationViewFactory.MapCarrierView(carrier);
        }
    }

    

}
