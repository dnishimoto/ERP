using ERP_Core2.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.entityframework;

namespace ERP_Core2.Services
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
                _dbContext.Entry(dataObject).State = EntityState.Modified;
            //_dbContext.Set<T>().Attach(dataObject);
            //}



        }
        public async Task<NextNumber> GetNextNumber(string nextNumberName)
        {
            try
            {
                //ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;

                SqlParameter param1 = new SqlParameter("@NextNumberName", nextNumberName);
                //NextNumber nextNumber = await _dbContext.Database.SqlQuery<NextNumber>("usp_GetNextNumber @NextNumberName", param1).SingleAsync();
                var command = _dbContext.Database.GetDbConnection().CreateCommand();

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_GetNextNumber";
                command.Parameters.Add(param1);

                _dbContext.Database.OpenConnection();
            
                var queryResults = command.ExecuteReader();
                NextNumber nextNumber = null;
                if (queryResults.Read())
                {
                            
                    foreach (NextNumber item in queryResults.Cast<NextNumber>())
                    {
                        nextNumber = item;
                    }
                }
                return nextNumber;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
      
            //foreach (NextNumber item in query)
            //{
            //    nextNumber=item;
            // }
            /*
            NextNumber nextNumber = null;
            long? currentNextNumberValue = 0;
            using (DbContextTransaction scope = _dbListensoftwareDBContext.Database.BeginTransaction())
            {
                //Lock the table during this transaction
                nextNumber = await (from e in _dbListensoftwareDBContext.NextNumbers
                                               where e.NextNumberName == nextNumberName
                                               select e).FirstOrDefaultAsync<NextNumber>();

                currentNextNumberValue = nextNumber.NextNumberValue;
                nextNumber.NextNumberValue += 1;
                _dbListensoftwareDBContext.NextNumbers.Attach(nextNumber);
                _dbListensoftwareDBContext.Entry(nextNumber).State = EntityState.Modified;
                _dbListensoftwareDBContext.SaveChanges();
                nextNumber.NextNumberValue = currentNextNumberValue??0;

                scope.Commit();
            }
            */

        }
        public async Task<AddressBook> GetAddressBookByCustomerView(CustomerView customerView)
        {
            try
            {
                ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;
                var query = await (from e in _dbListensoftwareDBContext.AddressBook
                                   join f in _dbListensoftwareDBContext.Emails on e.AddressId equals f.AddressId
                                   where e.Name == customerView.CustomerName &&
                                   f.Email == customerView.AccountEmail.EmailText
                                   && f.LoginEmail == true
                                   select e).FirstOrDefaultAsync<AddressBook>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Company> GetCompany()
        {
            try
            {
                ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;


                Company company = await (from e in _dbListensoftwareDBContext.Company
                                         where e.CompanyId == 1
                                         select e).FirstOrDefaultAsync<Company>();

                return company;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ChartOfAccts> GetChartofAccount(string company, string busUnit, string objectNumber, string subsidiary)
        {
            try
            {
                ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;


                ChartOfAccts chartOfAcct = await (from e in _dbListensoftwareDBContext.ChartOfAccts
                                                 where e.CompanyNumber == company
                                                 && e.BusUnit == busUnit
                                                 && e.ObjectNumber == objectNumber
                                                 && (e.Subsidiary ?? "") == subsidiary
                                                 select e).FirstOrDefaultAsync<ChartOfAccts>();

                return chartOfAcct;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<long> GetAddressIdByCustomerId(long ? customerId)
        {
            try
            {
                ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;

                Customer customer = await (from e in _dbListensoftwareDBContext.Customer
                                           where e.CustomerId == customerId
                                           select e).FirstOrDefaultAsync<Customer>();

                return customer.AddressId;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<TaxRatesByCode> GetTaxRateByCode(string TaxCode)
        {
            try
            {
                ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;

                TaxRatesByCode tax = await (from e in _dbListensoftwareDBContext.TaxRatesByCode
                                            where e.TaxCode == TaxCode
                                            select e).FirstOrDefaultAsync<TaxRatesByCode>();

                return tax;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Udc> GetUdc(string productCode, string keyCode)
        {
            try
            {
                ListensoftwareDBContext _dbListensoftwareDBContext = (ListensoftwareDBContext)_dbContext;

                Udc udc = await (from e in _dbListensoftwareDBContext.Udc
                                 where e.ProductCode == productCode
                                 && e.KeyCode == keyCode
                                 select e).FirstOrDefaultAsync<Udc>();

                return udc;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<T> GetObjectAsync(long id)
        {
                Task<T> result = _dbContext.Set<T>().FindAsync(id);
                return await result;
        }
        public IQueryable<T> GetObjectsQueryable(Expression<Func<T, bool>> predicate,string includeTable="")
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
