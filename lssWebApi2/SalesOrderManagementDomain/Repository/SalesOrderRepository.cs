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
    public class SalesOrderRepository :  Repository<SalesOrder> , ISalesOrderRepository
    {
        ListensoftwaredbContext _dbContext;
        public SalesOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    }
}
