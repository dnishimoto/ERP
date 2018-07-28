using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.Services;
using ERP_Core2.AbstractFactory;
using System.Collections;
using MillenniumERP.GeneralLedgerDomain;

namespace MillenniumERP.PurchaseOrderDomain
{
    public class PurchaseOrderView
    {
        public PurchaseOrderView() { }
        public PurchaseOrderView(PurchaseOrder po)
        {
            this.PurchaseOrderId = po.PurchaseOrderId;
            this.DocType = po.DocType;
            this.PaymentTerms = po.PaymentTerms;
            this.GrossAmount = po.GrossAmount;
            this.Remark = po.Remark;
            this.GLDate = po.GLDate;
            this.AccountId = po.AccountId;
            this.SupplierId = po.SupplierId;
            this.SupplierName = po.Supplier.AddressBook.Name;
            this.ContractId = po.ContractId;
            this.POQuoteId = po.POQuoteId;
            this.Description = po.Description;
            this.PONumber = po.PONumber;
            this.Quantity = po.Quantity;
            this.UnitOfMeasure = po.UnitOfMeasure;
            this.TakenBy = po.TakenBy;
            this.ShippedToLocationId = po.ShippedToLocationId;
            this.BuyerId = po.BuyerId;
            this.RequestedDate = po.RequestedDate;
            this.PromisedDeliveredDate = po.PromisedDeliveredDate;
            this.Tax = po.Tax;
            this.TaxCode = po.TaxCode;
            this.TransactionDate = po.TransactionDate;
            this.QuantityReceived = po.QuantityReceived;
            this.AmountReceived = po.AmountReceived;
            this.AmountPaid = po.AmountPaid;
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
        public string Description { get; set; }
        public string PONumber { get; set; }
        public decimal? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string TakenBy { get; set; }
        public long? ShippedToLocationId { get; set; }
        public string ShippedAddress1 { get; set; }
        public string ShippedAddress2 { get; set; }
        public string ShippedCity { get; set; }
        public string ShippedState { get; set; }
        public string ShippedZipcode { get; set; }
        public long? BuyerId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? PromisedDeliveredDate { get; set; }
        public decimal? Tax { get; set; }
        public string TaxCode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? QuantityReceived { get; set; }
        public decimal? AmountReceived { get; set; }
        public decimal? AmountPaid { get; set; }

        public virtual ICollection PurchaseOrderDetailView { get; set; }
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
    }




    public class PurchaseOrderRepository: Repository<PurchaseOrder>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public PurchaseOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<PurchaseOrder> GetPurchaseOrderByDocNumber(string PONumber)
        {
            try
            {
                List<PurchaseOrder> list = await GetObjectsAsync(e => e.PONumber == PONumber).ToListAsync<PurchaseOrder>();
                PurchaseOrder purchaseOrder = list[0];

                return purchaseOrder;

            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
  
      
    }
}
