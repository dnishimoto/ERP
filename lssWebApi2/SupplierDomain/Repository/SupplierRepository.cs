   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.SupplierDomain
{
    public  class SupplierView
    {
        public long SupplierId { get; set; }
        public long AddressId { get; set; }
        public string Identification { get; set; }
        public long SupplierNumber { get; set; }


        public string SupplierName { get; set; }

    }

    public class SupplierRepository: Repository<Supplier>, ISupplierRepository
    {
        ListensoftwaredbContext _dbContext;
        public SupplierRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<Supplier> FindEntityByAddressId(long ? addressId)
        {

            Supplier query = await (from e in _dbContext.Supplier

                                    where e.AddressId == addressId

                                    select e).FirstOrDefaultAsync<Supplier>();
            return query;
        }

        public async Task<Supplier>GetEntityById(long ? supplierId)
        {
			try{
            return await _dbContext.FindAsync<Supplier>(supplierId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Supplier> GetEntityByNumber(long supplierNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Supplier
                               where detail.SupplierNumber == supplierNumber
                               select detail).FirstOrDefaultAsync<Supplier>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		

        public async Task<Supplier> GetEntityByEmail(EmailEntity email)
        {

            Supplier query = await (from e in _dbContext.Supplier
                                    join f in _dbContext.EmailEntity
                                        on e.AddressId equals f.AddressId
                                    where f.Email == email.Email
                                    && f.LoginEmail == true
                                    select e).FirstOrDefaultAsync<Supplier>();


            return query;

        }
     


    }
}
