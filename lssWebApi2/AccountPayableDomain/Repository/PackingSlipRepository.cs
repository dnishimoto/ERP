using System;
using System.Collections.Generic;
using lssWebApi2.entityframework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using System.Collections;
using ERP_Core2.GeneralLedgerDomain;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;
using ERP_Core2.AccountPayableDomain;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.PackingSlipDomain
{
    public class PackingSlipView
    {
        public PackingSlipView() { this.PackingSlipDetailViews = new List<PackingSlipDetailView>(); }
        public PackingSlipView(PackingSlip packingSlip)
        {
            this.PackingSlipId = packingSlip.PackingSlipId;
            this.SupplierId = packingSlip.SupplierId;
            this.ReceivedDate = packingSlip.ReceivedDate;
            this.SlipDocument = packingSlip.SlipDocument;
            this.PONumber = packingSlip.Ponumber;
            this.Remark = packingSlip.Remark;
            this.SlipType = packingSlip.SlipType;
            this.Amount = packingSlip.Amount;
            this.PackingSlipDetailViews = new List<PackingSlipDetailView>();
        }
        public long PackingSlipId { get; set; }
        public long SupplierId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string SlipDocument { get; set; }
        public string PONumber { get; set; }
        public string Remark { get; set; }
        public string SlipType { get; set; }
        public decimal? Amount { get; set; }
        public IList<PackingSlipDetailView> PackingSlipDetailViews { get; set; }
    }
    public class PackingSlipDetailView
    {
        public PackingSlipDetailView() { }
        public PackingSlipDetailView(PackingSlipDetail detail)
        {
            this.PackingSlipDetailId = detail.PackingSlipDetailId;
            this.PackingSlipId = detail.PackingSlipId;
            this.ItemId = detail.ItemId;
            this.Quantity = detail.Quantity;
            this.UnitPrice = detail.UnitPrice;
            this.ExtendedCost = detail.ExtendedCost;
            this.UnitOfMeasure = detail.UnitOfMeasure;
            this.Description = detail.Description;
        }
        public long PackingSlipDetailId { get; set; }
        public long PackingSlipId { get; set; }
        public long ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ExtendedCost { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }
    }




    public class PackingSlipRepository: Repository<PackingSlip>
    {
        public ListensoftwareDBContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public PackingSlipRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public async Task<PackingSlipView> GetPackingSlipViewBySlipDocument(string slipDocument)
        {
            try
            {
                List<PackingSlip> list = await GetObjectsQueryable(e => e.SlipDocument == slipDocument).ToListAsync<PackingSlip>();

                PackingSlipView view = applicationViewFactory.MapPackingSlipView(list[0]);

                var query = await (from e in _dbContext.PackingSlipDetail
                                   where e.PackingSlipId == view.PackingSlipId
                                   select e).ToListAsync<PackingSlipDetail>();
                foreach (var item in query)
                {
                    view.PackingSlipDetailViews.Add(applicationViewFactory.MapPackingSlipDetailView(item));

                }
                return view;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreatePackingSlipDetailsByView(PackingSlipView view)
        {
            try
            {
                PackingSlip packingSlip = await (from e in _dbContext.PackingSlip
                                                 where e.SlipDocument == view.SlipDocument
                                                 select e).FirstOrDefaultAsync<PackingSlip>();

                if (packingSlip != null)
                {
                    long packingSlipId = packingSlip.PackingSlipId;

                    foreach (var detail in view.PackingSlipDetailViews)
                    {
                        detail.PackingSlipId = packingSlipId;

                        PackingSlipDetail newDetail = new PackingSlipDetail();
                        applicationViewFactory.MapPackingSlipDetailEntity(ref newDetail, detail);

                        var queryDetail = await (from e in _dbContext.PackingSlipDetail
                                                 where e.ItemId == detail.ItemId
                                                 && e.PackingSlipId == newDetail.PackingSlipId
                                                 select e).FirstOrDefaultAsync<PackingSlipDetail>();
                        if (queryDetail == null)
                        {
                            _dbContext.Set<PackingSlipDetail>().Add(newDetail);

                        }
                    }
                    return CreateProcessStatus.Insert;
                }
                return CreateProcessStatus.AlreadyExists;

            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreatePackingSlipByView(PackingSlipView view)
        {
            decimal amount = 0;
            try
            {
                //check if packing slip exists
                var query = await (from e in _dbContext.PackingSlip
                                   where e.SlipDocument == view.SlipDocument
                              
                                   select e).FirstOrDefaultAsync<PackingSlip>();
                if (query != null) { return CreateProcessStatus.AlreadyExists; }


                foreach (var detail in view.PackingSlipDetailViews)
                {
                    amount += detail.ExtendedCost?? 0;
                }
                view.Amount = amount;

                PackingSlip packingSlip = new PackingSlip();
                applicationViewFactory.MapPackingSlipEntity(ref packingSlip, view);

                base.AddObject(packingSlip);

           
                return CreateProcessStatus.Insert;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<PackingSlip>> GetPackingSlipsByDocNumber(string PONumber)
        {
            try
            {
                List<PackingSlip> list = await GetObjectsQueryable(e => e.Ponumber == PONumber).ToListAsync<PackingSlip>();
                return list;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
  
      
    }
}
