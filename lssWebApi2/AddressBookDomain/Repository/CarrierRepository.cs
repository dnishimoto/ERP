﻿using ERP_Core2.AbstractFactory;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

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

        public class CarrierRepository : Repository<Carrier>
        {
            private ApplicationViewFactory applicationViewFactory;

            ListensoftwareDBContext _dbContext;
            public CarrierRepository(DbContext db) : base(db)
            {
                _dbContext = (ListensoftwareDBContext)db;
                applicationViewFactory = new ApplicationViewFactory();
            }
        public async Task<CarrierView> GetCarrierViewByCarrierId(long carrierId)
        {
            Carrier carrier = await GetObjectAsync(carrierId);
            return applicationViewFactory.MapCarrierView(carrier);
        }
    }

    

}
