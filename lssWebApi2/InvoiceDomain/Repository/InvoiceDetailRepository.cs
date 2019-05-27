﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.InvoiceDomain.Repository;

namespace ERP_Core2.InvoiceDetailsDomain
{

    public class InvoiceDetailView
    {
        public InvoiceDetailView() { }
        public InvoiceDetailView(InvoiceDetail invoiceDetail)
        {
            this.InvoiceDetailId = invoiceDetail.InvoiceDetailId;
            this.UnitOfMeasure = invoiceDetail.UnitOfMeasure;
            this.Quantity = invoiceDetail.Quantity;
            this.UnitPrice = invoiceDetail.UnitPrice;
            this.Amount = invoiceDetail.Amount;
            this.DiscountPercent = invoiceDetail.DiscountPercent;
            this.DiscountAmount = invoiceDetail.DiscountAmount;
            this.DiscountDueDate = invoiceDetail.DiscountDueDate;
            this.ItemId = invoiceDetail.Item.ItemId;
            this.ItemNumber = invoiceDetail.Item.ItemNumber;
            this.ItemDescription = invoiceDetail.Item.Description;
            this.ItemDescription2 = invoiceDetail.Item.Description2;
            this.ExtendedDescription = invoiceDetail.ExtendedDescription;
            this.InvoiceId = invoiceDetail.InvoiceId;
        }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        //todo public long? PurchaseOrderLineId { get; set; }
        //todo public long? SalesOrderDetailId { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        //todo    public long? ShipmentDetailId { get; set; }
        //todo maybe public string InvoiceNumber { get; set; }
        //public virtual Invoice Invoice { get; set; }

        public long? ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public string ItemDescription2 { get; set; }

        public long? InvoiceId { get; set; }
        public long? InvoiceDetailId { get; set; }
        public string ExtendedDescription { get; set; }
    }
    public class InvoiceDetailRepository : Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        public ListensoftwaredbContext _dbContext;
        public ApplicationViewFactory applicationViewFactory;

        public InvoiceDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateInvoiceDetailsByView(InvoiceView invoiceView)
        {
            try
            {
                int count = 0;

                Invoice invoice = await (from e in _dbContext.Invoice
                                         where e.InvoiceNumber == invoiceView.InvoiceNumber
                                         select e).FirstOrDefaultAsync<Invoice>();

        
                //Assign the InvoiceId
                for (int i = 0; i < invoiceView.InvoiceDetailViews.Count; i++)
                {
                    invoiceView.InvoiceDetailViews[i].InvoiceId = invoice.InvoiceId;
                    InvoiceDetail invoiceDetail = new InvoiceDetail();
                    applicationViewFactory.MapInvoiceDetailEntity(ref invoiceDetail, invoiceView.InvoiceDetailViews[i]);

                    bool result = await AddInvoiceDetail(invoiceDetail);
                    if (result == true) { count++; }
                }
                if (count == 0) { return CreateProcessStatus.AlreadyExists; } else { return CreateProcessStatus.Insert; }
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<bool> AddInvoiceDetail(InvoiceDetail invoiceDetail)
        {

            try
            {
                var query = await (from a in _dbContext.InvoiceDetail
                                   where a.InvoiceId == invoiceDetail.InvoiceId
                                   select a).FirstOrDefaultAsync<InvoiceDetail>();
                if (query == null)
                {
                    AddObject(invoiceDetail);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<bool> UpdateInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            try
            {
                var query = await GetObjectAsync(invoiceDetail.InvoiceDetailId);

                InvoiceDetail invoiceDetailBase = query;

                invoiceDetailBase.InvoiceDetailId = invoiceDetail.InvoiceDetailId;
                invoiceDetailBase.UnitOfMeasure = invoiceDetail.UnitOfMeasure;
                invoiceDetailBase.Quantity = invoiceDetail.Quantity;
                invoiceDetailBase.UnitPrice = invoiceDetail.UnitPrice;
                invoiceDetailBase.Amount = invoiceDetail.Amount;
                invoiceDetailBase.DiscountPercent = invoiceDetail.DiscountPercent;
                invoiceDetailBase.DiscountAmount = invoiceDetail.DiscountAmount;
                invoiceDetailBase.ItemId = invoiceDetail.Item.ItemId;
                invoiceDetailBase.ExtendedDescription = invoiceDetail.ExtendedDescription;
                invoiceDetailBase.InvoiceId = invoiceDetail.InvoiceId;

                UpdateObject(invoiceDetailBase);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public bool DeleteInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            try
            {
                DeleteObject(invoiceDetail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
    }
}
