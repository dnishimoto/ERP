using ERP_Core2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.AddressBookDomain.Repository;

namespace ERP_Core2.AddressBookDomain
{ 
    public class BuyerView
    {
    public BuyerView() { }
    public BuyerView(Buyer buyer)
    {
        this.BuyerId = buyer.BuyerId;
        this.BuyerName= buyer.Address.Name;
        this.BuyerTitle = buyer.Title;
    }


    public long? BuyerId { get; set; }
    public string BuyerName { get; set; }
    public string BuyerTitle { get; set; }
}

public class BuyerRepository : Repository<Buyer>, IBuyerRepository
    {
        private ApplicationViewFactory applicationViewFactory;

        ListensoftwaredbContext _dbContext;
        public BuyerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<BuyerView> GetBuyerViewByBuyerId(long buyerId)
        {
            Buyer buyer = await GetObjectAsync(buyerId);
            return applicationViewFactory.MapBuyerView(buyer);
        }
    }
}
