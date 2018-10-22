using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.SupplierInvoicesDomain
{
    public class SupplierInvoiceView
    {
        public SupplierInvoiceView()
        {
            SupplierInvoiceDetailViews = new List<SupplierInvoiceDetailView>();
        }
        public SupplierInvoiceView(SupplierInvoice supplierInvoice)
        {
            this.SupplierInvoiceId = supplierInvoice.SupplierInvoiceId;
            this.SupplierInvoiceNumber = supplierInvoice.SupplierInvoiceNumber;
            this.SupplierInvoiceDate = supplierInvoice.SupplierInvoiceDate;
            this.Amount = supplierInvoice.Amount;
            this.Description = supplierInvoice.Description;
            this.TaxAmount = supplierInvoice.TaxAmount;
            this.PaymentDueDate = supplierInvoice.PaymentDueDate;
            this.PaymentTerms = supplierInvoice.PaymentTerms;
            this.DiscountDueDate = supplierInvoice.DiscountDueDate;
            this.DiscountAmount = supplierInvoice.DiscountAmount;
            this.PONumber = supplierInvoice.Ponumber;
            this.SupplierId = supplierInvoice.SupplierId;
            SupplierInvoiceDetailViews = new List<SupplierInvoiceDetailView>();

        }
        public long? SupplierInvoiceId { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public DateTime? SupplierInvoiceDate { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? FreightCost { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string PaymentTerms { get; set; }
        public string PONumber { get; set; }
        public long? SupplierId { get; set; }
        public IList<SupplierInvoiceDetailView> SupplierInvoiceDetailViews { get; set; }
    }
    public class SupplierInvoiceDetailView
    {
        public SupplierInvoiceDetailView() { }
        public SupplierInvoiceDetailView(SupplierInvoiceDetail supplierInvoiceDetail)
        {
            this.SupplierInvoiceDetailId = supplierInvoiceDetail.SupplierInvoiceDetailId;
            this.UnitOfMeasure = supplierInvoiceDetail.UnitOfMeasure;
            this.Quantity = supplierInvoiceDetail.Quantity;
            this.UnitPrice = supplierInvoiceDetail.UnitPrice;
            this.ExtendedCost = supplierInvoiceDetail.ExtendedCost;
            this.ItemId = supplierInvoiceDetail.Item.ItemId;
            this.ItemDescription = supplierInvoiceDetail.Description;
            this.SupplierInvoiceId = supplierInvoiceDetail.SupplierInvoiceId;
            this.DiscountAmount = supplierInvoiceDetail.DiscountAmount;
            this.DiscountPercent = supplierInvoiceDetail.DiscountPercent;
        }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ExtendedCost { get; set; }

        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? DiscountDueDate { get; set; }

        public long? ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }

        public long? SupplierInvoiceId { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public long? SupplierInvoiceDetailId { get; set; }
        public string Description { get; set; }
    }

    public class SupplierInvoiceRepository : Repository<SupplierInvoice>
    {
        public ListensoftwaredbContext _dbContext;
        public ApplicationViewFactory applicationViewFactory;
        public SupplierInvoiceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<SupplierInvoice> GetSupplierInvoiceByPONumber(string poNumber)
        {
            try
            {
                var query = await (from e in _dbContext.SupplierInvoice
                                   where e.Ponumber == poNumber

                                   select e).FirstOrDefaultAsync<SupplierInvoice>();

                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreateSupplierInvoiceByView(SupplierInvoiceView view)
        {
            decimal amount = 0;
            try
            {
                //check if packing slip exists
                var query = await (from e in _dbContext.SupplierInvoice
                                   where e.SupplierInvoiceNumber == view.SupplierInvoiceNumber

                                   select e).FirstOrDefaultAsync<SupplierInvoice>();

                if (query != null) { return CreateProcessStatus.AlreadyExists; }


                foreach (var detail in view.SupplierInvoiceDetailViews)
                {
                    amount += detail.ExtendedCost ?? 0;
                }
                view.Amount = amount;


                SupplierInvoice supplierInvoice = new SupplierInvoice();
                applicationViewFactory.MapSupplierInvoiceEntity(ref supplierInvoice, view);

                base.AddObject(supplierInvoice);

                return CreateProcessStatus.Insert;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<CreateProcessStatus> CreateSupplierInvoiceDetailsByView(SupplierInvoiceView view)

        {

            var query = await (from e in _dbContext.SupplierInvoice
                               where e.SupplierInvoiceNumber == view.SupplierInvoiceNumber

                               select e).FirstOrDefaultAsync<SupplierInvoice>();

            if (query !=null)
            {
                long supplierInvoiceId = query.SupplierInvoiceId;

                foreach (var detail in view.SupplierInvoiceDetailViews)
                {
                    detail.SupplierInvoiceId = supplierInvoiceId;

                    SupplierInvoiceDetail newDetail = new SupplierInvoiceDetail();
                    applicationViewFactory.MapSupplierInvoiceDetailEntity(ref newDetail, detail);

                    var queryDetail = await (from e in _dbContext.SupplierInvoiceDetail
                                             where e.ItemId == detail.ItemId
                                             && e.SupplierInvoiceDetailId == newDetail.SupplierInvoiceDetailId
                                             select e).FirstOrDefaultAsync<SupplierInvoiceDetail>();
                    if (queryDetail == null)
                    {
                        _dbContext.Set<SupplierInvoiceDetail>().Add(newDetail);
              
                    }
                }
                return CreateProcessStatus.Insert;
            }
            return CreateProcessStatus.Failed;
        }

        public async Task<bool> AddSupplierInvoice(SupplierInvoice supplierInvoice)
        {

            try
            {
                var query = await (from a in _dbContext.SupplierInvoice
                                   where a.SupplierInvoiceNumber == supplierInvoice.SupplierInvoiceNumber
                                   select a).FirstOrDefaultAsync<SupplierInvoice>();
                if (query == null)
                {
                    AddObject(supplierInvoice);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateSupplierInvoice(SupplierInvoice supplierInvoice)
        {
            try
            {
                var query = await GetObjectAsync(supplierInvoice.SupplierInvoiceId);

                SupplierInvoice supplierInvoiceBase = query;


                supplierInvoiceBase.Amount = supplierInvoice.Amount;
                supplierInvoiceBase.Description = supplierInvoice.Description;
                supplierInvoiceBase.SupplierInvoiceDate = supplierInvoice.SupplierInvoiceDate;
                supplierInvoiceBase.SupplierInvoiceNumber = supplierInvoice.SupplierInvoiceNumber;
                supplierInvoiceBase.Ponumber = supplierInvoice.Ponumber;
                supplierInvoiceBase.PaymentDueDate = supplierInvoice.PaymentDueDate;
                supplierInvoiceBase.PaymentTerms = supplierInvoice.PaymentTerms;
                supplierInvoiceBase.DiscountAmount = supplierInvoice.DiscountAmount;
                supplierInvoiceBase.TaxAmount = supplierInvoice.TaxAmount;
                supplierInvoiceBase.FreightCost = supplierInvoice.FreightCost;
                supplierInvoiceBase.DiscountDueDate = supplierInvoice.DiscountDueDate;

                UpdateObject(supplierInvoiceBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool DeleteSupplierInvoice(SupplierInvoice supplierInvoice)
        {
            try
            {
                DeleteObject(supplierInvoice);
                //TODO Delete all detail
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
