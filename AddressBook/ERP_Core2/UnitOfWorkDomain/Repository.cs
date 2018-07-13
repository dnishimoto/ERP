using ERP_Core2.EntityFramework;
using MillenniumERP.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MillenniumERP.Services
{
    public class Repository<T> where T : class
    {
        private DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
             _dbContext=dbContext;  
        }
        public string GetMyMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }
        public void UpdateObject(T dataObject)
        {

            //if (_dbContext.Entry(dataObject).State == EntityState.Detached || _dbContext.Entry(dataObject).State == EntityState.Modified)
            //{
                _dbContext.Entry(dataObject).State = System.Data.Entity.EntityState.Modified;
                //_dbContext.Set<T>().Attach(dataObject);
            //}

         

         }
        public async Task<NextNumber> GetNextNumber(string nextNumberName)
        {
            //Entities _dbEntities = (Entities)_dbContext;

            SqlParameter param1 = new SqlParameter("@NextNumberName", nextNumberName);
            NextNumber nextNumber = await _dbContext.Database.SqlQuery<NextNumber>("usp_GetNextNumber @NextNumberName", param1).SingleAsync();

            //foreach (NextNumber item in query)
            //{
             //    nextNumber=item;
           // }
            /*
            NextNumber nextNumber = null;
            long? currentNextNumberValue = 0;
            using (DbContextTransaction scope = _dbEntities.Database.BeginTransaction())
            {
                //Lock the table during this transaction
                nextNumber = await (from e in _dbEntities.NextNumbers
                                               where e.NextNumberName == nextNumberName
                                               select e).FirstOrDefaultAsync<NextNumber>();

                currentNextNumberValue = nextNumber.NextNumberValue;
                nextNumber.NextNumberValue += 1;
                _dbEntities.NextNumbers.Attach(nextNumber);
                _dbEntities.Entry(nextNumber).State = EntityState.Modified;
                _dbEntities.SaveChanges();
                nextNumber.NextNumberValue = currentNextNumberValue??0;

                scope.Commit();
            }
            */
            return nextNumber;
        }
        public async Task<AddressBook> GetAddressBookByCustomerView(CustomerView customerView)
        {
            try
            {
                Entities _dbEntities = (Entities)_dbContext;
                var query = await (from e in _dbEntities.AddressBooks
                                   join f in _dbEntities.Emails on e.AddressId equals f.AddressId
                                   where e.Name == customerView.CustomerName &&
                                   f.Email1 == customerView.AccountEmail.EmailText
                                   && f.LoginEmail == true
                                   select e).FirstOrDefaultAsync<AddressBook>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ChartOfAcct> GetChartofAccount(string company, string busUnit, string objectNumber, string subsidiary)
        {
            try
            {
                Entities _dbEntities = (Entities)_dbContext;


                ChartOfAcct chartOfAcct = await (from e in _dbEntities.ChartOfAccts
                                                 where e.CompanyNumber == company
                                                 && e.BusUnit == busUnit
                                                 && e.ObjectNumber == objectNumber
                                                 && (e.Subsidiary ?? "") == subsidiary
                                                 select e).FirstOrDefaultAsync<ChartOfAcct>();

                return chartOfAcct;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<long> GetAddressIdByCustomerId(long ? customerId)
        {
            try
            {
                Entities _dbEntities = (Entities)_dbContext;

                Customer customer = await (from e in _dbEntities.Customers
                                           where e.CustomerId == customerId
                                           select e).FirstOrDefaultAsync<Customer>();

                return customer.AddressId;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<UDC> GetUdc(string productCode, string keyCode)
        {
            try
            {
                Entities _dbEntities = (Entities)_dbContext;

                UDC udc = await (from e in _dbEntities.UDCs
                                 where e.ProductCode == productCode
                                 && e.KeyCode == keyCode
                                 select e).FirstOrDefaultAsync<UDC>();

                return udc;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<T> GetObjectAsync(int id)
        {
                Task<T> result = _dbContext.Set<T>().FindAsync(id);
                return await result;
        }
        public IQueryable<T> GetObjectsAsync(Expression<Func<T, bool>> predicate,string includeTable="")
        {
            IQueryable<T> result = _dbContext.Set<T>().Where(predicate);
            if (includeTable != "")
            {
                result=result.Include(includeTable);
            }
                
            
                return result;
        }
       
        public void DeleteObject(T dataObject)
        {

                _dbContext.Set<T>().Remove(dataObject);

        }
        public void DeleteObjects(List<T> dataObjects)
        {

                _dbContext.Set<T>().RemoveRange(dataObjects);
         }

        public void AddObject(T dataObject)
        {

                _dbContext.Set<T>().Add(dataObject);

        }
        public void AddObjects(List<T> dataObjects)
        {
            _dbContext.Set<T>().AddRange(dataObjects);
        }
    }
}
