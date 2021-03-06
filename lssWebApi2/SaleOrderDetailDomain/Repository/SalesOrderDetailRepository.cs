﻿using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using lssWebApi2.SalesOrderDetailDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace lssWebApi2.SalesOrderDetailDomain

{

    public class SalesOrderDetailView

    {
        public long SalesOrderDetailId { get; set; }
        public long SalesOrderId { get; set; }
        public long SalesOrderDetailNumber { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public long? Quantity { get; set; }
        public long? QuantityOpen { get; set; }
        public long? QuantityShipped { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountOpen { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public long? AccountId { get; set; }

        public long? CarrierId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? PurchaseOrderDetailId { get; set; }
        public long? PickListId { get; set; }
        public long? PickListDetailId { get; set; }
        public DateTime? ScheduledShipDate { get; set; }
        public DateTime? PromisedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? GLDate { get; set; }
        public decimal? GrossWeight { get; set; }
        public string GrossWeightUnitOfMeasure { get; set; }
        public decimal? UnitVolume { get; set; }
        public string UnitVolumeUnitOfMeasurement { get; set; }
        public string LotSerial { get; set; }
        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string CompanyCode { get; set; }
        public long? LineNumber { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentInstrument { get; set; }

        public string ItemDescription2 { get; set; }
        public string Account { get; set; }
        public string CarrierName { get; set; }
        public string OrderNumber { get; set; }
    }

    }

    public class SalesOrderDetailRepository : Repository<SalesOrderDetail>, ISalesOrderDetailRepository

    {

        ListensoftwaredbContext _dbContext;

        public SalesOrderDetailRepository(DbContext db) : base(db)

        {

            _dbContext = (ListensoftwaredbContext)db;

        }
        public IQueryable<SalesOrderDetail> GetIQueryableEntitiesBySalesOrderId(long ? salesOrderId)
        {
            IQueryable<SalesOrderDetail> query = _dbContext.SalesOrderDetail.Where(e => e.SalesOrderId == salesOrderId).Select(e => e);
            return query;
        }
        public async Task<IList<SalesOrderDetail>> GetEntitiesBySalesOrderId(long ? salesOrderId)
        {
            return await (from detail in _dbContext.SalesOrderDetail
                          where detail.SalesOrderId == salesOrderId
                          select detail).ToListAsync<SalesOrderDetail>();
        }

        public async Task<SalesOrderDetail> GetEntityById(long ? salesOrderDetailId)

        {

            return await _dbContext.FindAsync<SalesOrderDetail>(salesOrderDetailId);

        }

        public async Task<SalesOrderDetail> GetEntityByNumber(long salesOrderDetailNumber)

        {

            var query = await (from detail in _dbContext.SalesOrderDetail

                               where detail.SalesOrderDetailNumber == salesOrderDetailNumber
                               select detail).FirstOrDefaultAsync<SalesOrderDetail>();

            return query;

        }

        public async Task<IList<SalesOrderDetail>> GetDetailsBySalesOrderId(long ? salesOrderId)

        {

            var query =

                await (from detail in _dbContext.SalesOrderDetail

                       where detail.SalesOrderId == salesOrderId

                       select detail).ToListAsync<SalesOrderDetail>();

            return query;

        }

    }





