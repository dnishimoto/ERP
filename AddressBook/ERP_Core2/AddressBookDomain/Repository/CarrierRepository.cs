﻿using ERP_Core2.AbstractFactory;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;

namespace ERP_Core2.AddressBookDomain
{
    public class CarrierView
    {
        public CarrierView() { }
        public CarrierView(Carrier carrier)
        {
            this.CarrierId = carrier.CarrierId;
            this.CarrierName = carrier.AddressBook.CompanyName;
            this.CarrierType = carrier.UDC.Value.ToString();
        }


        public long? CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierType { get; set; }
    }

        public class CarrierRepository : Repository<Carrier>
        {
            private ApplicationViewFactory applicationViewFactory;

            Entities _dbContext;
            public CarrierRepository(DbContext db) : base(db)
            {
                _dbContext = (Entities)db;
                applicationViewFactory = new ApplicationViewFactory();
            }
        public async Task<CarrierView> GetCarrierViewByCarrierId(long carrierId)
        {
            Carrier carrier = await GetObjectAsync(carrierId);
            return applicationViewFactory.MapCarrierView(carrier);
        }
    }

    

}
