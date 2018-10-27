using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lssWebApi2;
using lssWebApi2.EntityFramework;

namespace ERP_Core2.TimeAndAttendanceDomain.Repository
{
    
    public class NextNumberRepository : Repository<NextNumber>
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public NextNumberRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
      
    }
}
