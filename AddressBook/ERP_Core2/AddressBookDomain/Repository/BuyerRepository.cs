using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;

namespace ERP_Core2.AddressBookDomain
{ 
    public class BuyerView
    {
    public BuyerView() { }
    public BuyerView(Buyer buyer)
    {
        this.BuyerId = buyer.BuyerId;
        this.BuyerName= buyer.AddressBook.Name;
        this.BuyerTitle = buyer.Title;
    }


    public long? BuyerId { get; set; }
    public string BuyerName { get; set; }
    public string BuyerTitle { get; set; }
}

public class BuyerRepository : Repository<Buyer> {
        private ApplicationViewFactory applicationViewFactory;

        Entities _dbContext;
        public BuyerRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<BuyerView> GetBuyerViewByBuyerId(long buyerId)
        {
            Buyer buyer = await GetObjectAsync(buyerId);
            return applicationViewFactory.MapBuyerView(buyer);
        }
    }
}
