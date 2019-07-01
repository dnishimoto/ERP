using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.AccountPayableDomain.Repository;

namespace ERP_Core2.PurchaseOrderDomain
{
    public class PurchaseOrderView
    {
        public PurchaseOrderView() { this.PurchaseOrderDetailViews = new List<PurchaseOrderDetailView>(); }
        public PurchaseOrderView(PurchaseOrder po)
        {
            this.PurchaseOrderId = po.PurchaseOrderId;
            this.DocType = po.DocType;
            this.PaymentTerms = po.PaymentTerms;
            this.GrossAmount = po.GrossAmount;
            this.Remark = po.Remark;
            this.GLDate = po.Gldate;
            this.AccountId = po.AccountId;
            this.SupplierId = po.SupplierId;
            this.SupplierName = po.Supplier.Address.Name;
            this.ContractId = po.ContractId;
            this.POQuoteId = po.PoquoteId;
            this.Description = po.Description;
            this.PONumber = po.Ponumber;
            this.TakenBy = po.TakenBy;
            this.ShippedToName = po.ShippedToName;
            this.ShippedToAddress1 = po.ShippedToAddress1;
            this.ShippedToAddress2 = po.ShippedToAddress2;
            this.ShippedToCity = po.ShippedToCity;
            this.ShippedToState = po.ShippedToState;
            this.ShippedToZipcode = po.ShippedToZipcode;
            this.BuyerId = po.BuyerId;
            this.RequestedDate = po.RequestedDate;
            this.PromisedDeliveredDate = po.PromisedDeliveredDate;
            this.Tax = po.Tax;
            this.TaxCode1 = po.TaxCode1;
            this.TaxCode2 = po.TaxCode2;
            this.TransactionDate = po.TransactionDate;
            this.AmountPaid = po.AmountPaid;
            this.TaxCode1 = po.TaxCode1;
            this.TaxCode2 = po.TaxCode2;
            this.PurchaseOrderDetailViews = new List<PurchaseOrderDetailView>();
        }
        public long? PurchaseOrderId { get; set; }
        public string DocType { get; set; }
        public string PaymentTerms { get; set; }
        public decimal? GrossAmount { get; set; }
        public string Remark { get; set; }
        public DateTime? GLDate { get; set; }
        public long AccountId { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long? ContractId { get; set; }
        public long? POQuoteId { get; set; }
        public long? InvoiceId { get; set; }
        public string Description { get; set; }
        public string PONumber { get; set; }
        public string TakenBy { get; set; }
        public string ShippedToName { get; set; }
        public string ShippedToAddress1 { get; set; }
        public string ShippedToAddress2 { get; set; }
        public string ShippedToCity { get; set; }
        public string ShippedToState { get; set; }
        public string ShippedToZipcode { get; set; }
        public long? BuyerId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? PromisedDeliveredDate { get; set; }
        public decimal? Tax { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? AmountPaid { get; set; }
        public string TaxCode1 { get; set; }
        public string TaxCode2 { get; set; }
        public IList<PurchaseOrderDetailView> PurchaseOrderDetailViews { get; set; }
    }
    public class PurchaseOrderDetailView
    {
        public PurchaseOrderDetailView() { }
        public PurchaseOrderDetailView(PurchaseOrderDetail detail)

        {
            this.PurchaseOrderDetailId = detail.PurchaseOrderDetailId;
            this.PurchaseOrderId = detail.PurchaseOrderId;
            this.Amount = detail.Amount;
            this.OrderedQuantity = detail.OrderedQuantity;
            this.ItemId = detail.ItemId;
            this.UnitPrice = detail.UnitPrice;
            this.UnitOfMeasure = detail.UnitOfMeasure;
            this.ReceivedDate = detail.ReceivedDate;
            this.ExpectedDeliveryDate = detail.ExpectedDeliveryDate;
            this.OrderDate = detail.OrderDate;
            this.ReceivedQuantity = detail.ReceivedQuantity;
            this.RemainingQuantity = detail.RemainingQuantity;
            this.Description = detail.Description;
        }
        public long PurchaseOrderDetailId { get; set; }
        public long PurchaseOrderId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? OrderedQuantity { get; set; }
        public long ItemId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ReceivedQuantity { get; set; }
        public int? RemainingQuantity { get; set; }
        public string Description { get; set; }
    }




    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public PurchaseOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public async Task<PurchaseOrderView> GetPurchaseOrderViewByOrderNumber(string orderNumber)
        {
            try
            {
                List<PurchaseOrder> list = await GetObjectsQueryable(e => e.Ponumber == orderNumber).ToListAsync<PurchaseOrder>();

                PurchaseOrderView view = applicationViewFactory.MapPurchaseOrderView(list[0]);

                var query = await (from e in _dbContext.PurchaseOrderDetail
                                   where e.PurchaseOrderId == view.PurchaseOrderId
                                   select e).ToListAsync<PurchaseOrderDetail>();
                foreach (var item in query)
                {
                    view.PurchaseOrderDetailViews.Add(applicationViewFactory.MapPurchaseOrderDetailView(item));

                }
                return view;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreatePurchaseOrderByView(PurchaseOrderView purchaseOrderView)
        {
            decimal grossAmount = 0;
            try
            {
                //check if PO exists
                var queryPO = await (from e in _dbContext.PurchaseOrder
                                     where e.Ponumber == purchaseOrderView.PONumber

                                     select e).FirstOrDefaultAsync<PurchaseOrder>();
                if (queryPO != null) { return CreateProcessStatus.AlreadyExists; }


                foreach (var detail in purchaseOrderView.PurchaseOrderDetailViews)
                {
                    grossAmount += detail.Amount ?? 0;
                }
                purchaseOrderView.GrossAmount = grossAmount;
                purchaseOrderView.AmountPaid = 0;

                TaxRatesByCode tax = await base.GetTaxRatesByCode(purchaseOrderView.TaxCode1);
                purchaseOrderView.Tax = grossAmount * tax.TaxRate;

                PurchaseOrder po = new PurchaseOrder();
                applicationViewFactory.MapPurchaseOrderEntity(ref po, purchaseOrderView);

                base.AddObject(po);

                return CreateProcessStatus.Insert;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<CreateProcessStatus> CreatePurchaseOrderDetailsByView(PurchaseOrderView purchaseOrderView)
        {
            try
            {
                PurchaseOrder po = await (from e in _dbContext.PurchaseOrder
                                          where e.Ponumber == purchaseOrderView.PONumber
                                          select e).FirstOrDefaultAsync<PurchaseOrder>();


                if (po != null)
                {
                    long purchaseOrderId = po.PurchaseOrderId;

                    foreach (var detail in purchaseOrderView.PurchaseOrderDetailViews)
                    {
                        detail.PurchaseOrderId = purchaseOrderId;

                        PurchaseOrderDetail poDetail = new PurchaseOrderDetail();
                        applicationViewFactory.MapPurchaseOrderDetailEntity(ref poDetail, detail);

                        var queryPODetail = await (from e in _dbContext.PurchaseOrderDetail
                                                   where e.ItemId == detail.ItemId
                                                   && e.PurchaseOrderId == purchaseOrderId
                                                   select e).FirstOrDefaultAsync<PurchaseOrderDetail>();
                        if (queryPODetail == null)
                        {
                            _dbContext.Set<PurchaseOrderDetail>().Add(poDetail);

                        }
                    }
                   
                }
                return CreateProcessStatus.Insert;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<PurchaseOrder> GetPurchaseOrderByDocNumber(string PONumber)
        {
            try
            {
                List<PurchaseOrder> list = await GetObjectsQueryable(e => e.Ponumber == PONumber).ToListAsync<PurchaseOrder>();
                PurchaseOrder purchaseOrder = list[0];

                return purchaseOrder;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }


    }
}
