using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderManagementDomain.Repository
{
    public class SalesOrderView
    {
        public long SalesOrderId { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? Amount { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public long CustomerId { get; set; }
        public string TakenBy { get; set; }
        public decimal? FreightAmount { get; set; }
        public string PaymentInstrument { get; set; }
        public string PaymentTerms { get; set; }
        public string Note { get; set; }
    }
    public class SalesOrderRepository :  Repository<SalesOrder> , ISalesOrderRepository
    {
        ListensoftwaredbContext _dbContext;
        public SalesOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<SalesOrder> GetSalesOrderByNumber(string orderNumber)
        {
            return await _dbContext.SalesOrder.Where(salesOrder=>salesOrder.OrderNumber == orderNumber).FirstOrDefaultAsync<SalesOrder>();
        }
        public async Task<SalesOrder> GetSalesOrderById(long salesOrderId)
        {
            return await _dbContext.SalesOrder.FindAsync(salesOrderId);
        }
    }
}
